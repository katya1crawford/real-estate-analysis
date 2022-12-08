import { Component, Input, OnInit } from '@angular/core';
import { ReadProperty } from 'src/app/modules/rental-properties/dtos/reads/readProperty';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {


  @Input() public property: ReadProperty;
  @Input() public pageLoading: boolean;
  constructor() { }

  ngOnInit(): void {
  }

}
