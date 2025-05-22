import { Component, OnInit, OnDestroy, IterableDiffers, DoCheck, IterableDiffer } from '@angular/core';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { Title } from '@angular/platform-browser';
import { appConfig } from '../../../../app.config';
import { CityService } from '../../services/city.service';
import { ReadCity } from '../../dtos/reads/readCity';
import { takeWhile } from 'rxjs/internal/operators/takeWhile';
import { HttpErrorResponse } from '@angular/common/http';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { MessageType } from '../../../../shared/enums/messageType';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { AddEditCityComponent } from '../../modals/add-edit-city/add-edit-city.component';
import { ModalService } from '../../../../shared/services/modal/modal.service';
import { ReadState } from 'src/app/shared/dtos/reads/readState';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ItemCloneService } from 'src/app/shared/services/item-clone.service';
import { HarvestCityDataComponent } from '../../modals/harvest-city-data/harvest-city-data.component';

@Component({
    templateUrl: './cities.component.html'
})
export class CitiesComponent implements OnInit, OnDestroy, DoCheck {
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public filteredCities: ReadCity[];
    public pageLoading = true;
    public states: ReadState[];
    public citiesSearchForm: UntypedFormGroup;
    public passingColumnsNumbers = [1, 2, 3, 4, 5];
    public orderByItems: { key: string, value: string }[] = [
        { key: 'populationSize', value: 'Population Size' },
        { key: 'populationGrowth', value: 'Population Growth' },
        { key: 'medianHouseholdIncomeGrowth', value: 'Median Household Income Growth' },
        { key: 'medianHouseCondoValueGrowth', value: 'Median House/Condo Value Growth' },
        { key: 'jobGrowthInRecentYear', value: 'Job Growth In Recent Year' }
    ];
    public latestHarvestDate: Date;
    public harvestingCityData = false;

    private alive = true;
    private selectedCity: ReadCity;
    private cities: ReadCity[];
    private iterableDiffer: IterableDiffer<unknown>;

    constructor(titleService: Title,
        private cityService: CityService,
        private messagingService: MessagingService,
        private modalService: ModalService,
        private formBuilder: UntypedFormBuilder,
        private itemCloneService: ItemCloneService<ReadCity[]>,
        iterableDiffers: IterableDiffers
    ) {
        this.iterableDiffer = iterableDiffers.find([]).create(null);
        titleService.setTitle(`${appConfig.businessName}: Location Analysis: Cities`);
        this.createCitiesSearchForm();
        this.subscribeToMessagingService();
    }

    ngDoCheck(): void {
        const citiesHaveChange = this.iterableDiffer.diff(this.cities);

        if (citiesHaveChange) {
            this.itemCloneService.setCloneItem(this.cities);
            this.setStates();
            this.filterCities();
        }
    }

    ngOnInit(): void {
        this.loadCities();
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onAddNewClick(): void {
        const addModal = this.modalService.show(AddEditCityComponent, { sizeClass: 'modal-xl' });
        addModal.messageType = MessageType.locationAnalysisNewCityAdded;
    }

    public onHarvestCityDataClick(): void {
        const addModal = this.modalService.show(HarvestCityDataComponent);
        addModal.messageType = MessageType.locationAnalysisFinishedHarvestingCityData;
    }

    public onToggleIsFavoriteClick(city: ReadCity): void {
        this.selectedCity = city;

        if (city.isFavorite) {
            const unfavoriteModal = this.modalService.show(ConfirmComponent);
            unfavoriteModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to remove ${city.cityName} city from your favorites?`, MessageType.locationAnalysisRemoveCityFromFavorites);

        } else {

            city.isFavorite = !city.isFavorite;
            this.cityService.toggleIsFavorite(this.selectedCity.id)
                .pipe(takeWhile(() => this.alive))
                .subscribe(() => {
                    this.cities.find(x => x.id === city.id).isFavorite = city.isFavorite;
                },
                    (error: HttpErrorResponse) => {
                        if (error.status === 400) {
                            this.validationErrorResult = error.error as ReadValidationResult;
                        } else {
                            this.serverError = true;
                        }
                    });

        }



    }

    public onEditClick(city: ReadCity): void {
        const editModal = this.modalService.show(AddEditCityComponent, { sizeClass: 'modal-xl' });
        editModal.setEditCityFormValues(city);
        editModal.messageType = MessageType.locationAnalysisCityUpdated;
    }

    public onDeleteClick(city: ReadCity): void {
        this.selectedCity = city;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${city.cityName} city?`, MessageType.locationAnalysisDeleteCity);
    }

    public filterCities(): void {
        if (this.itemCloneService.getCloneItem() === undefined) {
            return;
        }

        // setTimeOut is to prevent page from freezing;
        setTimeout(() => {
            const stateId = +this.stateId.value;
            const numberOfPassingColumns = +this.numberOfPassingColumns.value;
            const minPopulationSize = +this.minPopulationSize.value;
            const maxPopulationSize = +this.maxPopulationSize.value;
            const showOnlyFavorite = this.showOnlyFavorite.value as Boolean;
            const filterByPopulationGrowth = this.filterByPopulationGrowth.value as Boolean;
            const filterByCrimeIndex = this.filterByCrimeIndex.value as Boolean;
            const filterByHouseholdIncomeGrowth = this.filterByHouseholdIncomeGrowth.value as Boolean;
            const filterByHouseValueGrowth = this.filterByHouseValueGrowth.value as Boolean;
            const filterByJobGrowth = this.filterByJobGrowth.value as Boolean;

            const filterByFailingPopulationGrowth = this.filterByFailingPopulationGrowth.value as Boolean;
            const filterByFailingCrimeIndex = this.filterByFailingCrimeIndex.value as Boolean;
            const filterByFailingHouseholdIncomeGrowth = this.filterByFailingHouseholdIncomeGrowth.value as Boolean;
            const filterByFailingHouseValueGrowth = this.filterByFailingHouseValueGrowth.value as Boolean;
            const filterByFailingJobGrowth = this.filterByFailingJobGrowth.value as Boolean;

            this.filteredCities = this.itemCloneService.getCloneItem();

            if (stateId !== 0) {
                this.filteredCities = this.filteredCities.filter(x => x.state.id === stateId);
            }

            if (numberOfPassingColumns !== 0) {
                this.filteredCities = this.filteredCities.filter(x => this.getNumberOfPassingColumns(x) === numberOfPassingColumns);
            }

            if (minPopulationSize !== 0) {
                this.filteredCities = this.filteredCities.filter(x => x.populationInYearEnd >= minPopulationSize);
            }

            if (maxPopulationSize !== 0) {
                this.filteredCities = this.filteredCities.filter(x => x.populationInYearEnd <= maxPopulationSize);
            }

            if (filterByPopulationGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.populationGrowthRateIsGood);
            }

            if (filterByCrimeIndex === true) {
                this.filteredCities = this.filteredCities.filter(x => x.crimeIndexInYearEndIsGood);
            }

            if (filterByHouseholdIncomeGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.medianHouseholdIncomeGrowthRateIsGood);
            }

            if (filterByHouseValueGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.medianHouseOrCondoValueGrowthRateIsGood);
            }

            if (filterByJobGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.recentYearJobsGrowthRateIsGood);
            }

            if (filterByFailingPopulationGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.populationGrowthRateIsGood === false);
            }

            if (filterByFailingCrimeIndex === true) {
                this.filteredCities = this.filteredCities.filter(x => x.crimeIndexInYearEndIsGood === false);
            }

            if (filterByFailingHouseholdIncomeGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.medianHouseholdIncomeGrowthRateIsGood === false);
            }

            if (filterByFailingHouseValueGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.medianHouseOrCondoValueGrowthRateIsGood === false);
            }

            if (filterByFailingJobGrowth === true) {
                this.filteredCities = this.filteredCities.filter(x => x.recentYearJobsGrowthRateIsGood === false);
            }

            if (showOnlyFavorite === true) {
                this.filteredCities = this.filteredCities.filter(x => x.isFavorite);
            }

            this.orderResultsByDesc();
        }, 0);
    }

    public orderResultsByDesc(): void {
        const orderByDescItem = this.orderByDescItem.value;

        if (orderByDescItem !== '') {
            switch (orderByDescItem) {
                case 'populationSize':
                    this.filteredCities = this.filteredCities.sort((a, b) => this.orderByDesc(a.populationInYearEnd, b.populationInYearEnd));
                    break;

                case 'populationGrowth':
                    this.filteredCities = this.filteredCities.sort((a, b) => this.orderByDesc(a.populationGrowthRate, b.populationGrowthRate));
                    break;

                case 'medianHouseholdIncomeGrowth':
                    this.filteredCities = this.filteredCities.sort((a, b) => this.orderByDesc(a.medianHouseholdIncomeGrowthRate, b.medianHouseholdIncomeGrowthRate));
                    break;

                case 'medianHouseCondoValueGrowth':
                    this.filteredCities = this.filteredCities.sort((a, b) => this.orderByDesc(a.medianHouseOrCondoValueGrowthRate, b.medianHouseOrCondoValueGrowthRate));
                    break;

                case 'jobGrowthInRecentYear':
                    this.filteredCities = this.filteredCities.sort((a, b) => this.orderByDesc(a.recentYearJobsGrowthRate, b.recentYearJobsGrowthRate));
                    break;
            }
        }
    }

    private getNumberOfPassingColumns(city: ReadCity): number {
        let number = 0;

        if (city.populationGrowthRateIsGood) {
            number++;
        }

        if (city.medianHouseholdIncomeGrowthRateIsGood) {
            number++;
        }

        if (city.medianHouseOrCondoValueGrowthRateIsGood) {
            number++;
        }

        if (city.crimeIndexInYearEndIsGood) {
            number++;
        }

        if (city.recentYearJobsGrowthRateIsGood) {
            number++;
        }

        return number;
    }

    private setStates(): void {
        const dupStates = this.cities.map(x => x.state);
        this.states = [];

        for (let i = 0; i < dupStates.length; i++) {
            const existingState = this.states.find(x => x.id === dupStates[i].id);

            if (existingState === undefined) {
                this.states.push(dupStates[i]);
            }
        }

        this.states = this.states.sort(this.orderStatesByNameAsc);
    }

    private createCitiesSearchForm(): void {
        this.citiesSearchForm = this.formBuilder.group({
            stateId: [''],
            numberOfPassingColumns: [''],
            minPopulationSize: [''],
            maxPopulationSize: [''],
            orderByDescItem: [''],
            showOnlyFavorite: [false],
            filterByPopulationGrowth: [false],
            filterByCrimeIndex: [false],
            filterByHouseholdIncomeGrowth: [false],
            filterByHouseValueGrowth: [false],
            filterByJobGrowth: [false],
            filterByFailingPopulationGrowth: [false],
            filterByFailingCrimeIndex: [false],
            filterByFailingHouseholdIncomeGrowth: [false],
            filterByFailingHouseValueGrowth: [false],
            filterByFailingJobGrowth: [false]
        });
    }

    private loadCities(): void {
        this.pageLoading = true;

        this.cityService.getAllCities()
            .pipe(takeWhile(() => this.alive))
            .subscribe(cities => {
                this.cities = cities;

                if (cities.length > 0) {
                    this.setLatestHarvestDate(cities);
                }

                this.pageLoading = false;
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.pageLoading = false;
                });
    }

    private setLatestHarvestDate(cities: ReadCity[]): void {
        const autoGeneratedCities = cities.filter(x => x.autoGeneratedDate);

        if (autoGeneratedCities.length > 0) {
            const sortedResults = autoGeneratedCities.sort((a, b) => this.orderByDesc(a.autoGeneratedDate, b.autoGeneratedDate));
            this.latestHarvestDate = sortedResults[0].autoGeneratedDate;
        }
    }

    private orderByDesc(a: any, b: any): number {
        if (a > b) {
            return -1;
        }

        if (a < b) {
            return 1;
        }

        return 0;
    }

    private orderStatesByNameAsc(a: ReadState, b: ReadState): number {
        if (a.name > b.name) {
            return 1;
        }

        if (a.name < b.name) {
            return -1;
        }

        return 0;
    }

    private addCity(newCity: ReadCity): void {
        this.cities.push(newCity);
    }

    private updateCity(updatedCity: ReadCity): void {
        const itemIndex = this.cities.findIndex(x => x.id === updatedCity.id);
        this.cities.splice(itemIndex, 1);
        this.cities.push(updatedCity);
    }

    private deleteCity() {
        this.cityService.deleteCity(this.selectedCity.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.cities.findIndex(x => x.id === this.selectedCity.id);
                this.cities.splice(itemIndex, 1);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private removeFromFavorites() {
        this.cityService.toggleIsFavorite(this.selectedCity.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                this.selectedCity.isFavorite = !this.selectedCity.isFavorite;
                this.cities.find(x => x.id === this.selectedCity.id).isFavorite = this.selectedCity.isFavorite;
                this.filterCities();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private subscribeToMessagingService(): void {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.locationAnalysisNewCityAdded) {
                    this.addCity(message.content as ReadCity);
                }

                if (message.messageType === MessageType.locationAnalysisDeleteCity) {
                    this.deleteCity();
                }

                if (message.messageType === MessageType.locationAnalysisCityUpdated) {
                    this.updateCity(message.content as ReadCity);
                }

                if (message.messageType === MessageType.locationAnalysisFinishedHarvestingCityData) {
                    this.loadCities();
                }

                if (message.messageType === MessageType.locationAnalysisRemoveCityFromFavorites) {
                    this.removeFromFavorites();
                }
            });
    }

    get stateId(): UntypedFormControl {
        return this.citiesSearchForm.get('stateId') as UntypedFormControl;
    }

    get numberOfPassingColumns(): UntypedFormControl {
        return this.citiesSearchForm.get('numberOfPassingColumns') as UntypedFormControl;
    }

    get minPopulationSize(): UntypedFormControl {
        return this.citiesSearchForm.get('minPopulationSize') as UntypedFormControl;
    }

    get maxPopulationSize(): UntypedFormControl {
        return this.citiesSearchForm.get('maxPopulationSize') as UntypedFormControl;
    }

    get orderByDescItem(): UntypedFormControl {
        return this.citiesSearchForm.get('orderByDescItem') as UntypedFormControl;
    }

    get showOnlyFavorite(): UntypedFormControl {
        return this.citiesSearchForm.get('showOnlyFavorite') as UntypedFormControl;
    }

    get filterByPopulationGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByPopulationGrowth') as UntypedFormControl;
    }

    get filterByCrimeIndex(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByCrimeIndex') as UntypedFormControl;
    }

    get filterByHouseholdIncomeGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByHouseholdIncomeGrowth') as UntypedFormControl;
    }

    get filterByHouseValueGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByHouseValueGrowth') as UntypedFormControl;
    }

    get filterByJobGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByJobGrowth') as UntypedFormControl;
    }

    get filterByFailingPopulationGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByFailingPopulationGrowth') as UntypedFormControl;
    }

    get filterByFailingCrimeIndex(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByFailingCrimeIndex') as UntypedFormControl;
    }

    get filterByFailingHouseholdIncomeGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByFailingHouseholdIncomeGrowth') as UntypedFormControl;
    }

    get filterByFailingHouseValueGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByFailingHouseValueGrowth') as UntypedFormControl;
    }

    get filterByFailingJobGrowth(): UntypedFormControl {
        return this.citiesSearchForm.get('filterByFailingJobGrowth') as UntypedFormControl;
    }
}
