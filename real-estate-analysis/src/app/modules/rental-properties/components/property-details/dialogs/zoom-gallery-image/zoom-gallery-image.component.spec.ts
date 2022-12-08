import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZoomGalleryImageComponent } from './zoom-gallery-image.component';

describe('ZoomGalleryImageComponent', () => {
  let component: ZoomGalleryImageComponent;
  let fixture: ComponentFixture<ZoomGalleryImageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ZoomGalleryImageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZoomGalleryImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
