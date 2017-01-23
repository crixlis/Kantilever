/* tslint:disable:no-unused-variable */

import { TestBed, async, expect, it } from '@angular/core/testing';
import { PrijsPipe } from './prijs.pipe';

describe('PrijsPipe', () => {
  it('create an instance', () => {
    let pipe = new PrijsPipe();
    expect(pipe).toBeTruthy();
  });

  it('xxx', () => {
    let pipe = new PrijsPipe();
    except(pipe.transform(12.00)).toBe('12');
  });
});
