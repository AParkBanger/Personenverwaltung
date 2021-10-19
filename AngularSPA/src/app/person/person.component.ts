import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogConfig } from '@angular/material/dialog';
import { PersonDialogComponent } from './person-dialog/person-dialog.component';
import { PersonsService } from '../core/api/v1';

@Component({
  selector: 'app-person',
  templateUrl: './person.component.html',
  styleUrls: ['./person.component.scss'],
})
export class PersonComponent implements OnInit {
  public listData: MatTableDataSource<any>;
  public displayedColumns: string[];
  public searchKey: string;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private personService: PersonsService,
    private dialog: MatDialog
  ) {
    this.displayedColumns = [
      'firstName',
      'lastName',
      'birth',
      'sex',
      'actionsEdit',
      'actionsDelete',
    ];

    var test = this.personService.apiPersonsGet();

    console.log(test);

    this.personService.apiPersonsGet().subscribe(
      (result) => {
        this.listData = new MatTableDataSource(result);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
      },
      (error) => console.error(error)
    );
  }

  ngOnInit(): void {}

  onSearchClear() {
    this.searchKey = '';
    this.applyFilter();
  }

  applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  onCreate() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true; // springt direkt in die erste Textbox
    //dialogConfig.width = "60%";
    this.dialog.open(PersonDialogComponent, dialogConfig);
  }
}
