import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ReadProperty } from '../../dtos/reads/readProperty';

@Component({
  selector: 'app-attachments',
  templateUrl: './attachments.component.html',
  styleUrls: ['./attachments.component.css']
})
export class AttachmentsComponent implements OnInit {

  property: ReadProperty;
  description = 'Property Attachments';
  attachmentsFormGroup: FormGroup;
  selectedFiles: any;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<AttachmentsComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) { this.property = data.property; }


  ngOnInit(): void {
    this.attachmentsFormGroup = this.fb.group({
      file: ['', Validators.required]
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.dialogRef.close(true);
  }

  onFilesChange(event: any): void { }

  get filesCtrl(): FormControl {
    return this.attachmentsFormGroup.get('file') as FormControl;
  }

}
