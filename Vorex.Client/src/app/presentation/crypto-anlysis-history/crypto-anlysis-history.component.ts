import {
  Component,
  ElementRef,
  inject,
  signal,
  ViewChild,
} from '@angular/core';
import { SearchBarComponent } from '../../shared/components/search-bar/search-bar.component';
import {
  ArrowUpDown,
  CalendarMinus2,
  ChevronDown,
  Funnel,
  LucideAngularModule,
  Trash2,
} from 'lucide-angular';
import { DropdownComponent } from '../../shared/components/dropdown/dropdown.component';
import { DatePipe, JsonPipe, NgClass } from '@angular/common';
import { ICryptoHistoryTableColumn } from '../crypto-anlysis/types/crypto.type';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { GetPaginatedUserCryptoAnlysisHistoryUseCase } from '../../application/users/use-cases/getPaginatedUserCryptoAnlysisHistory.usecase';
import {
  IDatePaginatedInput,
  IPaginatedResponse,
} from '../../shared/types/shared.types';
import { HumanizeDaysPipe } from '../../shared/pipes/humanize-days.pipe';
import { EmptyResultComponent } from '../../shared/components/empty-result/empty-result.component';
import { filterHistoryFormControls } from '../crypto-anlysis/types/user.type';
import { ICryptoAnylysisHistory } from '../../application/users/models/user.model';
import { PaginatorComponent } from '../../shared/components/paginator/paginator.component';
import { DeleteCryptoAnlysisHistoryModalComponent } from './delete-crypto-anlysis-history-modal/delete-crypto-anlysis-history-modal.component';
import { ToastrService } from '../../shared/services/toastr.service';

@Component({
  selector: 'app-crypto-anlysis-history',
  imports: [
    SearchBarComponent,
    LucideAngularModule,
    DropdownComponent,
    NgClass,
    FormsModule,
    DatePipe,
    HumanizeDaysPipe,
    EmptyResultComponent,
    ReactiveFormsModule,
    PaginatorComponent,
    DeleteCryptoAnlysisHistoryModalComponent,
  ],
  templateUrl: './crypto-anlysis-history.component.html',
  styleUrl: './crypto-anlysis-history.component.scss',
})
export class CryptoAnlysisHistoryComponent {
  readonly Trash2 = Trash2;
  readonly Funnel = Funnel;
  readonly ChevronDown = ChevronDown;
  readonly CalendarMinus2 = CalendarMinus2;
  readonly ArrowUpDown = ArrowUpDown;

  cryptoHistoryTableColumns: ICryptoHistoryTableColumn[];
  cryptoHistoryTableData?: IPaginatedResponse<ICryptoAnylysisHistory[]>;
  isAllRecordsChecked = false;
  numberOfCheckedRecords = 0;
  paginationInput: IDatePaginatedInput;
  searchText = '';
  filterHistoryForm!: FormGroup<filterHistoryFormControls>;

  isFilterDropDownOpen = signal(false);

  // services
  getAllUserCryptoAnlysisHistoryUseCase = inject(
    GetPaginatedUserCryptoAnlysisHistoryUseCase
  );

  toastService = inject(ToastrService);

  constructor() {
    this.cryptoHistoryTableColumns = [
      { id: 1, name: 'Crypto Name', isAsc: true, key: 'cryptoName' },
      { id: 2, name: 'Amout', isAsc: true, key: 'investmentAmount' },
      { id: 3, name: 'Holding Time', isAsc: true, key: 'holdingDays' },
      { id: 4, name: 'ROI', isAsc: true, key: 'risk' },
      { id: 5, name: 'Submit Date', isAsc: true, key: 'submitDate' },
    ];

    this.paginationInput = {
      PageIndex: 1,
      PageSize: 10,
      SearchValue: '',
    };
  }

  ngOnInit() {
    this.initializeFilterForm();
    this.fetchUserCryptoAnlysisHistory();
  }

  initializeFilterForm() {
    this.filterHistoryForm = new FormGroup<filterHistoryFormControls>({
      startDate: new FormControl<Date | null>(null),
      endDate: new FormControl<Date | null>(null),
    });
  }

  fetchUserCryptoAnlysisHistory() {
    this.getAllUserCryptoAnlysisHistoryUseCase
      .execute(this.paginationInput)
      .subscribe({
        next: (resp) => {
          this.cryptoHistoryTableData = resp;
          this.numberOfCheckedRecords = 0;
          this.isAllRecordsChecked = false;
        },
        error: (err) => console.log(err),
      });
  }

  toggleFilter = () =>
    this.isFilterDropDownOpen.set(!this.isFilterDropDownOpen());

  toggleRecordCheck = (id: string) => {
    let record = this.cryptoHistoryTableData?.data.find((x) => x.id === id);
    if (!record) return;
    record.isChecked = !record.isChecked;

    if (record.isChecked) {
      this.incrementNumberOfCheckedRecords();
      return;
    }
    this.decrementNumberOfCheckedRecords();
  };

  toggleAllRecordsCheckTo(isCheck: boolean) {
    if (!this.cryptoHistoryTableData) return;
    this.cryptoHistoryTableData.data = this.cryptoHistoryTableData?.data.map(
      (x) => ({
        ...x,
        isChecked: isCheck,
      })
    );

    if (isCheck) {
      this.numberOfCheckedRecords = this.cryptoHistoryTableData.data.length;
      return;
    }
    this.numberOfCheckedRecords = 0;
  }

  isAllRecoredChecked = () =>
    this.cryptoHistoryTableData?.data.every((x) => x.isChecked);

  changeAllRecordsCheckedStatus = () => {
    if (this.isAllRecoredChecked()) {
      this.isAllRecordsChecked = true;
      return;
    }

    this.isAllRecordsChecked = false;
  };

  incrementNumberOfCheckedRecords = () => this.numberOfCheckedRecords++;

  decrementNumberOfCheckedRecords = () => this.numberOfCheckedRecords--;

  updatePaginationInputSearchValue() {
    this.paginationInput.SearchValue = this.searchText;
  }

  updatePaginationInputEndDate() {
    this.paginationInput.endDate =
      this.filterHistoryForm.value.endDate || undefined;
  }

  updatePaginationInputStartDate() {
    this.paginationInput.startDate =
      this.filterHistoryForm.value.startDate ?? undefined;
  }

  sortTableData(column: ICryptoHistoryTableColumn) {
    const col = this.cryptoHistoryTableColumns.find((x) => x.id === column.id);
    if (!col || !this.cryptoHistoryTableData) return;

    col.isAsc = !col.isAsc;

    this.cryptoHistoryTableData.data = [
      ...this.cryptoHistoryTableData.data,
    ].sort((a, b) => {
      const valA = a[col.key];
      const valB = b[col.key];

      if (valA < valB) return col.isAsc ? -1 : 1;
      if (valA > valB) return col.isAsc ? 1 : -1;
      return 0;
    });
  }

  updatePaginationInputPage(page: number) {
    this.paginationInput.PageIndex = page;
  }

  // delete crypto analysis
  @ViewChild(DeleteCryptoAnlysisHistoryModalComponent)
  deleteCryptoAnlysisHistoryModal!: DeleteCryptoAnlysisHistoryModalComponent;
  deleteCryptoAnalysisHistoryModalInput?: string[];
  openDeleteCryptoAnalysisHistoryModal() {
    setTimeout(() => {
      this.deleteCryptoAnlysisHistoryModal.model.show();
    }, 100);
  }

  setDeleteCryptoAnalysisHistoryModalInput() {
    this.deleteCryptoAnalysisHistoryModalInput =
      this.cryptoHistoryTableData!.data.filter((x) => x.isChecked).map(
        (x) => x.id
      );
  }

  @ViewChild('cryptoHistoryTable') cryptoHistoryTableEl?: ElementRef;

  scrollToTopOfHistoryTable() {
    window.scrollTo({
      top: this.cryptoHistoryTableEl?.nativeElement.offsetTop - 50,
      behavior: 'smooth',
    });
  }
}
