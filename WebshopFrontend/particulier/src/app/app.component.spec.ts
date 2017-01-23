/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ShoppingCartService, ArtikelService } from './shared';

@Component({
  template: '<p>CatalogusComponent werkt!</p>'
})
class CatalogusComponent {}

@Component({
  template: '<p>PageNotFoundComponent werkt!</p>'
})
class PageNotFoundComponent {}

describe('AppComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        CatalogusComponent,
        PageNotFoundComponent        
      ],
      providers: [
        ShoppingCartService,
        ArtikelService
      ],
      imports: [
        RouterTestingModule.withRoutes([
          { 
            path: 'catalogus', 
            component: CatalogusComponent 
          },
          { 
            path: '',
            redirectTo: '/catalogus',
            pathMatch: 'full'
          },
          { 
            path: '**', 
            component: PageNotFoundComponent 
          }
        ])
      ]
    });
    TestBed.compileComponents();
  });

  it('should create the app', async(() => {
    let fixture = TestBed.createComponent(AppComponent);
    let app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));

  it(`should have a navigation element`, async(() => {
    let fixture = TestBed.createComponent(AppComponent);
    let compiled = fixture.debugElement.nativeElement;
    expect(compiled.querySelector('nav')).toBeDefined;
  }));

  let router, location;

  describe('by default navigates', () => {

    beforeEach(inject([Router, Location], (_router: Router, _location: Location) => {
      router = _router;
      location = _location;
    }));

    it('to catalogus',
      async(inject([Router, Location], (router: Router, location: Location) => {
        let fixture = TestBed.createComponent(AppComponent);
        fixture.detectChanges();
        router.navigate(['']).then(() => {
        expect(location.path()).toBe('/catalogus');
    });

    })));
  });

});
