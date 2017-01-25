import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Http } from '@angular/http';
import { ShoppingCartService, ArtikelService, BestellingService, Artikel, PrijsPipe, Klant, Bestelling, IProductPair } from './../shared';

@Component({
  moduleId: module.id,
  selector: 'bestellingAfronden',
  providers: [],
  templateUrl: './bestellingAfronden.component.html',
  styleUrls: ['./bestellingAfronden.component.css']
})
export class BestellingAfrondenComponent implements OnInit {

  constructor(
    private _shoppingCart : ShoppingCartService, 
    private _artikelService : ArtikelService,
    private _bestellingenService : BestellingService) {}

  error : string;


  klant: Klant =  new Klant;

  @Output() onNewAmountProducts = new EventEmitter();

    ngOnInit() {
    }

    bestellingAfronden(){
      let bestelling : Bestelling =  new Bestelling;

      bestelling.id = 0;
      bestelling.klant = this.klant;
      //bestelling.artikelen : IProductPair[] = this._shoppingCart.getProducts();

      console.log(bestelling)

      this._bestellingenService.postBestelling(bestelling);
    }
}
