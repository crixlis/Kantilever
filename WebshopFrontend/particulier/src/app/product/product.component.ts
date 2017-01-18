import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'appProduct',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  private _sub: any;
  public productId: number;

  public productTitel: string;
  public productOmschrijving: string;
  public productPrijs: number;
  public productLeverbaarVanaf: Date;
  public productLeverbaarTot: string;
  public productLeverancierCode: string;
  public productLeverancier: string;
  public productCatagorieen: string[];

  constructor(private route: ActivatedRoute) {}

  _shoppingCart : { [productId: number] : number};


  ngOnInit() {
     this._sub = this.route.params.subscribe(params => {
       this.productId = +params['id']; // (+) converts string 'id' to a number
    });

    //
    if(window.localStorage['winkelmandje'] === undefined)
    {
      window.localStorage['winkelmandje'] = this._shoppingCart;
    }
    //


    //Data ophalen, nu eerst mock data tot we een GET request hebben
    this.productTitel = 'Altec Manta - Stadsfiets - Mannen - Zwart - 61 cm';
    this.productOmschrijving = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero quo, dolor ut eius accusantium repellat consequatur, dignissimos error in adipisci, sit placeat minima, harum dicta nam magnam expedita obcaecati. Iste veritatis adipisci tempore voluptatum, sit quibusdam, natus reiciendis repellendus tempora! Quam temporibus velit ullam nisi recusandae, asperiores mollitia voluptatem quo.';
    this.productPrijs = 599;
    this.productLeverancier = 'Altec Manta';
    this.productCatagorieen = ['Fietsen', 'Stadsfietsen'];
    this.productLeverbaarTot = '20-02-2017';
  }

  ngOnDestroy() {
    this._sub.unsubscribe();
  }


  AddToShoppingCart() {
    this._shoppingCart[this.productId]++;
  }
  

}
