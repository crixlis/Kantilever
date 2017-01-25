import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ShoppingCartService, ArtikelService, Artikel, PrijsPipe } from './../shared';
/*import { FormGroup, Validators, FormBuilder } from '@angular/forms';*/
import { Http } from '@angular/http';

@Component({
  selector: 'bestellingAfronden',
  templateUrl: './bestellingAfronden.component.html',
  styleUrls: ['./bestellingAfronden.component.css']
})
export class BestellingAfrondenComponent implements OnInit {

  constructor(private _shoppingCart : ShoppingCartService, private _artikelService : ArtikelService
  /*, private http: Http,
		private fb: FormBuilder*/
    ) {
  }

  artikelen : Artikel[] = [];
  error : string;
  /*bestellingAfrondenForm : FormGroup;*/

  @Output() onNewAmountProducts = new EventEmitter();

    ngOnInit() {
       /*this.bestellingAfrondenForm = this.fb.group({

      });*/
    }

    bestellingAfronden(){
      console.log("test");
    }
}
