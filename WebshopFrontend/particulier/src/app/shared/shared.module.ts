import { NgModule, ModuleWithProviders, Class } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule} from "@angular/forms";
import { RouterModule } from '@angular/router';

import { ShoppingCartService, IProductPair } from './shoppingCartService';

/**
 * Do not specify providers for modules that might be imported by a lazy loaded module.
 */

@NgModule({
  imports: [CommonModule, RouterModule, FormsModule, ReactiveFormsModule],
  declarations: [],
  exports: []
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [ShoppingCartService]
    };
  }
}