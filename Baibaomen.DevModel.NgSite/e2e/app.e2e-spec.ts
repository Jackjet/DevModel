import { NgnewPage } from './app.po';

describe('ngnew App', () => {
  let page: NgnewPage;

  beforeEach(() => {
    page = new NgnewPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
