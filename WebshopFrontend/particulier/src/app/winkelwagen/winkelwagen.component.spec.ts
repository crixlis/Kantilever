/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WinkelwagenComponent } from './winkelwagen.component';
import { PrijsPipe, ShoppingCartService, ArtikelService, Artikel } from './../shared';

//http mocking
import { Http, BaseRequestOptions, XHRBackend, HttpModule } from '@angular/http' ;
import { MockBackend } from '@angular/http/testing';

describe('WinkelwagenComponent', () => {
  let component: WinkelwagenComponent;
  let fixture: ComponentFixture<WinkelwagenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: 
      [ 
        WinkelwagenComponent,
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
      ],
      imports: [
        HttpModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelwagenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
