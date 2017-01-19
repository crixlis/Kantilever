import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ShoppingCartService } from './../shared'
import { Artikel, PrijsPipe } from './../shared'

@Component({
  selector: 'appCatalogus',
  templateUrl: './catalogus.component.html',
  styleUrls: ['./catalogus.component.css']
})
export class CatalogusComponent implements OnInit {

  constructor(shoppingCart : ShoppingCartService) {
    this._shoppingCart = shoppingCart;
  }

  _shoppingCart : ShoppingCartService;
  // _artikelenService : ArtikelenService;
  artikelen : Artikel[];

  @Output() onNewAmountProducts = new EventEmitter();

  ngOnInit() {
    //this.amountOfProducts = this._shoppingCart.amountOfProducts();
    this.artikelen = [new Artikel(
      {
        id: 1,
        naam: "Fietsband",
        beschrijving: "voor 26\" velg",
        prijs: 6.00,
        leverbaarVanaf: null,
        leverbaarTot: null,
        leverancier: 'Piet Fietsenbouwer',
        categorieen: 'Fietsband'
      }
    ), new Artikel(
      {
        id: 1,
        naam: "Fietsband",
        beschrijving: "voor 24\" velg",
        prijs: 5.50,
        leverbaarVanaf: null,
        leverbaarTot: null,
        leverancier: 'Piet Fietsenbouwer',
        categorieen: 'Fietsband'
      }
    )];
    //_artikelen = this._artikelenService.getArtikelen(); 
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
