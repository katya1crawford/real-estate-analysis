import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteNeighborhoodComponent } from './delete-neighborhood.component';

describe('DeleteNeighborhoodComponent', () => {
  let component: DeleteNeighborhoodComponent;
  let fixture: ComponentFixture<DeleteNeighborhoodComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteNeighborhoodComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteNeighborhoodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
