import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BestellingAfrondenComponent } from './bestellingAfronden.component';
import { ShoppingCartService, ArtikelService, BestellingService } from './../shared';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    BestellingAfrondenComponent
  ],
  imports: [],
  providers: [
    ShoppingCartService,
    ArtikelService,
    BestellingService    
  ]
})
export class BestellingAfrondenModule { }