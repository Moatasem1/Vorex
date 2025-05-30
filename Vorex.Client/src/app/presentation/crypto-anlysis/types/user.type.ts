import { FormControl } from '@angular/forms';

export interface filterHistoryFormControls {
  startDate: FormControl<Date | null>;
  endDate: FormControl<Date | null>;
}
