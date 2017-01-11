import { ParticulierPage } from './app.po';

describe('particulier App', function() {
  let page: ParticulierPage;

  beforeEach(() => {
    page = new ParticulierPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
