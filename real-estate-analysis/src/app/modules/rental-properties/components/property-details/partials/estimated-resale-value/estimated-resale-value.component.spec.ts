import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstimatedResaleValueComponent } from './estimated-resale-value.component';

describe('EstimatedResaleValueComponent', () => {
  let component: EstimatedResaleValueComponent;
  let fixture: ComponentFixture<EstimatedResaleValueComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EstimatedResaleValueComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EstimatedResaleValueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
