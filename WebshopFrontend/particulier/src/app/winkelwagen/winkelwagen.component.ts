import { Component, OnInit } from '@angular/core';
import { PrijsPipe, ShoppingCartService, ArtikelService, Artikel } from './../shared';

@Component({
  selector: 'appWinkelwagen',
  templateUrl: './winkelwagen.component.html',
  styleUrls: ['./winkelwagen.component.css']
})
export class WinkelwagenComponent implements OnInit {

  constructor(private shoppingCart : ShoppingCartService, private artikelService : ArtikelService) {
  }

  Artikelen : Artikel[];

  ngOnInit() {

    this.artikelService.getArtikelen().then(result => {
      this.Artikelen = result;
    }, error => console.log(error));
  
    
  }

}
