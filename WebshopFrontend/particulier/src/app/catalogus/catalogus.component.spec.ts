/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CatalogusComponent } from './catalogus.component';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared';

//http mocking
import { Http, BaseRequestOptions, XHRBackend, HttpModule } from '@angular/http' ;
import { MockBackend } from '@angular/http/testing';

describe('CatalogusComponent', () => {
  let component: CatalogusComponent;
  let fixture: ComponentFixture<CatalogusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: 
        [ 
          CatalogusComponent,
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
        }
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
