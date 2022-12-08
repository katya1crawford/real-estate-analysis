import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { takeWhile, finalize } from 'rxjs/operators';
import { ReadFile } from '../../dtos/reads/readFile';
import { ReadFinancialForecast } from '../../dtos/reads/readFinancialForecast';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { CalculatorService } from '../../services/calculator.service';
import { GalleryService } from '../../services/gallery.service';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.css']
})
export class PropertyDetailsComponent implements OnInit {

  constructor(private propertyService: PropertyService,
    private calculatorService: CalculatorService,
    private galleryService: GalleryService,
    private route: Router, private activatedRoute: ActivatedRoute) { }

  public pageLoading = true;
  property: ReadProperty;
  errorMessages: any[];
  serverError = false;
  alive = true;
  financialForecasts: ReadFinancialForecast[];
  galleryImages: ReadFile[];

  ngOnInit(): void {
    this.activatedRoute.paramMap
      .pipe(takeWhile(() => this.alive))
      .subscribe((params: ParamMap) => {
        const id = params.get('id');
        if (typeof id === "string") {
          this.getAllData(parseInt(id));
        }
      });
  }

  ngOnDestroy(): void {
    this.alive = false;
  }


  getAllData(id: number): void {
    this.pageLoading = true;
    const joined$ = forkJoin(
      [this.propertyService.getProperty(id),
      this.calculatorService.getRentalPropertyLongTermFinancialForecasts(id),
      this.galleryService.getAllSmall(id)]);

    joined$
      .pipe(takeWhile(() => this.alive),
        finalize(() => {
          this.pageLoading = false;
        }))
      .subscribe(([property, forecast, gallery]) => {

        this.property = property;
        this.financialForecasts = forecast;
        this.galleryImages = gallery;
      }, (error: HttpErrorResponse) => {
        if (error.status === 400) {
          this.serverError = true;
          this.errorMessages = error.error.errors;
        }
      });


  }



  onRentRollClick(property: ReadProperty): void {
    this.route.navigate([`/rental-property-details/${property.id}/rent-roll`]);

  }



}
