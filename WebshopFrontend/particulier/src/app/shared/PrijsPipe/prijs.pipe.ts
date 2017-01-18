import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'prijs'
})
export class PrijsPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    let prijs = String(value);
    let prijslengte = prijs.length;
    if(value % 1 != 0)
    {
        //Prijs heeft decimalen
        if(prijs.substring(prijslengte -3, 1) == '.')
        {
          prijs.replace('.',',')
        }
    }

    if(value >= 1000)
    {
      let getallenvoorkomma = value % 1 != 0 ? prijs.split(',')[0] : prijs;
      if(getallenvoorkomma.length >= 4)
      {
        let getal1 = getallenvoorkomma.substring(0,getallenvoorkomma.length -3);
        let getal2 = getallenvoorkomma.substring(getallenvoorkomma.length -3, 3);
      }
      

    }

    return '€' + prijs;
  }

}
