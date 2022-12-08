import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveFromFavoritesComponent } from './remove-from-favorites.component';

describe('RemoveFromFavoritesComponent', () => {
  let component: RemoveFromFavoritesComponent;
  let fixture: ComponentFixture<RemoveFromFavoritesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoveFromFavoritesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RemoveFromFavoritesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
