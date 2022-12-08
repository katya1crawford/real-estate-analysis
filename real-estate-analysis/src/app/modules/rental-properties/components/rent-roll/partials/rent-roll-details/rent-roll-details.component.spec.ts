import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentRollDetailsComponent } from './rent-roll-details.component';

describe('RentRollDetailsComponent', () => {
  let component: RentRollDetailsComponent;
  let fixture: ComponentFixture<RentRollDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RentRollDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RentRollDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
