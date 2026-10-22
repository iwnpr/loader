# prepare-xsd.ps1
# Creates modified copies of XSD schemas with date validation (>= 1900-01-01)
#
# 1. ReferencesTypes.xsd -> ReferencesTypes.Modified.xsd (+ validDateType)
# 2. Other .xsd -> *.Modified.xsd (xs:date -> validDateType in unions)
# 3. All xs:include updated to *.Modified.xsd
#
# Run: powershell -ExecutionPolicy Bypass -File .\prepare-xsd.ps1
 
param(
    [string]$SourceDir = "."
)
 
$ReferencesFile = "ReferencesTypes.xsd"

$AcceptanceFile = "NotificationOfAcceptance.xsd"

$ReceiptFile = "NotificationOfReceipt.xsd"
 
$ValidDateTypeBlock = @"
 
  <!-- Date with restriction >= 1900-01-01 -->
  <xs:simpleType name="validDateType">
    <xs:restriction base="xs:date">
      <xs:minInclusive value="1900-01-01"/>
    </xs:restriction>
  </xs:simpleType>
 
"@

$ValidDateTimeTypeBlock = @"
 
  <!-- Date with restriction >= 1900-01-01 -->
  <xs:simpleType name="validDateTimeType">
    <xs:restriction base="xs:dateTime">
      <xs:minInclusive value="1900-01-01T00:00:00"/>
    </xs:restriction>
  </xs:simpleType>
 
"@
 
$utf8NoBom = [System.Text.UTF8Encoding]::new($false)
 
$xsdFiles = Get-ChildItem -Path $SourceDir -Filter "*.xsd" |
    Where-Object { $_.Name -notmatch '\.Modified\.xsd$' }
 
foreach ($file in $xsdFiles) {
	if ($file.Name -ne $AcceptanceFile -and $file.Name -ne $ReceiptFile) {
		Write-Host "Processing $($file.Name)..." -ForegroundColor Cyan
	 
		$content = [System.IO.File]::ReadAllText($file.FullName, $utf8NoBom)
		$modified = $content
	 
		# ReferencesTypes: add validDateType
		if ($file.Name -eq $ReferencesFile) {
			$modified = $modified -replace 'xs:dateTime', 'validDateTimeType'
			$modified = $modified -replace 'xs:date', 'validDateType'
			$modified = $modified -replace '</xs:schema>', "$ValidDateTypeBlock$ValidDateTimeTypeBlock</xs:schema>"
		}
	 
		# All others: replace xs:date -> validDateType in union memberTypes
		if ($file.Name -ne $ReferencesFile) {
			$modified = $modified -replace 'xs:dateTime', 'validDateTimeType'
			$modified = $modified -replace 'xs:date', 'validDateType'
		}
	 
		# Update include schemaLocation to *.Modified.xsd
		$modified = [regex]::Replace(
			$modified,
			'(<xs:include\s+schemaLocation=")([^"]+?)(\.xsd)(")',
			'$1$2.DateCheck$3$4'
		)
	 
		$outputName = $file.Name -replace '\.xsd$', '.DateCheck.xsd'
		$outputPath = Join-Path $SourceDir $outputName
		[System.IO.File]::WriteAllText($outputPath, $modified, $utf8NoBom)
		Write-Host "  Created: $outputName" -ForegroundColor Green
	}
}
 
#Write-Host ""
#Write-Host "Done. Use *.Modified.xsd files for generation." -ForegroundColor Cyan
 