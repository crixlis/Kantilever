cd %1\WebshopBeheer\src\WebshopBeheer
call dotnet publish -o %1\WebshopBeheer\src\WebshopBeheer\bin\publish -c release
cd %1\BestelService\src\BestelService
call dotnet publish -o .\BestelService\src\BestelService\bin\publish -c release
cd %1\WebshopAPI\src\Webshop.API
call dotnet publish -o .\WebshopAPI\src\Webshop.API\bin\publish -c release
cd %1\WebshopAPI\src\Webshop.Listener
call dotnet publish -o .\WebshopAPI\src\Webshop.Listener\bin\publish -c release
cd %1\FactuurService\src\FactuurService
call dotnet publish -o .\FactuurService\src\FactuurService\bin\publish -c release

