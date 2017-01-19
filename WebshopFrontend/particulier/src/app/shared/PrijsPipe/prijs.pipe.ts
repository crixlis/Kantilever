import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'prijs'
})
export class PrijsPipe implements PipeTransform {

  transform(value: any, args?: any): any {
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
      console.log('prijsachterkomma: ' + prijsachterkomma);
    }
    else
    {
      prijsvoorkomma = prijs;
    }
    console.log('prijsvoorkomma: ' + prijsvoorkomma);

    if(value >= 1000)
    {
      if(prijsvoorkomma.length >= 4)
      {
        console.log('prijsvoorkommma lengte: ' + prijsvoorkomma.length);
        let prijsvoorkomma1 = prijsvoorkomma.substring(0, prijsvoorkomma.length - 4);
        let prijsvoorkomma2 = prijsvoorkomma.substring(prijsvoorkomma.length -4, prijsvoorkomma.length)
        console.log('prijsvoorkomma1: ' + prijsvoorkomma1);
        console.log('prijsvoorkomma2: ' + prijsvoorkomma2);
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
