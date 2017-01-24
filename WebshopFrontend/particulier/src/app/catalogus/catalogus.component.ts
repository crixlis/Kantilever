import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared'

@Component({
  selector: 'appCatalogus',
  templateUrl: './catalogus.component.html',
  styleUrls: ['./catalogus.component.css']
})
export class CatalogusComponent implements OnInit {

  constructor(private _shoppingCart : ShoppingCartService, private _artikelService : ArtikelService ) {
  }

  // _artikelenService : ArtikelenService;
  artikelen : Artikel[] = [];

  @Output() onNewAmountProducts = new EventEmitter();

  ngOnInit() {
    this._artikelService.getArtikelen().then(result => { this.artikelen = result; }, error => console.error(error) );
  }

  public addProductToCart(event: any, productId : number) {
    event.stopPropagation();
    event.preventDefault();
    this._shoppingCart.addProduct(productId);
    let amountOfProducts = this._shoppingCart.amountOfProducts();
    this.onNewAmountProducts.emit();
  }

  public goToProductPage(productId : number) {
    window.location.href = 'product/' + productId;
  }

}
