import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { CatalogusComponent } from './catalogus.component';
import { shoppingCartService } from './../shared';

@NgModule({
  declarations: [
    CatalogusComponent
  ],
  imports: [],
  providers: [
    shoppingCartService
  ]
})
export class CatalogusModule { }