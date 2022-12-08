import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteGalleryImageComponent } from './delete-gallery-image.component';

describe('DeleteGalleryImageComponent', () => {
  let component: DeleteGalleryImageComponent;
  let fixture: ComponentFixture<DeleteGalleryImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteGalleryImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteGalleryImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
