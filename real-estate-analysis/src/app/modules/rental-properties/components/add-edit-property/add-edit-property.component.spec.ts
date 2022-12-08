import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditNewPropertyComponent } from './add-edit-property.component';

describe('AddNewPropertyComponent', () => {
  let component: AddEditNewPropertyComponent;
  let fixture: ComponentFixture<AddEditNewPropertyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddEditNewPropertyComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(AddEditNewPropertyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
