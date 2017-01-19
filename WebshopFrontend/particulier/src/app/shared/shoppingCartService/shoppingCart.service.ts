import { Injectable } from '@angular/core';
import { IProductPair } from './productPair.interface';
import { Subject } from 'rxjs/Subject';

const WINKELMANDJE = 'winkelmandje';

@Injectable()
export class shoppingCartService {

    private newAmountOfProductsSource = new Subject();
    newAmountOfProducts = this.newAmountOfProductsSource.asObservable();

    getProducts() : IProductPair[] {
        if(window.localStorage[WINKELMANDJE]){
            return JSON.parse(window.localStorage[WINKELMANDJE]);
        }
        return [];
    }

    getProduct(productId : number) : number {
        if(window.localStorage[WINKELMANDJE]){
            let storage : IProductPair[] = JSON.parse(window.localStorage[WINKELMANDJE]);
            let index = storage.findIndex(elem => elem.productId === productId);
            if(index >= 0){
                return storage[index].amount;
            }
        }
        return 0;
    }

    addProduct(productId : number){
        let storage : IProductPair[] = [];

        if(window.localStorage[WINKELMANDJE]){
            storage = JSON.parse(window.localStorage[WINKELMANDJE]);
        }
        let index = storage.findIndex(elem => elem.productId === productId)
        if(index < 0){
            index = storage.push( { 
                productId: productId,
                amount: 0
            }) - 1;
        }    
        ++storage[index].amount;
        window.localStorage[WINKELMANDJE] = JSON.stringify(storage);
        this.newAmountOfProductsSource.next();
    } 

    amountOfProducts() : number {
        if(window.localStorage[WINKELMANDJE]){
            let storage : IProductPair[] = JSON.parse(window.localStorage[WINKELMANDJE]);
            let total = 0;
            storage.forEach(productType => {
                total += productType.amount;
            });
            return total;
        }
        return 0;
    }
}

