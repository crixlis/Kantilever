import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from './shared'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

export class AppComponent {

  _shoppingCart : ShoppingCartService;
  amountOfProducts : number;

  constructor(shoppingCart : ShoppingCartService) {
    this._shoppingCart = shoppingCart;
  }

  ngOnInit() {
    this.amountOfProducts = this._shoppingCart.amountOfProducts();
    this._shoppingCart.newAmountOfProducts.subscribe(() => {
      this.amountOfProducts = this._shoppingCart.amountOfProducts();
    });
  }

  ToggleSideNav() {
    var sidebar = document.querySelector('#sidebar');

    if(sidebar.className == '' || !sidebar.className)
    {
      sidebar.className = 'active';
    }
    else
    { 
      sidebar.className = '';
    }
  }

  onNewAmountProducts(){
    this.amountOfProducts = this._shoppingCart.amountOfProducts();
  }
  
}
