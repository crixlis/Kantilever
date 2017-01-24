import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { BestellingAfrondenComponent } from './bestellingAfronden.component';
import { ShoppingCartService, ArtikelService } from './../shared';

@NgModule({
  declarations: [
    BestellingAfrondenComponent
  ],
  imports: [],
  providers: [
    ShoppingCartService,
    ArtikelService
  ]
})
export class BestellingAfrondenModule { }