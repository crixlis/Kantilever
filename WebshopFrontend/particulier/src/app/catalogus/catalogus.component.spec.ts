/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CatalogusComponent } from './catalogus.component';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared'

class ShoppingCartServiceStub = {
      public amountOfProducts = 4;

      amountOfProducts() {
        return Promise.resolve(this.amountOfProducts);
      }

      addProduct() {
      }
    };

class ArtikelServiceStub = {

      public artikelen = {
          id: 1
          naam: "yolo"
        },

      getArtikelen() {
        return Promise.resolve(this.artikelen);
      }
    };

describe('CatalogusComponent', () => {
  let component: CatalogusComponent;
  let fixture: ComponentFixture<CatalogusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogusComponent ]
      providers:    [ 
        {provide: ShoppingCartService, useValue: ShoppingCartServiceStub },
        {provide: ArtikelService, useValue: ArtikelServiceStub }
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
