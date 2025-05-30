import {
  Component,
  inject,
  input,
  output,
  signal,
  ViewChild,
} from '@angular/core';
import { PopupComponent } from '../../../shared/components/popup/popup.component';
import { DeleteCryptoAnlysisHistoryRecordsUseCase } from '../../../application/users/use-cases/deleteCryptoAnlysisHistoryRecords.usecase';
import { IDeleteCryptoAnlysisHistoryRecordsInput } from '../../../application/users/models/user.model';
import { LoaderComponent } from '../../../shared/components/loader/loader.component';
import { ToastrService } from '../../../shared/services/toastr.service';

@Component({
  selector: 'app-delete-crypto-anlysis-history-modal',
  imports: [PopupComponent, LoaderComponent],
  templateUrl: './delete-crypto-anlysis-history-modal.component.html',
  styleUrl: './delete-crypto-anlysis-history-modal.component.scss',
})
export class DeleteCryptoAnlysisHistoryModalComponent {
  @ViewChild(PopupComponent) model!: PopupComponent;
  recordsToDelete = input.required<string[]>();
  recordsDeleted = output<string[]>();
  isDeleteRequestLoading = signal(false);

  //services
  private _deleteCryptoAnalysisHistoryRecordsUseCase = inject(
    DeleteCryptoAnlysisHistoryRecordsUseCase
  );
  toasterService = inject(ToastrService);
  constructor() {}

  deleteCryptoAnalysisHistoryRecords() {
    if (this.isDeleteRequestLoading()) return;

    let input = {
      ids: this.recordsToDelete(),
    } as IDeleteCryptoAnlysisHistoryRecordsInput;

    this.isDeleteRequestLoading.set(true);
    this._deleteCryptoAnalysisHistoryRecordsUseCase.execute(input).subscribe({
      next: () => {
        this.isDeleteRequestLoading.set(false);
        this.toasterService.success(
          'Records deleted successfully',
          `${this.recordsToDelete().length} records deleted`
        );
        this.model.hide();
        this.recordsDeleted.emit(this.recordsToDelete());
      },
      error: (err) => {
        this.isDeleteRequestLoading.set(false);
        this.model.hide();
      },
    });
  }
}
