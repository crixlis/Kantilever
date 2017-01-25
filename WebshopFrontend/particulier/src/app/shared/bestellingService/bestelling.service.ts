import { Injectable } from '@angular/core';
import { Http, Response, Headers }    from '@angular/http';
import { Bestelling } from '.';
import 'rxjs/add/operator/toPromise'; //needed for toPromise to work.

import { environment } from './../../../environments/environment';

const BESTELLINGSERVICEURL = environment.apiBaseUrl + '/api/Bestellingen';

@Injectable()
export class BestellingService {
    
    constructor(private http: Http) { }

    getBestelling(): Promise<Bestelling[]> {
        return this.http.get(BESTELLINGSERVICEURL)
                .toPromise()
                .then(response => response.json().data as Bestelling[])
                .catch(error => Promise.reject(error.message || error));
    }

    postBestelling(bestelling : Bestelling): Promise<Bestelling> {
            let header = new Headers();
                header.append('Content-Type', 'application/json');

        return this.http.post(BESTELLINGSERVICEURL, bestelling.toJSON(), { headers: header })
                .toPromise()
                .then(response => response.json().data)
                .catch(error => Promise.reject(error.message || error));
    }
}