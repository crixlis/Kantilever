import { Injectable } from '@angular/core';
import { Http, Response }    from '@angular/http';
import { Bestelling } from '.';
import 'rxjs/add/operator/toPromise'; //needed for toPromise to work.

const WEBSHOPAPIURL = 'http://localhost:23284/';

@Injectable()
export class BestellingAPIService {
    
    constructor(private http: Http) { }

    getBestelling(): Promise<Bestelling[]> {
    return this.http.get(WEBSHOPAPIURL)
                .toPromise()
                .then(response => response.json().data as Bestelling[])
                .catch(error => Promise.reject(error.message || error));
    }

}