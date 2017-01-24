import { Artikel } from './../objects.generated';

export class ProductPair {
  public Artikel : Artikel;
  public Amount: Number;

  constructor(artikel : Artikel, amount: Number) {
    this.Artikel = artikel,
    this.Amount = amount
  }
}