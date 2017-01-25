import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BestellingAfrondenComponent } from './bestellingAfronden.component';
import { ShoppingCartService, ArtikelService } from './../shared';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    BestellingAfrondenComponent
  ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  providers: [
    ShoppingCartService,
    ArtikelService
  ]
})
export class BestellingAfrondenModule { }