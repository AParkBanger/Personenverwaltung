import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogConfig } from '@angular/material/dialog';
import { PersonDialogComponent } from './person-dialog/person-dialog.component';
import { PersonDTO, PersonsService } from '../../@core/client';
import { ConfirmDialogModel } from '../../@core/components/dialogs/confirm-dialog/confirmDialogModel';
import { ConfirmDialogComponent } from '../../@core/components/dialogs/confirm-dialog/confirm-dialog.component';

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
    private dialog: MatDialog,
    private changeDetectorRefs: ChangeDetectorRef
  ) {
    this.displayedColumns = [
      'firstName',
      'lastName',
      'birth',
      'sex',
      'actionsEdit',
      'actionsDelete',
    ];
    this.refreshList();
  }

  ngOnInit(): void {}

  private refreshList() {
    this.personService.apiPersonsGet().subscribe(
      (result) => {
        this.listData = new MatTableDataSource(result);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
        this.changeDetectorRefs.detectChanges();
      },
      (error) => console.error(error)
    );
  }

  public onSearchClear() {
    this.searchKey = '';
    this.applyFilter();
  }

  public applyFilter() {
    this.listData.filter = this.searchKey.trim().toLowerCase();
  }

  public onCreate() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = false;
    let dialogRef = this.dialog.open(PersonDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((value) => {
      if (value != undefined) {
        const data = this.listData.data;
        data.push(value);
        this.listData.data = data;
      }
    });
  }

  public onEdit(element) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = false;
    dialogConfig.data = element;
    let dialogRef = this.dialog.open(PersonDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((value) => {
      if (value != undefined) {
        let index = this.listData.data.findIndex((x) => x.id == value.id);
        const data = this.listData.data;
        data[index] = value;
        this.listData.data = data;
      }
    });
  }

  public onDelete(element: PersonDTO) {
    const message = `Soll ${element.firstName} ${element.lastName} wirklich gelöscht werden?`;
    const dialogData = new ConfirmDialogModel('Aktion bestätigen', message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '400px',
      data: dialogData,
      autoFocus: false,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) {
        this.personService.apiPersonsIdDelete(element.id).subscribe(() => {
          const data = this.listData.data;
          const index = this.listData.data.indexOf(element);
          data.splice(index, 1);
          this.listData.data = data;
        });
      }
    });
  }
}
