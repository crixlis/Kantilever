import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { CatalogusComponent } from './catalogus';
import { PageNotFoundComponent } from './pageNotFound';
import { ProductComponent } from './product';

const appRoutes: Routes = [
  { path: 'product/:id', component: PageNotFoundComponent },
  {
    path: 'catalogus',
    component: CatalogusComponent,
    data: { title: 'Catalogus' }
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
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
