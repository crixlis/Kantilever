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
  TotalCosts : number = 0;
  Error : string = "";

  ngOnInit() {
    this.getProductsAndAmounts();
  }

  getProductsAndAmounts(){

    var shoppingcart : IProductPair[] = this.shoppingCart.getProducts();

    shoppingcart.forEach(artikel => {
      this.artikelService.getArtikel(artikel.productId).then(a => 
        {
            this.ProductPairs.push(new ProductPair(a, artikel.amount));
            this.TotalCosts += parseFloat((a.prijs*artikel.amount).toFixed(4));
        }
        ,error => {
          this.Error = "Er lijkt een probleem te zijn met de connectie. Probeer de pagina te verversen of probeer het later nog eens."
        });
    });
  }

  goToProductPage(productId : number) {
    window.location.href = 'product/' + productId;
  }
}
