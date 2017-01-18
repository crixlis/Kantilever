import { Component, OnInit } from '@angular/core';
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

  public amountOfProducts : number;

  ngOnInit() {
    this.amountOfProducts = this._shoppingCart.amountOfProducts();
  }

  addProductToCart(productId : number) {
    this._shoppingCart.addProduct(productId);
    this.amountOfProducts = this._shoppingCart.amountOfProducts();
  }

}
