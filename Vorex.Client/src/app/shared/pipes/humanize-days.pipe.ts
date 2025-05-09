import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'humanizeDays',
})
export class HumanizeDaysPipe implements PipeTransform {
  transform(days: number): string {
    let years = Math.floor(days / 365);
    let months = Math.floor((days - years * 365) / 30);
    let weeks = Math.floor((days - years * 365 - months * 30) / 7);
    let daysLeft = days - years * 365 - months * 30 - weeks * 7;

    let yearString = years > 0 ? `${years} year${years !== 1 ? 's' : ''} ` : '';
    let monthString =
      months > 0 ? `${months} month${months !== 1 ? 's' : ''} ` : '';
    let weekString = weeks > 0 ? `${weeks} week${weeks !== 1 ? 's' : ''} ` : '';
    let dayString =
      daysLeft > 0 ? `${daysLeft} day${daysLeft !== 1 ? 's' : ''} ` : '';

    let period = [yearString, monthString, weekString, dayString];

    period = period.filter((item) => item !== '');

    let finalResult = period.join(' and ');

    return finalResult.trim();
  }
}
