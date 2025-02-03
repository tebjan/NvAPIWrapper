$filename = "R560-developer\amd64\nvapi64.lib"
$dllFilename = [System.IO.Path]::ChangeExtension($filename, ".dll")
$dumpbinAddress = "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Tools\MSVC\14.42.34433\bin\Hostx86\x64\dumpbin.exe"
$dumpbinParameter = "/DISASM `"$filename`""

# Define output files
$rawOutputFile = "nvapi64_disassembly.txt"
$parsedOutputFile = "nvapi64_functions.txt"

# Run dumpbin and save raw output
Start-Process -FilePath $dumpbinAddress -ArgumentList $dumpbinParameter -Wait -NoNewWindow -RedirectStandardOutput $rawOutputFile

# Read the disassembly output
$content = Get-Content $rawOutputFile
$functionName = ""
$extractedFunctions = @()

foreach ($line in $content) {
    if (!$line) {
        if ($functionName -ne "") {			
            # Write function name with no address if not found
            $extractedFunctions += "$functionName = FAILED,"
        }
        $functionName = ""
        continue
    }

    # Identify function names that start with NvAPI_
    if ($functionName -eq "" -and $line.EndsWith(":") -and ($line.StartsWith("NvAPI_"))) {
        $functionName = $line.TrimEnd(':')
        continue
    }

    # Extract function address when found
    $leadingPattern = "ecx,"
    if ($functionName -ne "" -and $line.Contains($leadingPattern) -and $line.EndsWith("h")) {
        $functionAddress = $line.Substring($line.IndexOf($leadingPattern) + $leadingPattern.Length).TrimEnd('h')
        $functionAddressNumberic = 0

        if ([int32]::TryParse($functionAddress, 
            [System.Globalization.NumberStyles]::HexNumber, 
            [System.Globalization.CultureInfo]::CurrentCulture, 
            [ref] $functionAddressNumberic)) {
            $functionAddress = $functionAddressNumberic.ToString("X")
            $extractedFunctions += "$functionName = 0x$functionAddress,"
            $functionName = ""
            continue
        }
    }
}

# Sort the extracted functions alphabetically
$sortedFunctions = $extractedFunctions | Sort-Object

# Save sorted function addresses
$sortedFunctions | Out-File $parsedOutputFile -Encoding utf8

Write-Host "Raw disassembly saved to: $rawOutputFile"
Write-Host "Extracted function addresses (sorted, comma-separated) saved to: $parsedOutputFile"
