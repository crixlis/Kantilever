import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { shoppingCartService } from './../shared'

@Component({
  selector: 'appCatalogus',
  templateUrl: './catalogus.component.html',
  styleUrls: ['./catalogus.component.css']
})
export class CatalogusComponent implements OnInit {

  constructor(shoppingCart : shoppingCartService) {
    this._shoppingCart = shoppingCart;
  }

  _shoppingCart : shoppingCartService;

  @Output() onNewAmountProducts = new EventEmitter();

  ngOnInit() {
    //this.amountOfProducts = this._shoppingCart.amountOfProducts();
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
