/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed, inject, flushMicrotasks, fakeAsync } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CatalogusComponent } from './catalogus.component';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared';

//http mocking
import { Http, BaseRequestOptions, XHRBackend, HttpModule, Response, ResponseOptions  } from '@angular/http' ;
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

  describe('should', () => {

    beforeEach(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
    });

    it('create', () => {
      expect(component).toBeTruthy();
    });

    it('construct an ArtikelService', async(inject(
      [ArtikelService, MockBackend], (service, mockBackend) => {
      expect(service).toBeDefined();
    })));

    it('construct a ShoppingCartService', async(inject(
      [ShoppingCartService, MockBackend], (service, mockBackend) => {
      expect(service).toBeDefined();
    })));
  });

  describe('receiving 4 artikelen from ArtikelService', () => {

    const mockResponse = [
      new Artikel({
          id: 1,
          naam: 'naam1',
          beschrijving: 'beschrijving1',
          prijs: 12.99,
          leverancier: 'leverancier1',
          categorieen: [
            'categorie1',
            'categorie2'
          ]
      }),
      new Artikel({
            id: 3,
            naam: 'naam3',
            beschrijving: 'beschrijving3',
            prijs: 15,
            leverancier: 'leverancier3',
            categorieen: [
              'categorie1',
              'categorie2'
            ]
      }),
      new Artikel({
            id: 8,
            naam: 'naam8',
            beschrijving: 'beschrijving8',
            prijs: 12.60,
            leverancier: 'leverancier8',
            categorieen: [
              'categorie1',
              'categorie2'
            ]
      }),
      new Artikel({
            id: 2,
            naam: 'naam2',
            beschrijving: 'beschrijving2',
            prijs: 1000,
            leverancier: 'leverancier2',
            categorieen: [
              'categorie1',
            ]
      })
    ];

    beforeEach(async(inject( [ArtikelService, MockBackend], (service : ArtikelService, mockBackend : MockBackend) => {
      mockBackend.connections.subscribe(conn => {
        conn.mockRespond(new Response(new ResponseOptions({ body: mockResponse })));
      });
    })));

    it('should show 4 artikelen op de catalogusPagina', fakeAsync(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

      let compiled = fixture.debugElement.nativeElement;
      let list : HTMLUListElement = compiled.querySelector('#catalogus ul');

      expect(list).toBeDefined;
      expect(list.childElementCount).toBe(0);

      flushMicrotasks(); //execute service
      fixture.detectChanges(); //refresh view with new model
      
      expect(list).toBeDefined;
      expect(list.childElementCount).toBe(4);
    }));

    it('should show the naam, prijs, beschrijving for the first artikel', fakeAsync(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

      let compiled = fixture.debugElement.nativeElement;
      let list : HTMLUListElement = compiled.querySelector('#catalogus ul');

      flushMicrotasks(); //execute service
      fixture.detectChanges(); //refresh view with new model
      
      let item : Element = list.children[0];

      expect(item).toBeDefined;
      expect(item.querySelector('h3').textContent).toBe('naam1');
      expect(item.querySelector('span.prijs').textContent).toBe('€12,99');
      expect(item.querySelector('p').textContent).toBe('beschrijving1');
    }));

    it('should show the naam, prijs, beschrijving for the second artikel', fakeAsync(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

      let compiled = fixture.debugElement.nativeElement;
      let list : HTMLUListElement = compiled.querySelector('#catalogus ul');

      flushMicrotasks(); //execute service
      fixture.detectChanges(); //refresh view with new model
      
      let item : Element = list.children[1];

      expect(item).toBeDefined;
      expect(item.querySelector('h3').textContent).toBe('naam3');
      expect(item.querySelector('span.prijs').textContent).toBe('€15');
      expect(item.querySelector('p').textContent).toBe('beschrijving3');
    }));

    afterEach(async(inject( [MockBackend], (MockBackend : MockBackend) => {
      MockBackend.verifyNoPendingRequests();
    })));

    it('should show the naam, prijs, beschrijving for the third artikel', fakeAsync(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

      let compiled = fixture.debugElement.nativeElement;
      let list : HTMLUListElement = compiled.querySelector('#catalogus ul');

      flushMicrotasks(); //execute service
      fixture.detectChanges(); //refresh view with new model
      
      let item : Element = list.children[2];

      expect(item).toBeDefined;
      expect(item.querySelector('h3').textContent).toBe('naam8');
      expect(item.querySelector('span.prijs').textContent).toBe('€12,60');
      expect(item.querySelector('p').textContent).toBe('beschrijving8');
    }));

    it('should show the naam, prijs, beschrijving for the fourth artikel', fakeAsync(() => {
      fixture = TestBed.createComponent(CatalogusComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

      let compiled = fixture.debugElement.nativeElement;
      let list : HTMLUListElement = compiled.querySelector('#catalogus ul');

      flushMicrotasks(); //execute service
      fixture.detectChanges(); //refresh view with new model
      
      let item : Element = list.children[3];

      expect(item).toBeDefined;
      expect(item.querySelector('h3').textContent).toBe('naam2');
      expect(item.querySelector('span.prijs').textContent).toBe('€1.000');
      expect(item.querySelector('p').textContent).toBe('beschrijving2');
    }));

    afterEach(async(inject( [MockBackend], (MockBackend : MockBackend) => {
      MockBackend.verifyNoPendingRequests();
    })));
  });
});
