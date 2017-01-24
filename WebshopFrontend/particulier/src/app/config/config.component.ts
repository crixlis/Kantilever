import { Component, OnInit } from '@angular/core';
import { Routes } from '@angular/router';

import { ProductComponent } from './../product';
import { CatalogusComponent } from './../catalogus';
import { WinkelwagenComponent } from './../winkelwagen';
import { BestellingAfrondenComponent } from './../bestellingAfronden';
import { PageNotFoundComponent } from './../pageNotFound';

@Component({
  selector: 'config'
})
export class ConfigComponent {

    static routes : Routes = [
        { 
            path: 'product/:id', 
            component: ProductComponent },
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
        {
            path: 'bestellingAfronden',
            component: BestellingAfrondenComponent,
            data: { title: 'Bestelling afronden' }
        },
        { path: '',
            redirectTo: '/catalogus',
            pathMatch: 'full'
        },
        { path: '**', component: PageNotFoundComponent }
    ]
}