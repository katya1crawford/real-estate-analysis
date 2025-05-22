import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { takeWhile } from 'rxjs/operators';
import { ReadProperty } from '../../dtos/reads/readProperty';
import { ReadValidationResult } from '../../../../shared/dtos/reads/readValidationResult';
import { MessagingService } from '../../../../shared/services/messaging.service';
import { ItemCloneService } from '../../../../shared/services/item-clone.service';
import { RentalPropertyService, PropertyStatusEnum } from '../../services/rental-property.service';
import { appConfig } from '../../../../app.config';
import { AddEditPropertyComponent } from '../../modals/add-edit-property/add-edit-property.component';
import { ConfirmComponent } from '../../../../shared/modals/confirm/confirm.component';
import { ConfirmModalContentDto } from '../../../../shared/dtos/confirmModalContentDto';
import { AttachmentsComponent } from '../../modals/attachments/attachments.component';
import { IncomeStatementComponent } from '../../modals/income-statement/income-statement.component';
import { MessageType } from '../../../../shared/enums/messageType';
import { ModalService } from '../../../../shared/services/modal/modal.service';

@Component({
    templateUrl: './rental-properties.component.html'
})
export class RentalPropertiesComponent implements OnInit, OnDestroy {
    public properties: ReadProperty[];
    public validationErrorResult: ReadValidationResult | null = null;
    public serverError = false;
    public propertiesLoading = true;
    public noImage = '/assets/shared/noImage.jpg';
    public propertyStatus = '';
    public propertyStatusFriendlyDisplay = '';

    private alive = true;
    private selectedProperty: ReadProperty;

    constructor(private titleService: Title,
        private route: ActivatedRoute,
        private router: Router,
        private messagingService: MessagingService,
        private modalService: ModalService,
        private itemCloneService: ItemCloneService<ReadProperty[]>,
        private propertyService: RentalPropertyService) {
        titleService.setTitle(`${appConfig.businessName}: Rentals`);
        this.subscribeToMessagingService();
    }

    ngOnInit(): void {
        this.route.paramMap
            .pipe(takeWhile(() => this.alive))
            .subscribe((params: ParamMap) => {
                const propertyStatus = params.get('propertyStatus');

                if (propertyStatus) {
                    switch (propertyStatus.toLowerCase()) {
                        case 'listing':
                            this.setProperties(PropertyStatusEnum.Listing);
                            this.propertyStatus = 'Listing';
                            this.propertyStatusFriendlyDisplay = 'Listing';
                            break;
                        case 'in-review':
                            this.setProperties(PropertyStatusEnum.InReview);
                            this.propertyStatus = 'InReview';
                            this.propertyStatusFriendlyDisplay = 'In Review';
                            break;
                        case 'purchased':
                            this.setProperties(PropertyStatusEnum.Purchased);
                            this.propertyStatus = 'Purchased';
                            this.propertyStatusFriendlyDisplay = 'Purchased';
                            break;
                        default:
                            this.router.navigate(['/rentals/listing']);
                            break;
                    }
                } else {
                    this.router.navigate(['/rentals/listing']);
                }
            });
    }

    ngOnDestroy() {
        this.alive = false;
    }

    public onAddNewClick(): void {
        const addModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        addModal.messageType = MessageType.newRentalPropertyAdded;
    }

    public onDelete(property: ReadProperty): void {
        this.selectedProperty = property;
        const confirmModal = this.modalService.show(ConfirmComponent);
        confirmModal.modalContent = new ConfirmModalContentDto('Confirm!', `Are you sure that you want to delete ${property.address.address} property?`, MessageType.deleteRentalProperty);
    }

    public onDetailsClick(property: ReadProperty): void {
        this.router.navigate(['/rental-property-details', property.id]);
    }

    public onAttachmentsClick(property: ReadProperty): void {
        this.selectedProperty = property;

        const propertyAttachmentsModal = this.modalService.show(AttachmentsComponent, { sizeClass: 'modal-lg' });
        propertyAttachmentsModal.property = this.selectedProperty;
    }

    public onIncomeStatementClick(property: ReadProperty): void {
        const incomeStatementModal = this.modalService.show(IncomeStatementComponent, { sizeClass: 'modal-lg' });
        incomeStatementModal.initialize(property);
    }

    public onEditClick(property: ReadProperty): void {
        this.selectedProperty = property;

        const editModal = this.modalService.show(AddEditPropertyComponent, { sizeClass: 'modal-xl' });
        editModal.setEditPropertyFormValues(property);
        editModal.messageType = MessageType.updatedRentalProperty;
    }

    public filterProperties(searchTerm: string): void {
        if (searchTerm) {
            const lowerCaseSearchTerm = searchTerm.toLowerCase();

            this.properties = this.itemCloneService.getCloneItem().filter(x => x.address.address.toLowerCase().indexOf(lowerCaseSearchTerm) > -1
                || x.address.city.toLowerCase().indexOf(lowerCaseSearchTerm) > -1
                || x.address.state.name.toLowerCase().indexOf(lowerCaseSearchTerm) > -1
                || x.address.zipCode.toLowerCase().indexOf(lowerCaseSearchTerm) > -1
                || x.propertyType.name.toLowerCase().indexOf(lowerCaseSearchTerm) > -1
                || x.financialSummary.annualCapRate.toString().indexOf(lowerCaseSearchTerm) > -1
                || x.financialSummary.annualCashOnCashRate.toString().indexOf(lowerCaseSearchTerm) > -1
                || x.financialSummary.annualCashFlow.toString().indexOf(lowerCaseSearchTerm) > -1
                || (x.notes ? x.notes.toLowerCase().indexOf(lowerCaseSearchTerm) > -1 : false))
                .sort(this.orderByIdDesc);
        } else {
            this.properties = this.itemCloneService.getCloneItem().sort(this.orderByIdDesc);
        }
    }

    private deleteProperty() {
        this.propertyService.deleteProperty(this.selectedProperty.id)
            .pipe(takeWhile(() => this.alive))
            .subscribe(() => {
                const itemIndex = this.properties.findIndex(x => x.id === this.selectedProperty.id);
                this.properties.splice(itemIndex, 1);
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }
                });
    }

    private setProperties(propertyStatus: PropertyStatusEnum) {
        this.propertyService.getByPropertyStatus(propertyStatus)
            .pipe(takeWhile(() => this.alive))
            .subscribe(properties => {
                this.propertiesLoading = false;
                this.itemCloneService.setCloneItem(properties.sort(this.orderByIdDesc));
                this.properties = this.itemCloneService.getCloneItem();
            },
                (error: HttpErrorResponse) => {
                    if (error.status === 400) {
                        this.validationErrorResult = error.error as ReadValidationResult;
                    } else {
                        this.serverError = true;
                    }

                    this.propertiesLoading = false;
                });
    }

    private updateProperty(updatedProperty: ReadProperty): void {
        const itemIndex = this.properties.findIndex(x => x.id === this.selectedProperty.id);
        this.properties.splice(itemIndex, 1);
        this.properties.push(updatedProperty);
        this.itemCloneService.setCloneItem(this.properties.sort(this.orderByIdDesc));
        this.properties = this.itemCloneService.getCloneItem();
    }

    private subscribeToMessagingService() {
        this.messagingService.messageStatus
            .pipe(takeWhile(() => this.alive))
            .subscribe((message) => {
                if (message.messageType === MessageType.newRentalPropertyAdded) {
                    this.properties.push(message.content as ReadProperty);
                    this.itemCloneService.setCloneItem(this.properties.sort(this.orderByIdDesc));
                    this.properties = this.itemCloneService.getCloneItem();
                }

                if (message.messageType === MessageType.deleteRentalProperty) {
                    this.deleteProperty();
                }

                if (message.messageType === MessageType.updatedRentalProperty) {
                    this.updateProperty(message.content as ReadProperty);
                }

                if (message.messageType === MessageType.deletedRentalPropertyThumbnailImage) {
                    this.selectedProperty.thumbnailImageBase64 = '';
                }
            });
    }

    private orderByIdDesc(a: ReadProperty, b: ReadProperty): number {
        if (a.id > b.id) {
            return -1;
        }

        if (a.id < b.id) {
            return 1;
        }

        return 0;
    }
}
