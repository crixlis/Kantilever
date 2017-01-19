import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PrijsPipe, ShoppingCartService, ArtikelService, Artikel } from './../shared';

@Component({
  selector: 'appProduct',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  @Input() artikel : Artikel = new Artikel();

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
  public productVoorraad: number;

  constructor(private route: ActivatedRoute, private shoppingCart : ShoppingCartService, private artikelService : ArtikelService) {
  }


  ngOnInit() {
     this._sub = this.route.params.subscribe(params => {
       this.productId = +params['id']; // (+) converts string 'id' to a number

       //Data ophalen van Web API
       this.artikelService.getArtikel(this.productId).then(result => { 
         this.artikel = Artikel.fromJS(result); 
        }, error => console.error(error) );

       //Data ophalen, nu eerst mock data tot we een GET request hebben
      this.productTitel = 'Altec Manta - Stadsfiets - Mannen - Zwart - 61 cm';
      this.productOmschrijving = 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Libero quo, dolor ut eius accusantium repellat consequatur, dignissimos error in adipisci, sit placeat minima, harum dicta nam magnam expedita obcaecati. Iste veritatis adipisci tempore voluptatum, sit quibusdam, natus reiciendis repellendus tempora! Quam temporibus velit ullam nisi recusandae, asperiores mollitia voluptatem quo.';
      this.productPrijs = 12345.40;
      this.productLeverancier = 'Altec Manta';
      this.productCatagorieen = ['Fietsen', 'Stadsfietsen'];
      this.productLeverbaarTot = '20-02-2017';
      this.productVoorraad = 6;
    });

    //
    
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
