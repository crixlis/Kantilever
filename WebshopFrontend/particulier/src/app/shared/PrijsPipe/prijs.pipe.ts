import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'prijs'
})
export class PrijsPipe implements PipeTransform {

  transform(value: any, args?: any): any {

    if(value === undefined)
    {
      return "";
    }
    let prijs = String(value);
    let prijslengte = prijs.length;

    let prijsvoorkomma;
    let prijsachterkomma;

    if(value % 1 != 0)
    {
      let splitprijs = prijs.split('.');
      prijsvoorkomma = splitprijs[0];
      prijsachterkomma = splitprijs[1];

      if(prijsachterkomma.length == 1)
      {
        prijsachterkomma = prijsachterkomma + '0';
      }
    }
    else
    {
      prijsvoorkomma = prijs;
    }

    if(value >= 1000)
    {
      if(prijsvoorkomma.length >= 4)
      {
        let prijsvoorkomma1 = prijsvoorkomma.substring(0, prijsvoorkomma.length - 3);
        let prijsvoorkomma2 = prijsvoorkomma.substring(prijsvoorkomma.length -3, prijsvoorkomma.length)
        prijs = prijsvoorkomma1 + '.' + prijsvoorkomma2;
      }
    } else {
      prijs = prijsvoorkomma;
    }

    if(prijsachterkomma != null)
      {
        prijs = prijs + ',' + prijsachterkomma;
      }

    return '€' + prijs;
  }

}
