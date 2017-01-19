import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { CatalogusComponent } from './catalogus.component';
import { ShoppingCartService } from './../shared';

@NgModule({
  declarations: [
    CatalogusComponent
  ],
  imports: [],
  providers: [
    ShoppingCartService
  ]
})
export class CatalogusModule { }