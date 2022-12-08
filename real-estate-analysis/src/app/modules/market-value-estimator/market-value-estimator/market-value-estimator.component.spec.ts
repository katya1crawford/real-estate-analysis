import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarketValueEstimatorComponent } from './market-value-estimator.component';

describe('MarketValueEstimatorComponent', () => {
  let component: MarketValueEstimatorComponent;
  let fixture: ComponentFixture<MarketValueEstimatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarketValueEstimatorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MarketValueEstimatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
