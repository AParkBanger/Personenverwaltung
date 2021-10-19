import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogConfig } from '@angular/material/dialog';
import { PersonDialogComponent } from './person-dialog/person-dialog.component';
import { Person, PersonsService } from '../core';
import { MatDialogRef } from '@angular/material/dialog';

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

  public onSearchClear() {
    this.searchKey = '';
    this.applyFilter();
  }

  public applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  public onCreate() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    //dialogConfig.width = '30%';
    this.dialog.open(PersonDialogComponent, dialogConfig);
  }

  public onEdit(element) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    let dialogRef: MatDialogRef<PersonDialogComponent> = this.dialog.open(
      PersonDialogComponent,
      dialogConfig
    );
    dialogRef.componentInstance.person = element as Person;
    //dialogConfig.width = '30%';
    this.dialog.open(PersonDialogComponent, dialogConfig);
  }
}
