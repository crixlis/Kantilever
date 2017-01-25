import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Http } from '@angular/http';
import { ShoppingCartService, ArtikelService, BestellingService, BestellingArtikel, PrijsPipe, Klant, Bestelling, IProductPair } from './../shared';

@Component({
  selector: 'bestellingAfronden',
  providers: [],
  templateUrl: './bestellingAfronden.component.html',
  styleUrls: ['./bestellingAfronden.component.css']
})
export class BestellingAfrondenComponent implements OnInit {

  constructor(
    private _shoppingCart : ShoppingCartService, 
    private _artikelService : ArtikelService,
    private _bestellingService : BestellingService) {}

  error : string;
  klant: Klant =  new Klant;

  @Output() onNewAmountProducts = new EventEmitter();

    ngOnInit() {
      this.klant.id = 0;
    }

    bestellingAfronden(){
      let bestelling : Bestelling =  new Bestelling;
      let productpairs : IProductPair[] = this._shoppingCart.getProducts();
      let artikelen : BestellingArtikel[] = [];

      productpairs.forEach(a => {
        let artikel = new BestellingArtikel();
        artikel.aantal = a.amount;
        artikel.id = a.productId
        artikelen.push(artikel);
      });

      bestelling.id = 0;
      bestelling.artikelen = artikelen;
      bestelling.klant = this.klant;

      console.log(bestelling);
      this._bestellingService.postBestelling(bestelling);
    }
}
