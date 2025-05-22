import { Injectable, ComponentFactoryResolver, Injector, ApplicationRef, NgZone, ComponentRef, Type } from '@angular/core';
import { ModalComponent } from './components/modal.component';
import { ModalOptions } from '../../dtos/modalOptions';


declare let $;

class ModalData {
    constructor(public modalElement: HTMLElement,
        public contentCmptRef: ComponentRef<any>,
        public modalCmptRef: ComponentRef<any>) { }

    public destroy() {
        this.contentCmptRef.destroy();
        this.modalCmptRef.destroy();

        this.contentCmptRef = null;
        this.modalCmptRef = null;
        this.modalElement = null;
    }
}

@Injectable()
export class ModalService {
    constructor(private applicationRef: ApplicationRef,
        private resolver: ComponentFactoryResolver,
        private injector: Injector,
        private zone: NgZone
    ) {
    }

    private modalDataList: ModalData[] = [];

    public show<T>(content: Type<T>, options?: ModalOptions): T {
        const modalData = this.loadComponents(content, options);
        this.modalDataList.push(modalData);

        this.zone.runOutsideAngular(() => {
            const modalJqueryElement = $(modalData.modalElement);
            modalJqueryElement.modal('show');

            const zIndex = 1040 + (10 * $('.modal:visible').length);
            modalJqueryElement.css('z-index', zIndex);

            setTimeout(function () {
                $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
            }, 0);

            modalJqueryElement.on('hidden.bs.modal', (event) => {
                this.afterModelHide(event.target);

                if ($('.modal:visible').length > 0) {
                    $(document.body).addClass('modal-open');
                }
            });
        });

        return modalData.contentCmptRef.instance as T;
    }

    public hide() {
        const modalData = this.modalDataList[this.modalDataList.length - 1];
        $(modalData.modalElement).modal('hide');
    }

    private afterModelHide(modalElement: HTMLElement) {
        $(modalElement).modal('dispose');

        this.zone.run(() => {
            const itemIndex = this.modalDataList.length - 1;
            const modalData = this.modalDataList[itemIndex];
            modalData.destroy();
            this.modalDataList.splice(itemIndex, 1);
        });
    }

    private loadComponents<T>(content: Type<T>, options?: ModalOptions): ModalData {
        const contentCmpFactory = this.resolver.resolveComponentFactory(content);
        const contentCmptRef = contentCmpFactory.create(this.injector);
        contentCmptRef.changeDetectorRef.detectChanges();
        this.applicationRef.attachView(contentCmptRef.hostView);

        const modalCmpFactory = this.resolver.resolveComponentFactory(ModalComponent);
        const modalCmptRef = modalCmpFactory.create(this.injector, [[contentCmptRef.location.nativeElement]]);
        modalCmptRef.changeDetectorRef.detectChanges();
        this.applicationRef.attachView(modalCmptRef.hostView);
        (modalCmptRef.instance as ModalComponent).setModelOptions(options);

        const modalElement = (modalCmptRef.location.nativeElement as HTMLElement).firstChild as HTMLElement;
        document.querySelector('body').appendChild(modalCmptRef.location.nativeElement);

        return new ModalData(modalElement, contentCmptRef, modalCmptRef);
    }
}
