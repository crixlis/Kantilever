import { Component, OnInit } from '@angular/core';
import { PrijsPipe, ShoppingCartService, ArtikelService, Artikel, ProductPair, IProductPair } from './../shared';

@Component({
  selector: 'appWinkelwagen',
  templateUrl: './winkelwagen.component.html',
  styleUrls: ['./winkelwagen.component.css']
})
export class WinkelwagenComponent implements OnInit {

  constructor(private shoppingCart : ShoppingCartService, private artikelService : ArtikelService) {
  }

  ProductPairs : ProductPair[] = [];

  ngOnInit() {
    this.getProductsAndAmounts();
  }

  getProductsAndAmounts(){

    var shoppingcart : IProductPair[] = this.shoppingCart.getProducts();

    shoppingcart.forEach(artikel => {
      this.artikelService.getArtikel(artikel.productId).then(a => 
        {
            this.ProductPairs.push(new ProductPair(a, artikel.amount));
        }
        ,error => console.error(error));
    });

  }
}
