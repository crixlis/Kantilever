import { Injectable } from '@angular/core';
import { Http, Response }    from '@angular/http';
import 'rxjs/add/operator/toPromise'; //needed for toPromise to work.
import { Artikel } from './../objects.generated';

import { environment } from './../../../environments/environment';

const ARTIKELURL = environment.apiBaseUrl + '/api/Artikel';

@Injectable()
export class ArtikelService {
    
    constructor(private http: Http) { }

    getArtikelen(): Promise<Artikel[]> {
    return this.http.get(ARTIKELURL)
                .toPromise()
                .then(response => response.json())
                .catch(error => Promise.reject(error.message || error));
    }

    getArtikel(artikelId : number): Promise<Artikel> {
    return this.http.get(ARTIKELURL + '/' + artikelId)
                .toPromise()
                .then(response => response.json())
                .catch(response => console.log(response))
    }
}