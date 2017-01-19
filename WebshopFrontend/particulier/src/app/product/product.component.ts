import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PrijsPipe, shoppingCartService } from './../shared';

@Component({
  selector: 'appProduct',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  private _sub: any;
  public addtoshoppingcarttext = 'Voeg toe aan winkelwagen';

  public productId: number;

  public productTitel: string;
  public productOmschrijving: string;
  public productPrijs: number;
  public productLeverbaarVanaf: Date;
  public productLeverbaarTot: string;
  public productLeverancierCode: string;
  public productLeverancier: string;
  public productCatagorieen: string[];

  constructor(private route: ActivatedRoute, private shoppingCart : shoppingCartService) {
  }


  ngOnInit() {
     this._sub = this.route.params.subscribe(params => {
       this.productId = +params['id']; // (+) converts string 'id' to a number
    });

    //
    //Data ophalen, nu eerst mock data tot we een GET request hebben
    this.productTitel = 'Altec Manta - Stadsfiets - Mannen - Zwart - 61 cm';
    this.productOmschrijving = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero quo, dolor ut eius accusantium repellat consequatur, dignissimos error in adipisci, sit placeat minima, harum dicta nam magnam expedita obcaecati. Iste veritatis adipisci tempore voluptatum, sit quibusdam, natus reiciendis repellendus tempora! Quam temporibus velit ullam nisi recusandae, asperiores mollitia voluptatem quo.';
    this.productPrijs = 12345.40;
    this.productLeverancier = 'Altec Manta';
    this.productCatagorieen = ['Fietsen', 'Stadsfietsen'];
    this.productLeverbaarTot = '20-02-2017';
  }

  ngOnDestroy() {
    this._sub.unsubscribe();
  }


  AddToShoppingCart() {
    this.shoppingCart.addProduct(this.productId);
    this.addtoshoppingcarttext = 'Product toegevoegd';
    window.setTimeout(() => {
      this.addtoshoppingcarttext = 'Voeg toe aan winkelwagen';
    }, 1200);
    
    
  }
  

}
