import { Injectable } from '@angular/core';

@Injectable()
export class ItemCloneService<T> {
    private currentItem: T;

    public setCloneItem(item: T) {
        this.currentItem = this.clone(item);
    }

    public getCloneItem(): T {
        return this.currentItem;
    }

    private clone(item: T): T {
        return JSON.parse(JSON.stringify(item));
    }
}
