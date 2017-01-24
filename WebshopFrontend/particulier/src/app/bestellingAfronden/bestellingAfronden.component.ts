import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared'

@Component({
  selector: 'bestellingAfronden',
  templateUrl: './bestellingAfronden.component.html',
  styleUrls: ['./bestellingAfronden.component.css']
})
export class BestellingAfrondenComponent implements OnInit {

  constructor(private _shoppingCart : ShoppingCartService, private _artikelService : ArtikelService ) {
  }

  // _artikelenService : ArtikelenService;
  artikelen : Artikel[] = [];
  error : string;

  @Output() onNewAmountProducts = new EventEmitter();

    ngOnInit() {
    }
}
