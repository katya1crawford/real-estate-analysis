import { Component, Input } from '@angular/core';
import { ReadProperty } from '../../../../dtos/reads/readProperty';

@Component({
    selector: 'app-notes',
    templateUrl: './notes.component.html'
})
export class NotesComponent {
    @Input() public property: ReadProperty;
    @Input() public pageLoading: boolean;
}
