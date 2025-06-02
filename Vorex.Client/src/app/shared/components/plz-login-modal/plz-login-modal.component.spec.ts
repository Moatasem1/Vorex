import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlzLoginModalComponent } from './plz-login-modal.component';

describe('PlzLoginModalComponent', () => {
  let component: PlzLoginModalComponent;
  let fixture: ComponentFixture<PlzLoginModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlzLoginModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlzLoginModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
