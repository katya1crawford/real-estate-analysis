import { Component, Input, AfterViewInit, OnInit, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort'; import { ReadRentRollItem } from 'src/app/modules/rental-properties/dtos/reads/readRentRollItem';
import { ReadRentRollSummary } from 'src/app/modules/rental-properties/dtos/reads/ReadRentRollSummary';
import { FileSaverService } from 'src/app/modules/rental-properties/services/file-saver.service';

@Component({
  selector: 'app-rent-roll-details',
  templateUrl: './rent-roll-details.component.html',
  styleUrls: ['./rent-roll-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RentRollDetailsComponent implements OnInit, AfterViewInit {

  @Input() rentRollSummary: ReadRentRollSummary;
  @Input() pageLoading = false;
  public dataSource: MatTableDataSource<ReadRentRollItem>;
  public displayedColumns: string[] = ['units', 'fp', 'sqft', 'brs', 'baths', 'vacant', 'renovated', 'contract-rent', 'other-income', 'mkt-rent', '%-mkt-rent', 'mtm', 'lease-start', 'lease-end', 'lease-term'];
  public totalRowFooter = ['units', 'sqft', 'brs', 'baths', 'vacant', 'renovated', 'contract-rent', 'other-income', 'mkt-rent', '%-mkt-rent', 'mtm', 'lease-start', 'lease-end', 'lease-term']
  public averageRowFooter = ['units', 'sqft', 'brs', 'baths', 'vacant', 'renovated', 'contract-rent', 'other-income', 'mkt-rent', '%-mkt-rent', 'mtm', 'lease-start', 'lease-end', 'lease-term']
  @ViewChild('paginator') paginator: MatPaginator;
  @ViewChild(MatSort) sort = new MatSort();

  constructor(private fileSaver: FileSaverService) { }


  ngAfterViewInit(): void {
    this.dataSource = new MatTableDataSource(this.rentRollSummary.rentRollItems);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnInit(): void {
    console.log(this.rentRollSummary.rentRollItems);
  }

  export(data: any) {
    this.fileSaver.downloadFile(data);
  }



}

