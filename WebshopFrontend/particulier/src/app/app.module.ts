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
import { shoppingCartService, IProductPair } from './shared';

const appRoutes: Routes = [
  { path: 'product/:id', component: ProductComponent },
  {
    path: 'catalogus',
    component: CatalogusComponent,
    data: { title: 'Catalogus' }
  },
  {
    path: 'winkelwagen',
    component: WinkelwagenComponent,
    data: { title: 'Winkelwagen' }
  },
  { path: '',
    redirectTo: '/catalogus',
    pathMatch: 'full'
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    CatalogusComponent,
    PageNotFoundComponent,
    ProductComponent,
    WinkelwagenComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [shoppingCartService],
  bootstrap: [AppComponent]
})
export class AppModule { 
 }
