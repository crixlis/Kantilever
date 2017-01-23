/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { PrijsPipe } from './prijs.pipe';

describe('PrijsPipe', () => {
  it('create an instance', () => {
    let pipe = new PrijsPipe();
    expect(pipe).toBeTruthy();
  });

  it('price pipe should display 12 euro as \'€12\'', () => {
    let pipe = new PrijsPipe();
    expect(pipe.transform(12.00)).toBe('€12');
  });

  it('price pipe should display 12.99 euro as \'€12,99\'', () => {
    let pipe = new PrijsPipe();
    expect(pipe.transform(12.99)).toBe('€12,99');
  });

  it('price pipe should display 12000.00 euro as \'€12.000\'', () => {
    let pipe = new PrijsPipe();
    expect(pipe.transform(12000.00)).toBe('€12.000');
  });

  it('price pipe should display 12999.99 euro as \'€12.999,99\'', () => {
    let pipe = new PrijsPipe();
    expect(pipe.transform(12999.99)).toBe('€12.999,99');
  });
});
