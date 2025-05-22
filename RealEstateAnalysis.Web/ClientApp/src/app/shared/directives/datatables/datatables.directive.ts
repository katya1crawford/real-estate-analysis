import { Directive, ElementRef, Input, AfterViewInit, NgZone, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

declare var $: any;

@Directive({
    selector: '[appDatatables]'
})
export class DatatablesDirective implements AfterViewInit, OnDestroy {
    @Input() dtOptions: any;
    @Input() dtTrigger: Subject<any>;
    @Input() dtTitle = '';
    @Input() dtMessageTop = '';

    private datatable: any;
    private defaultDtOptions = {};

    constructor(private element: ElementRef, private zone: NgZone) { }

    ngAfterViewInit() {
        this.updateDtOptions();
        this.initializeDatatable();

        if (this.dtTrigger) {
            this.dtTrigger.subscribe(() => {
                this.updateDtOptions();
                this.initializeDatatable();
            });
        }
    }

    private updateDtOptions() {
        this.defaultDtOptions = {
            buttons: this.getButtons()
        };

        if (this.dtOptions) {
            Object.assign(this.defaultDtOptions, this.dtOptions);
        }
    }

    private initializeDatatable() {
        this.zone.runOutsideAngular(() => {
            this.datatable = $(this.element.nativeElement).DataTable(this.defaultDtOptions);
            const buttons = this.datatable.buttons().container();
            buttons.find('.btn-secondary').removeClass('btn btn-secondary');
            buttons.appendTo('.dataTables_wrapper .col-md-6:eq(0)');
        });
    }

    ngOnDestroy() {
        if (this.datatable) {
            this.datatable.destroy(true);
        }

        if (this.dtTrigger) {
            this.dtTrigger.unsubscribe();
        }
    }

    private getButtons(): any[] {
        const buttonCommon = {
            exportOptions: {
                format: {
                    body: function (data, row, column, node: HTMLElement) {
                        if ($(node).hasClass('dt-hidden')) {
                            return '';
                        }

                        return data;
                    },
                    footer: function (data, row, node: HTMLElement) {
                        if ($(node).hasClass('dt-hidden')) {
                            return '';
                        }

                        return data;
                    }
                }
            }
        };

        return [
            {
                extend: 'csvHtml5',
                text: '',
                tag: 'i',
                className: 'cursor-pointer fas fa-file-csv fa-2x me-5'
            },
            $.extend(true, {}, buttonCommon, {
                extend: 'pdfHtml5',
                text: '',
                orientation: 'landscape',
                title: this.dtTitle,
                footer: true,
                tag: 'i',
                messageTop: this.dtMessageTop,
                className: 'cursor-pointer fas fa-file-pdf fa-2x'
            })
        ];
    }
}
