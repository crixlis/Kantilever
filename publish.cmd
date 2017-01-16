cd .\WebshopBeheer\src\WebshopBeheer
call dotnet publish -o $(Build.Repository.LocalPath)\WebshopBeheer\src\WebshopBeheer\bin\publish -c release
cd $(Build.Repository.LocalPath)\BestelService\src\BestelService
call dotnet publish -o .\BestelService\src\BestelService\bin\publish -c release
cd $(Build.Repository.LocalPath)\WebshopAPI\src\Webshop.API
call dotnet publish -o .\WebshopAPI\src\Webshop.API\bin\publish -c release
cd $(Build.Repository.LocalPath)\WebshopAPI\src\Webshop.Listener
call dotnet publish -o .\WebshopAPI\src\Webshop.Listener\bin\publish -c release
cd $(Build.Repository.LocalPath)\FactuurService\src\FactuurService
call dotnet publish -o .\FactuurService\src\FactuurService\bin\publish -c release

