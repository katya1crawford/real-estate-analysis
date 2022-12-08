import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaseCountByMonthComponent } from './lease-count-by-month.component';

describe('LeaseCountByMonthComponent', () => {
  let component: LeaseCountByMonthComponent;
  let fixture: ComponentFixture<LeaseCountByMonthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeaseCountByMonthComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaseCountByMonthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
