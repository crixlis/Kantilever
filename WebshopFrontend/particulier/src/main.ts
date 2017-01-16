import './polyfills.ts';

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';
import { environment } from './environments/environment';
import { AppModule } from './app/';
import { PageNotFoundModule } from './app/pageNotFound';
import { ProductModule } from './app/product';
import { WinkelwagenModule } from './app/winkelwagen';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule);
