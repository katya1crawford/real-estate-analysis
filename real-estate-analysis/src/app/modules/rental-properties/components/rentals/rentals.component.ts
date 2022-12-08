import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { finalize, takeWhile } from 'rxjs/operators';
import { AnnualIncomeStatementComponent } from '../../dialogs/annual-income-statement/annual-income-statement.component';
import { AttachmentsComponent } from '../../dialogs/attachments/attachments.component';
import { DeleteRentalPropertyComponent } from '../../dialogs/delete-rental-property/delete-rental-property.component';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { PropertyService, PropertyStatusEnum } from '../../services/property.service';

@Component({
  selector: 'app-rentals',
  templateUrl: './rentals.component.html',
  styleUrls: ['./rentals.component.css'],
})
export class RentalsComponent implements OnInit, OnDestroy {

  public alive = true;
  public propertiesLoading = false;
  public properties: ReadProperty[];
  public filteredProperties: ReadProperty[] = [];

  public searchProperties$: Observable<ReadProperty[]>;
  public isSearching = false;
  public noImage = './assets/noImage.jpg';
  public statusDisplay: string;
  public serverError = false;
  public propertyStatus: any;
  public searchForm: FormGroup;
  constructor(private dialog: MatDialog,
    private fb: FormBuilder,
    private route: Router,
    private activatedRoute: ActivatedRoute,
    private propertyService: PropertyService) { this.createSearchForm() }


  ngOnDestroy(): void {
    this.alive = false;
  }

  ngOnInit(): void {
    this.activatedRoute.paramMap
      .pipe(takeWhile(() => this.alive))
      .subscribe((params: ParamMap) => {
        const propertyStatus = params.get('propertyStatus');
        this.setProperties(propertyStatus);
      });
  }

  setProperties(status: any): void {
    this.propertiesLoading = true;
    switch (status) {
      case 'listing':
        this.propertyStatus = PropertyStatusEnum.Listing;
        this.statusDisplay = ' - Listing';
        break;
      case 'purchased':
        this.propertyStatus = PropertyStatusEnum.Purchased;
        this.statusDisplay = ' - Purchased';
        break;
      case 'in-review':
        this.propertyStatus = PropertyStatusEnum.InReview;
        this.statusDisplay = ' - In-Review';
        break;
      default:
        this.route.navigate(['/rentals/listing']);
        break;

    }

    this.propertyService.getByPropertyStatus(status)
      .pipe(takeWhile(() => this.alive),
        finalize(() => this.propertiesLoading = false)
      )
      .subscribe(data => { this.properties = data },
        (error) => {
          console.log(error)
        });
  }


  search(term: string): void {
    if (term !== '') {
      const lowerCaseTerm = term.trim().toLowerCase();

      this.filteredProperties = this.properties.filter(x =>
        x.address.address.toLowerCase().indexOf(lowerCaseTerm) > -1
        || x.address.city.toLowerCase().indexOf(lowerCaseTerm) > -1
        || x.address.state.name.toLowerCase().indexOf(lowerCaseTerm) > -1
        || x.address.zipCode.toLowerCase().indexOf(lowerCaseTerm) > -1
        || x.propertyType.name.toLowerCase().indexOf(lowerCaseTerm) > -1
        || x.financialSummary.annualCapRate.toString().indexOf(lowerCaseTerm) > -1
        || x.financialSummary.annualCashOnCashRate.toString().indexOf(lowerCaseTerm) > -1
        || x.financialSummary.annualCashFlow.toString().indexOf(lowerCaseTerm) > -1
        || (x.notes ? x.notes.toLowerCase().indexOf(lowerCaseTerm) > -1 : false))
    } else {

      this.filteredProperties = [];

    }

  }

  onDelete(property: ReadProperty): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '25vh';
    dialogConfig.width = '30vw';
    dialogConfig.data = {
      property: property
    };

    const dialogRef = this.dialog.open(DeleteRentalPropertyComponent, dialogConfig);

    dialogRef
      .afterClosed()
      .subscribe(
        result => {
          console.log(result);
          if (result === true) {
            this.properties = this.properties.filter(x => x.id !== property.id)

          }
        }
      );
  }


  onDetails(property: ReadProperty): void {
    this.route.navigate(['rentals/property-details', property.id])
  }

  onAttachmentsClick(property: ReadProperty): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;
    dialogConfig.height = '250px';
    dialogConfig.width = '500px';


    dialogConfig.data = {
      property: property
    };

    const dialogRef = this.dialog.open(AttachmentsComponent, dialogConfig);

    dialogRef.afterClosed().subscribe();

  }

  onAnnualIncomeStatement(property: ReadProperty) {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '800px';
    dialogConfig.width = '700px';

    dialogConfig.data = {
      property: property
    };

    this.dialog.open(AnnualIncomeStatementComponent, dialogConfig);

  }


  onEditClick(property: ReadProperty): void {
    this.route.navigate(['/add-edit-property', property.id])
  }

  createSearchForm() {
    this.searchForm = this.fb.group({
      searchValue: ['']
    })

  }


  get searchValue(): FormControl {
    return this.searchForm.get('searchValue') as FormControl;
  }
}
