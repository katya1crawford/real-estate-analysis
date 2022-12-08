import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNeighborhoodComponent } from './add-neighborhood.component';

describe('AddNeighborhoodComponent', () => {
  let component: AddNeighborhoodComponent;
  let fixture: ComponentFixture<AddNeighborhoodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNeighborhoodComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddNeighborhoodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
