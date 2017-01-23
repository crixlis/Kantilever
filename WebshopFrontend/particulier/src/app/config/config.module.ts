import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { AppComponent } from './../app.component';
import { CatalogusComponent } from './../catalogus';
import { PageNotFoundComponent } from './../pageNotFound';
import { ProductComponent } from './../product';
import { WinkelwagenComponent } from './../winkelwagen';

import { ConfigComponent } from './config.component';

@NgModule({
  declarations: [
    AppComponent,
    CatalogusComponent,
    PageNotFoundComponent,
    ProductComponent,
    WinkelwagenComponent,
    ConfigComponent,
  ],
  bootstrap: [ConfigComponent]
})
export class ConfigModule { 
 }