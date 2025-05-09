import { HumanizeDaysPipe } from './humanize-days.pipe';

describe('HumanizeDaysPipe', () => {
  it('create an instance', () => {
    const pipe = new HumanizeDaysPipe();
    expect(pipe).toBeTruthy();
  });
});
