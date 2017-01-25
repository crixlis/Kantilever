import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { CatalogusComponent } from './catalogus';
import { PageNotFoundComponent } from './pageNotFound';
import { ProductComponent } from './product';
import { WinkelwagenComponent } from './winkelwagen';
import { BestellingAfrondenComponent } from './bestellingAfronden';
import { ShoppingCartService, IProductPair, PrijsPipe, ArtikelService, BestellingService } from './shared';
import { ConfigComponent } from './config';

const appRoutes: Routes = ConfigComponent.routes;

@NgModule({
  declarations: [
    AppComponent,
    CatalogusComponent,
    PageNotFoundComponent,
    ProductComponent,
    WinkelwagenComponent,
    BestellingAfrondenComponent,
    PrijsPipe
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [ShoppingCartService, ArtikelService, BestellingService],
  bootstrap: [AppComponent]
})
export class AppModule { 
 }
