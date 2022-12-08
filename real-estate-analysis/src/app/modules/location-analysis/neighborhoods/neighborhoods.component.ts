import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { concatMap, finalize, takeWhile, tap } from 'rxjs/operators';
import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { AddNeighborhoodComponent } from '../dialogs/add-neighborhood/add-neighborhood.component';
import { DeleteNeighborhoodComponent } from '../dialogs/delete-neighborhood/delete-neighborhood.component';
import { ReadNeighborhood } from '../dtos/reads/readNeighborhood';
import { NeighborhoodService } from '../services/neighborhoodService/neighborhood.service';

@Component({
  selector: 'app-neighborhoods',
  templateUrl: './neighborhoods.component.html',
  styleUrls: ['./neighborhoods.component.css']
})
export class NeighborhoodsComponent implements OnInit {

  public displayedColumns: string[] = ['neighborhood', 'median-household-income', 'median-contract-rent', 'unemployment-rate-difference', 'poverty-level', 'ethnic-mox-main-slice', 'homes-median-days-on-market', 'buttons'];
  public neighborhoods: ReadNeighborhood[];
  public filteredNeighborhoods: ReadNeighborhood[] = [];
  public passingColumns = [1, 2, 3, 4, 5];
  public neighborhoodsSearchForm: FormGroup;
  public filteredCities: string[];
  public cities: string[];
  public states: ReadState[];
  public pageLoading = true;
  private alive = true;


  constructor(private dialog: MatDialog,
    private fb: FormBuilder,
    private neighborhoodService: NeighborhoodService) { this.createSearchForm(); }

  ngOnInit(): void {
    this.loadNeighborhoods();
  }


  public loadNeighborhoods(): void {
    this.pageLoading = true;
    this.neighborhoodService.getAllNeighborhoods()
      .pipe(takeWhile(() => this.alive))
      .subscribe(data => {
        this.neighborhoods = data;
        this.filteredNeighborhoods = [...this.neighborhoods]

        this.setCities();
        this.setStates();
        this.pageLoading = false;
      },
        error => {
          console.log(error);
          this.pageLoading = false;
        });
  }


  public onAddNewClick(): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '90vh';
    dialogConfig.maxWidth = '50%';
    dialogConfig.scrollStrategy?.disable;

    this.dialog.open(AddNeighborhoodComponent, dialogConfig)
      .afterClosed()

      .subscribe((newNeighborhoodObject: ReadNeighborhood) => {
        if (newNeighborhoodObject !== undefined) {
          this.neighborhoods.push(newNeighborhoodObject);
          this.filteredNeighborhoods = [...this.neighborhoods];
          this.setCities();
          this.setStates();
        }
      });
  }

  filterNeighborhoods(): void {

    this.filteredNeighborhoods = this.neighborhoods;

    const value = this.neighborhoodsSearchForm.value;
    const city = value.city;
    const stateId = +value.stateId;
    const numberOfPassingColumns = +value.numberOfPassingColumns;

    if (city !== '') {
      this.filteredNeighborhoods = this.filteredNeighborhoods.filter(x => x.city === city);
    }

    if (stateId !== 0) {
      this.filteredNeighborhoods = this.filteredNeighborhoods.filter(x => x.state.id === stateId);

    }

    if (numberOfPassingColumns !== 0) {
      this.filteredNeighborhoods = this.filteredNeighborhoods.filter(x => this.getNumberPassingColumns(x) === numberOfPassingColumns);
    }
  }




  public onDeleteClick(neighborhood: ReadNeighborhood): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      neighborhood
    };

    const dialogRef = this.dialog.open(DeleteNeighborhoodComponent, dialogConfig);
    dialogRef
      .afterClosed()
      .subscribe(response => {
        if (response === true) {
          const itemIndex = this.neighborhoods.findIndex(x => x.id === neighborhood.id);
          this.neighborhoods.splice(itemIndex, 1);
          this.filteredNeighborhoods = [...this.neighborhoods];

          this.setCities();
          this.setStates();
        }
      }

      );
  }

  public onEditClick(neighborhood: ReadNeighborhood): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      neighborhood
    };

    const dialogRef = this.dialog.open(AddNeighborhoodComponent, dialogConfig);


    dialogRef.afterClosed()
      .subscribe((updatedNeighborhood) => {
        if (updatedNeighborhood) {
          const itemIndex = this.filteredNeighborhoods.findIndex(x => x.id === updatedNeighborhood.id);
          this.neighborhoods.splice(itemIndex, 1);
          this.neighborhoods.push(updatedNeighborhood);
          this.filteredNeighborhoods = [...this.neighborhoods];
          this.setCities();
          this.setStates();
        }

      }, (err: HttpErrorResponse) => {
        console.log(err);
      }
      );
  }

  private createSearchForm(): void {
    this.neighborhoodsSearchForm = this.fb.group({
      city: [''],
      stateId: [''],
      numberOfPassingColumns: ['', Validators.required]
    })
  }

  getNumberPassingColumns(neighborhood: ReadNeighborhood): number {
    let number = 0;

    if (neighborhood.medianHouseholdIncomeIsGood) {
      number++;
    }
    if (neighborhood.medianContractRentIsGood) {
      number++;
    }
    if (neighborhood.cityToNeighborhoodUnemploymentRateDifferenceIsGood) {
      number++;
    }
    if (neighborhood.povertyRateIsGood) {
      number++;
    }
    if (neighborhood.ethnicMixLargestSlicePercentIsGood) {
      number++;
    }

    return number;
  }


  private setCities(): string[] {
    this.cities = [...new Set(this.neighborhoods.map(x => x.city))];
    this.filteredCities = [...this.cities];
    return this.filteredCities;
  }

  private setStates(): ReadState[] {
    let duplicateArray = [...new Set(this.neighborhoods.map(x => x.state))];

    // filters out duplicate state objects
    this.states = duplicateArray.reduce((unique, o) => {
      if (!unique.some(obj => obj.id === o.id)) {
        unique.push(o);
      }
      return unique;
    }, []);


    return this.states;
  }

}
