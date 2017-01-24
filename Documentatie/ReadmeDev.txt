                    ##        .            
              ## ## ##       ==            
           ## ## ## ##      ===            
       /""""""""""""""""\___/ ===        
  ~~~ {~~ ~~~~ ~~~ ~~~~ ~~ ~ /  ===- ~~~   
       \______ o          __/            
         \    \        __/             
          \____\______/         



WebshopBeheerContext Migrations
Add-Migration "migration" -Project "WebshopBeheer.Database" -StartupProject "WebshopBeheer.Listener"
Remove-Migration -Project "WebshopBeheer.Database" -StartupProject "WebshopBeheer.Listener"
Update-Database -Project "WebshopBeheer.Database" -StartupProject "WebshopBeheer.Listener"