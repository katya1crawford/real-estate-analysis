import { AfterViewInit, Component, ElementRef, Input, NgZone } from '@angular/core';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';
import { GoogleMapService } from 'src/app/shared/services/google-map.service';

@Component({
  selector: 'app-maps',
  template: '<ng-template></ng-template>'
})
export class MapsComponent implements AfterViewInit {
  constructor(private zone: NgZone, private element: ElementRef, private googleService: GoogleMapService) { }


  @Input() width: string;
  @Input() height: string;
  @Input() property: ReadProperty;
  @Input() pageLoading: boolean;

  public map: google.maps.Map;
  public marker: google.maps.Marker;




  ngAfterViewInit(): void {
    this.zone.runOutsideAngular(async () => {

      const mapsLoader = this.googleService.getMapsLoader();
      const google = await mapsLoader.load();

      let div = document.createElement("div") as HTMLElement;
      div.setAttribute("id", "map");


      let parent = this.element.nativeElement as HTMLElement;
      div.style.height = this.height;
      div.style.width = this.width;
      div.style.position = 'relative';
      parent.appendChild(div);



      this.map = new google.maps.Map(div as HTMLElement, {
        center: { lat: this.property.address.latitude, lng: this.property.address.longitude },
        zoom: 16,
      });

      this.marker = new google.maps.Marker({
        position: { lat: this.property.address.latitude, lng: this.property.address.longitude },
        map: this.map,
        draggable: true,
        animation: google.maps.Animation.DROP,
      });
      this.marker.addListener("click", this.toggleBounce);

    });

  }

  toggleBounce(): void {
    if (this.marker !== undefined && !this.pageLoading) {

      if (this.marker.getAnimation() !== null) {
        this.marker.setAnimation(null);
      } else {
        this.marker.setAnimation(google.maps.Animation.BOUNCE);
      }

    } else if (!this.pageLoading) {
      console.log(this.property);
      this.marker = new google.maps.Marker({
        position: this.map.getCenter(),
        map: this.map,
        draggable: true,

      });
    }

  }


}


