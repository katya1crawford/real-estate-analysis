import { HttpErrorResponse } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { takeWhile } from 'rxjs/operators';

import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { AddCityComponent } from '../dialogs/add-city/add-city.component';
import { DeleteCityComponent } from '../dialogs/delete-city/delete-city.component';
import { RemoveFromFavoritesComponent } from '../dialogs/remove-from-favorites/remove-from-favorites.component';
import { ReadCity } from '../dtos/reads/readCity';
import { WriteCity } from '../dtos/writes/writeCity';
import { CityService } from '../services/cityService/city.service';

@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.css'],

})
export class CitiesComponent implements OnInit, AfterViewInit, OnDestroy {
  public checked = false;
  public disabled = false;
  public alive = true;
  public harvestingCityData = false;
  public pageLoading = false;
  public validationErrorResult = false;
  public filteredCities: ReadCity[];
  public serverError = false;
  public cities: ReadCity[];
  private selectedCity: ReadCity;
  public states$: Observable<ReadState[]>;
  public cityId: number | undefined;
  public states: ReadState[];
  public dataSource: MatTableDataSource<ReadCity>;
  public citiesSearchForm: FormGroup;
  public displayedColumns: string[] = ['city', 'population-growth', 'median-household-income', 'median-value-growth', 'crime-index', 'job-growth', 'delete-edit-favorite'];
  public latestHarvestDate: Date | undefined;
  public passingColumnsNumbers = [1, 2, 3, 4, 5];
  public orderByItems: { key: string, value: string }[] = [
    { key: 'populationSize', value: 'Population Size' },
    { key: 'populationGrowth', value: 'Population Growth' },
    { key: 'medianHouseholdIncomeGrowth', value: 'Median Household Income Growth' },
    { key: 'medianHouseCondoValueGrowth', value: 'Median House/Condo Value Growth' },
    { key: 'jobGrowthInRecentYear', value: 'Job Growth In Recent Year' }
  ];

  @ViewChild(MatPaginator)
  set paginator(value: MatPaginator) {
    if (this.dataSource) {
      this.dataSource.paginator = value;
    }
  }


  constructor(private cityService: CityService,
    private route: Router,
    private formBuilder: FormBuilder,
    // private differs: IterableDiffers,
    private dialog: MatDialog,
  ) {
    this.createForm();
    // this.diff = this.differs.find([]).create(null);
  }


  ngOnDestroy(): void {
    this.alive = false;
  }


  ngAfterViewInit() {
    this.dataSource = new MatTableDataSource(this.filteredCities);
    this.dataSource.paginator = this.paginator;
  }



  ngOnInit(): void {
    this.states$ = this.cityService.getAllStates();
    this.getCities();

  }


  onToggleIsFavoriteClick(city: ReadCity): void {
    if (city.isFavorite) {
      this.removeFromFavorites(city);

    } else {
      city.isFavorite = !city.isFavorite;
      this.cityService.toggleIsFavorite(city.id)
        .subscribe(() => {
          this.cities.find(x => x.id === city.id)!.isFavorite = !city.isFavorite;
          this.filteredCities.find(x => x.id === city.id)!.isFavorite = !city.isFavorite;
        },
          (error: any) => {
            console.log(error);
          }
        );
    }
  }


  removeFromFavorites(city: ReadCity): void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    dialogConfig.data = {
      city
    };

    const dialogRef = this.dialog.open(RemoveFromFavoritesComponent, dialogConfig);
    dialogRef.afterClosed()
      .subscribe((response) => {
        if (response === true) {
          city.isFavorite = !city.isFavorite;
          this.cities.find(x => x.id === city.id).isFavorite = !city.isFavorite;
          this.filteredCities.find(x => x.id === city.id).isFavorite = !city.isFavorite;
        }
      });

  }


  onAddNewClick(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '90%';
    dialogConfig.width = '60vw';
    dialogConfig.scrollStrategy?.enable;
    dialogConfig.data = {
      city: this.selectedCity
    };

    const dialogRef = this.dialog.open(AddCityComponent, dialogConfig);
    dialogRef.afterClosed()
      .pipe(
        takeWhile(() => this.alive)
      )
      .subscribe((newCity: any | ReadCity) => {
        if (newCity !== undefined) {
          this.cities.push(newCity);
          this.filteredCities = [...this.cities];
          this.dataSource = new MatTableDataSource(this.filteredCities);
          this.setLatestHarvestDate(this.cities);
        }
      }, (err) => {
        console.log(err);
      }
      );
  }

  onHarvestCityDataClick(): void {
    this.route.navigate(['location-analysis/harvestCityData']);
  }


  onDeleteClick(city: ReadCity): void {
    this.pageLoading = true;
    this.selectedCity = city;
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.data = {
      city: this.selectedCity
    };

    const dialogRef = this.dialog.open(DeleteCityComponent, dialogConfig);
    dialogRef
      .afterClosed()
      .subscribe((response) => {
        if (response == true) {
          this.filteredCities = this.cities.filter(x => x.id !== city.id);
          this.dataSource = new MatTableDataSource(this.filteredCities);
          this.pageLoading = true;

        }
      });
  }



  showOnlyFavoriteCities(): void {
    this.checked = !this.checked;
  }

  filterCities(): void {
    this.filteredCities = [...this.cities];

    if (this.stateId.value !== '' && this.stateId.value !== 0) {

      this.filteredCities = this.filteredCities.filter(x => x.state.id === +(this.stateId.value));
      this.dataSource = new MatTableDataSource(this.filteredCities);
    }

    else {
      this.filteredCities = [...this.cities!];
      this.dataSource = new MatTableDataSource(this.filteredCities);
    }

    if (this.minPopulationSize.value > 0) {
      this.filteredCities = this.filteredCities.filter(x => x.populationInYearEnd >= +(this.minPopulationSize.value));
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.maxPopulationSize.value > 0) {
      this.filteredCities = this.filteredCities.filter(x => x.populationInYearEnd <= +(this.maxPopulationSize.value));
      this.dataSource = new MatTableDataSource(this.filteredCities);


    }

    if (this.showOnlyFavorite.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.isFavorite === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByPopulationGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.populationGrowthRateIsGood === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByCrimeIndex.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.crimeIndexInYearEndIsGood === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByHouseValueGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.medianHouseOrCondoValueGrowthRateIsGood === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByJobGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.recentYearJobsGrowthRateIsGood === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByHouseholdIncomeGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.medianHouseholdIncomeGrowthRateIsGood === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByFailingCrimeIndex.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.crimeIndexInYearEndIsGood === false);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByFailingHouseValueGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.medianHouseOrCondoValueGrowthRateIsGood === false);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByFailingJobGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.recentYearJobsGrowthRateIsGood === false);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByFailingPopulationGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.populationGrowthRateIsGood === false);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.filterByFailingHouseholdIncomeGrowth.value === true) {
      this.filteredCities = this.filteredCities.filter(x => x.medianHouseholdIncomeGrowthRateIsGood === false);
      this.dataSource = new MatTableDataSource(this.filteredCities);

    }

    if (this.checked == true) {
      this.filteredCities = this.filteredCities.filter(x => x.isFavorite === true);
      this.dataSource = new MatTableDataSource(this.filteredCities);
    }

  }

  onEditClick(city: ReadCity): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.height = '90%';
    dialogConfig.width = '60vw';
    dialogConfig.scrollStrategy?.enable;
    dialogConfig.data = {
      city
    };

    const dialogRef = this.dialog.open(AddCityComponent, dialogConfig);
    dialogRef.afterClosed()
      .pipe(
        takeWhile(() => this.alive)
      )
      .subscribe((updatedCity) => {
        if (updatedCity !== undefined) {


          this.dataSource = new MatTableDataSource(this.filteredCities);
          this.setLatestHarvestDate(this.cities);
        }
      }, (err) => {
        console.log(err);
      }
      );
  }






  getCities(): void {
    this.pageLoading = true;

    this.cityService.getAllCities()
      .pipe(
        takeWhile(() => this.alive)
      )

      .subscribe((cities: any) => {
        this.pageLoading = false

        this.cities = cities;
        this.filteredCities = [...this.cities];

        this.dataSource = new MatTableDataSource(this.filteredCities);
        this.dataSource.paginator = this.paginator;
        if (cities.length > 0) {
          this.setLatestHarvestDate(cities);
        }

      },
        (error) => {
          this.pageLoading = false

          console.log(error);

        });

  }


  setLatestHarvestDate(cities: ReadCity[]): void {
    const autoGeneratedCities = cities.filter(x => x.autoGeneratedDate);

    if (autoGeneratedCities.length > 0) {
      const sortedResults = autoGeneratedCities.sort((a, b) => this.orderByDesc(a.autoGeneratedDate, b.autoGeneratedDate));
      this.latestHarvestDate = sortedResults[0].autoGeneratedDate;
    }
  }

  public createForm(): void {

    this.citiesSearchForm = this.formBuilder.group({
      stateId: [''],
      numberOfPassingColumns: [''],
      minPopulationSize: [''],
      maxPopulationSize: [''],
      showOnlyFavorite: [false],
      orderByDescItem: [false],
      filterByPopulationGrowth: [false],
      filterByCrimeIndex: [false],
      filterByHouseholdIncomeGrowth: [false],
      filterByHouseValueGrowth: [false],
      filterByJobGrowth: [false],
      filterByFailingPopulationGrowth: [false],
      filterByFailingCrimeIndex: [false],
      filterByFailingHouseholdIncomeGrowth: [false],
      filterByFailingHouseValueGrowth: [false],
      filterByFailingJobGrowth: [false],

    });

  }

  orderByDesc(a: any, b: any): number {

    if (a > b) {
      return 1;

    }

    if (a < b) {
      return -1;
    }

    return 0;

  }

  get stateId(): FormControl {
    return this.citiesSearchForm.get('stateId') as FormControl;
  }

  get numberOfPassingColumns(): FormControl {
    return this.citiesSearchForm.get('numberOfPassingColumns') as FormControl;
  }

  get minPopulationSize(): FormControl {
    return this.citiesSearchForm.get('minPopulationSize') as FormControl;
  }

  get maxPopulationSize(): FormControl {
    return this.citiesSearchForm.get('maxPopulationSize') as FormControl;
  }
  get showOnlyFavorite(): FormControl {
    return this.citiesSearchForm.get('showOnlyFavorite') as FormControl;
  }

  get orderByDescItem(): FormControl {
    return this.citiesSearchForm.get('orderByDescItem') as FormControl;
  }

  get filterByHouseValueGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByHouseValueGrowth') as FormControl;
  }

  get filterByPopulationGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByPopulationGrowth') as FormControl;
  }

  get filterByCrimeIndex(): FormControl {
    return this.citiesSearchForm.get('filterByCrimeIndex') as FormControl;
  }


  get filterByJobGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByJobGrowth') as FormControl;
  }

  get filterByFailingJobGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByFailingJobGrowth') as FormControl;
  }


  get filterByFailingPopulationGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByFailingPopulationGrowth') as FormControl;
  }

  get filterByFailingHouseValueGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByFailingHouseValueGrowth') as FormControl;
  }

  get filterByHouseholdIncomeGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByHouseholdIncomeGrowth') as FormControl;
  }

  get filterByFailingCrimeIndex(): FormControl {
    return this.citiesSearchForm.get('filterByFailingCrimeIndex') as FormControl;
  }

  get filterByFailingHouseholdIncomeGrowth(): FormControl {
    return this.citiesSearchForm.get('filterByFailingHouseholdIncomeGrowth') as FormControl;
  }
}
