import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractRentByMonthComponent } from './contract-rent-by-month.component';

describe('ContractRentByMonthComponent', () => {
  let component: ContractRentByMonthComponent;
  let fixture: ComponentFixture<ContractRentByMonthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContractRentByMonthComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContractRentByMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
