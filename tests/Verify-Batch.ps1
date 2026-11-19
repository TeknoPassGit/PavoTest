$ErrorActionPreference = 'Stop'
$dir = (Join-Path $PSScriptRoot '..\PavoTest\bin\Release')
[void][Reflection.Assembly]::LoadFrom((Join-Path $dir 'Newtonsoft.Json.dll'))
$asm = [Reflection.Assembly]::LoadFrom((Join-Path $dir 'PavoTest.exe'))
$flags = [Reflection.BindingFlags]'Instance,Public,NonPublic'
$payType = $asm.GetType('PavoTest.PavoPay')
$lineType = $asm.GetType('PavoTest.PavoPay+SatisKalemi')
$pay = [Activator]::CreateInstance($payType, $true)
$list = $payType.GetField('SatisKalemleri').GetValue($pay)
$build = $payType.GetMethod('SatisIstegiOlustur', $flags)
function Assert($ok, $message) { if (!$ok) { throw $message } }
$kdvRates = @(20,10,1,0,20,10)
$names = @('Otopark "A"', 'İşlem\B', "Satır`nücreti", 'Abonelik', 'Yıkama', 'Hizmet')
for ($i=0; $i -lt 6; $i++) {
    $line = $lineType.GetConstructor([type[]]@([string], [decimal], [int])).Invoke([object[]]@($names[$i], ([decimal]($i + 1) + [decimal]0.10), [int]$kdvRates[$i]))
    [void]$list.Add($line)
}
$cari = $payType.GetField('secilenCari').GetValue($pay)
$cari.CARIADI = 'Firma "A"'
$cari.ADRES = "Adres\1`nİstanbul"
foreach ($culture in @('tr-TR', 'en-US')) {
    [Threading.Thread]::CurrentThread.CurrentCulture = [Globalization.CultureInfo]::GetCultureInfo($culture)
    foreach ($kind in @(0,1,14)) {
        $json = $build.Invoke($pay, [object[]]@('TEST', [int]$kind, [decimal]1)) | ConvertFrom-Json
        $sale = $json.Sale
        Assert ($sale.AddedSaleItems.Count -eq 6) 'Six items missing'
        Assert ([decimal]$sale.TotalPrice -eq [decimal]21.60) 'Total mismatch'
        Assert ([decimal]$sale.GrossPrice -eq [decimal]21.60) 'Gross mismatch'
        Assert ([decimal]$sale.PaymentInformations[0].Amount -eq [decimal]21.60) 'Payment mismatch'
        $mediator = if ($kind -eq 14) {14} else {$kind+1}
        Assert ($sale.PaymentInformations[0].Mediator -eq $mediator) 'Mediator mismatch'
        if ($kind -eq 14) { Assert ($sale.PaymentInformations[0].Brand -eq 225) 'HGS brand mismatch' }
        Assert ($sale.CustomerParty.CompanyName -eq $cari.CARIADI) 'Customer escaping mismatch'
        for ($i=0; $i -lt 6; $i++) {
            Assert ($sale.AddedSaleItems[$i].TaxGroupCode -eq ('KDV' + $kdvRates[$i])) 'Per-row VAT mismatch'
            Assert ($sale.AddedSaleItems[$i].Name -eq $names[$i]) 'Name escaping mismatch'
            Assert ([decimal]$sale.AddedSaleItems[$i].TotalPriceAmount -eq ([decimal]($i+1)+[decimal]0.10)) 'Item amount mismatch'
        }
    }
}
$list.Clear()
$single = $build.Invoke($pay, [object[]]@('TEST', [int]0, [decimal]12.50)) | ConvertFrom-Json
Assert ($single.Sale.AddedSaleItems.Count -eq 1) 'Single item fallback broken'
Assert ([decimal]$single.Sale.TotalPrice -eq [decimal]12.50) 'Fallback amount mismatch'
Assert ($single.Sale.AddedSaleItems[0].TaxGroupCode -eq 'KDV20') 'Default single VAT mismatch'
$formType = $asm.GetType('PavoTest.Form1')
$form = [Activator]::CreateInstance($formType)
$grid = $formType.GetField('tahsilatListesi', $flags).GetValue($form)
$read = $formType.GetMethod('KalemleriOku', $flags)
for ($i=0; $i -lt 6; $i++) {
    Assert ($grid.Rows[$i].Cells[2].Value -eq 20) 'Default UI VAT mismatch'
    $grid.Rows[$i].Cells[2].Value = $kdvRates[$i]
    $grid.Rows[$i].Cells[0].Value = $names[$i]
    $grid.Rows[$i].Cells[1].Value = if ($i % 2 -eq 0) { '12,50' } else { '12.50' }
}
$items = $read.Invoke($form, @())
Assert ($items.Count -eq 6) 'UI six rows not read'
Assert (($items | Measure-Object -Property Tutar -Sum).Sum -eq 75) 'UI decimal parsing failed'
for ($i=0; $i -lt 6; $i++) { Assert ($items[$i].Kdv -eq $kdvRates[$i]) 'UI VAT mismatch' }
foreach ($invalidKdv in @('', '-1', '101', '20,5', 'abc')) {
    $grid.Rows[0].Cells[2].Value = $invalidKdv
    $rejected = $false
    try { [void]$read.Invoke($form, @()) } catch { $rejected = $true }
    Assert $rejected ('Accepted invalid VAT ' + $invalidKdv)
}
$grid.Rows[0].Cells[2].Value = 20
foreach ($invalid in @('0', '-1', '1,234', '1.234,56', 'abc')) {
    $grid.Rows[0].Cells[1].Value = $invalid
    $rejected = $false
    try { [void]$read.Invoke($form, @()) } catch { $rejected = $true }
    Assert $rejected ('Accepted invalid amount ' + $invalid)
}
$grid.Rows.Clear()
$newRow = $grid.Rows.Add()
Assert ($grid.Rows[$newRow].Cells[2].Value -eq 20) 'Default new-row VAT mismatch'
$grid.Rows.Clear()
$rejected = $false
try { [void]$read.Invoke($form, @()) } catch { $rejected = $true }
Assert $rejected 'Accepted empty batch'
$csvRead = $formType.GetMethod('CsvKalemleriOku', [Reflection.BindingFlags]'Static,NonPublic')
$tempCsv = Join-Path ([IO.Path]::GetTempPath()) ([Guid]::NewGuid().ToString() + '.csv')
try {
    $utf8 = New-Object Text.UTF8Encoding($true)
    [IO.File]::WriteAllText($tempCsv, "KDV;Tutar;İşlem Adı`r`n10;12,50;Yıkama`r`n;7.50;Otopark", $utf8)
    $csvItems = $csvRead.Invoke($null, [object[]]@([string]$tempCsv))
    Assert ($csvItems.Count -eq 2) 'CSV row count mismatch'
    Assert ($csvItems[0].Ad -eq 'Yıkama' -and $csvItems[0].Tutar -eq 12.50 -and $csvItems[0].Kdv -eq 10) 'CSV reordered columns mismatch'
    Assert ($csvItems[1].Kdv -eq 20) 'CSV blank VAT default mismatch'
    $loadCsv = $formType.GetMethod('CsvListesiniYukle', $flags)
    [void]$loadCsv.Invoke($form, [object[]]@([string]$tempCsv))
    $loaded = $read.Invoke($form, @())
    Assert ($loaded.Count -eq 2 -and $loaded[0].Ad -eq 'Yıkama') 'CSV grid load mismatch'
    Assert ($formType.GetField('topluTahsilat', $flags).GetValue($form).Checked) 'CSV batch mode not enabled'
    [void]$loadCsv.Invoke($form, [object[]]@([string]($tempCsv + '.missing')))
    Assert (($read.Invoke($form, @())).Count -eq 2) 'Missing CSV changed grid'
    [IO.File]::WriteAllText($tempCsv, "IslemAdi,Tutar`r`n`"Otopark, `"`"A`"`"`",`"12,50`"", $utf8)
    $csvItems = $csvRead.Invoke($null, [object[]]@([string]$tempCsv))
    Assert ($csvItems[0].Ad -eq 'Otopark, "A"' -and $csvItems[0].Tutar -eq 12.50 -and $csvItems[0].Kdv -eq 20) 'CSV quoting or absent VAT mismatch'
    foreach ($badCsv in @('', 'Ad;Fiyat', "IslemAdi;Tutar;Kdv`nTest;1;101", "IslemAdi;Tutar`nTest;abc", "IslemAdi;Tutar`nTest;1;20", "IslemAdi;Tutar`n`"Kapanmayan;1")) {
        [IO.File]::WriteAllText($tempCsv, $badCsv, $utf8)
        $rejected = $false
        try { [void]$csvRead.Invoke($null, [object[]]@([string]$tempCsv)) } catch { $rejected = $true }
        Assert $rejected 'Accepted invalid CSV'
    }
}
finally {
    if (Test-Path -LiteralPath $tempCsv) { Remove-Item -LiteralPath $tempCsv }
}
$form.Dispose()
Write-Output 'PASS: CSV import, headers, defaults, quoting, missing/invalid files; per-row VAT, defaults, invalid VAT; six-item cash/card/HGS payloads, tr-TR/en-US decimals, totals, escaping, customer, single-item fallback, UI rows, invalid and empty input.'
