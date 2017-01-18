param(
    $rootURL
)

foreach ($folder in $(Get-ChildItem -Path $rootURL | Where-Object{ $_.PSIsContainer -and $_.Name -ne "Documentatie" -and $_.Name -ne "WebshopFrontend" })) {
    foreach($item in $(Get-ChildItem -Path "$($folder.FullName)\src")) {
        if($item.Name -like "*.Test" ) {
            dotnet test "$($item.FullName)" -xml TEST-TestBuild.xml 
        }
    }            
}    

