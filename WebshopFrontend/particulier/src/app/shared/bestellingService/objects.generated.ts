/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v8.6.6221.23503 (NJsonSchema v7.6.6221.22528) (http://NSwag.org)
// </auto-generated>
//----------------------


export class Artikel { 
    id: number; 
    naam: string; 
    beschrijving: string; 
    prijs: number; 
    leverbaarVanaf: Date; 
    leverbaarTot: Date; 
    leverancier: string; 
    categorieen: string[];

    constructor(data?: any) {
        if (data !== undefined) {
            this.id = data["id"] !== undefined ? data["id"] : null;
            this.naam = data["naam"] !== undefined ? data["naam"] : null;
            this.beschrijving = data["beschrijving"] !== undefined ? data["beschrijving"] : null;
            this.prijs = data["prijs"] !== undefined ? data["prijs"] : null;
            this.leverbaarVanaf = data["leverbaarVanaf"] ? new Date(data["leverbaarVanaf"].toString()) : null;
            this.leverbaarTot = data["leverbaarTot"] ? new Date(data["leverbaarTot"].toString()) : null;
            this.leverancier = data["leverancier"] !== undefined ? data["leverancier"] : null;
            if (data["categorieen"] && data["categorieen"].constructor === Array) {
                this.categorieen = [];
                for (let item of data["categorieen"])
                    this.categorieen.push(item);
            }
        }
    }

    static fromJS(data: any): Artikel {
        return new Artikel(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["id"] = this.id !== undefined ? this.id : null;
        data["naam"] = this.naam !== undefined ? this.naam : null;
        data["beschrijving"] = this.beschrijving !== undefined ? this.beschrijving : null;
        data["prijs"] = this.prijs !== undefined ? this.prijs : null;
        data["leverbaarVanaf"] = this.leverbaarVanaf ? this.leverbaarVanaf.toISOString() : null;
        data["leverbaarTot"] = this.leverbaarTot ? this.leverbaarTot.toISOString() : null;
        data["leverancier"] = this.leverancier !== undefined ? this.leverancier : null;
        if (this.categorieen && this.categorieen.constructor === Array) {
            data["categorieen"] = [];
            for (let item of this.categorieen)
                data["categorieen"].push(item);
        }
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Artikel(JSON.parse(json));
    }
}

export class Bestelling { 
    artikelen: Artikel[]; 
    id: number; 
    klant: Klant;

    constructor(data?: any) {
        if (data !== undefined) {
            if (data["artikelen"] && data["artikelen"].constructor === Array) {
                this.artikelen = [];
                for (let item of data["artikelen"])
                    this.artikelen.push(Artikel.fromJS(item));
            }
            this.id = data["id"] !== undefined ? data["id"] : null;
            this.klant = data["klant"] ? Klant.fromJS(data["klant"]) : null;
        }
    }

    static fromJS(data: any): Bestelling {
        return new Bestelling(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        if (this.artikelen && this.artikelen.constructor === Array) {
            data["artikelen"] = [];
            for (let item of this.artikelen)
                data["artikelen"].push(item.toJS());
        }
        data["id"] = this.id !== undefined ? this.id : null;
        data["klant"] = this.klant ? this.klant.toJS() : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Bestelling(JSON.parse(json));
    }
}

export class Klant { 
    acternaam: string; 
    adres: string; 
    id: number; 
    plaatsnaam: string; 
    postcode: string; 
    telefoonnummer: number; 
    voornaam: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.acternaam = data["acternaam"] !== undefined ? data["acternaam"] : null;
            this.adres = data["adres"] !== undefined ? data["adres"] : null;
            this.id = data["id"] !== undefined ? data["id"] : null;
            this.plaatsnaam = data["plaatsnaam"] !== undefined ? data["plaatsnaam"] : null;
            this.postcode = data["postcode"] !== undefined ? data["postcode"] : null;
            this.telefoonnummer = data["telefoonnummer"] !== undefined ? data["telefoonnummer"] : null;
            this.voornaam = data["voornaam"] !== undefined ? data["voornaam"] : null;
        }
    }

    static fromJS(data: any): Klant {
        return new Klant(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["acternaam"] = this.acternaam !== undefined ? this.acternaam : null;
        data["adres"] = this.adres !== undefined ? this.adres : null;
        data["id"] = this.id !== undefined ? this.id : null;
        data["plaatsnaam"] = this.plaatsnaam !== undefined ? this.plaatsnaam : null;
        data["postcode"] = this.postcode !== undefined ? this.postcode : null;
        data["telefoonnummer"] = this.telefoonnummer !== undefined ? this.telefoonnummer : null;
        data["voornaam"] = this.voornaam !== undefined ? this.voornaam : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Klant(JSON.parse(json));
    }
}