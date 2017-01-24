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

  productPairs : ProductPair[] = [];
  totalCosts : number = 0;
  error : string = "";

  ngOnInit() {
    this.getProductsAndAmounts();
  }

  getProductsAndAmounts(){

    var shoppingcart : IProductPair[] = this.shoppingCart.getProducts();

    shoppingcart.forEach(artikel => {
      this.artikelService.getArtikel(artikel.productId).then(a => 
        {
          this.productPairs.push(new ProductPair(a, artikel.amount));
          this.totalCosts += parseFloat((a.prijs*artikel.amount).toFixed(4));
        },error => {
          console.error(error);
          this.error = "Er lijkt een probleem te zijn met de connectie. Probeer de pagina te verversen of probeer het later nog eens."
        });
    });
  }

  goToProductPage(productId : number) {
    window.location.href = 'product/' + productId;
  }
}
