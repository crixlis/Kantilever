/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed, getTestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import { ProductComponent } from './product.component';
import { PrijsPipe, ShoppingCartService, ArtikelService, Artikel } from './../shared';

//Router mocking
import { ActivatedRoute, Data } from '@angular/router';

//http mocking
import { Http, BaseRequestOptions, XHRBackend, HttpModule } from '@angular/http' ;
import { MockBackend } from '@angular/http/testing';

describe('ProductComponent', () => {
  let component: ProductComponent;
  let fixture: ComponentFixture<ProductComponent>;
  let shoppingCartService, artikelService;
  let mockBackend: MockBackend;

  let getArtikelMockData : Artikel = new Artikel(
    {
      id: 2,
      naam: 'product2',
      beschrijving: 'beschrijving',
      prijs: 9.99,
      leverbaarVanaf: null,
      leverbaarTot: null,
      leverancier: 'leverancier',
      categorieen: [
        'categorie1',
        'categorie2'
      ],
      voorraad: 400
    });

  let mockedIdUrlQuery = '2';

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: 
      [ 
        ProductComponent,
        PrijsPipe
      ],
      providers: [
        ShoppingCartService,
        ArtikelService,
        MockBackend,
        BaseRequestOptions,
        {
          provide: Http,
          deps: [MockBackend, BaseRequestOptions],
          useFactory:
            (backend: XHRBackend, defaultOptions: BaseRequestOptions) => {
                return new Http(backend, defaultOptions);
            }
        },
        {
          provide: ActivatedRoute,
          useValue: {
              params: Observable.from([{ 'id': 1 }])
          }
        }
      ],
      imports: [
        HttpModule
      ]
    });
  
    mockBackend = getTestBed().get(MockBackend);

    fixture = TestBed.createComponent(ProductComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
