import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { GroupDTO, GroupService, PersonDTO } from 'src/app/@core/client';
import { ConfirmDialogComponent } from 'src/app/@core/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogModel } from 'src/app/@core/components/dialogs/confirm-dialog/confirmDialogModel';
import { GroupDialogComponent } from './group-dialog/group-dialog.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss'],
})
export class GroupComponent implements OnInit {
  public listData: MatTableDataSource<any>;
  public displayedColumns: string[];
  public searchKey: string;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private groupService: GroupService,
    private dialog: MatDialog,
    private changeDetectorRefs: ChangeDetectorRef
  ) {
    this.displayedColumns = ['name', 'actionsEdit', 'actionsDelete'];

    this.refreshList();
  }

  ngOnInit(): void {}

  private refreshList() {
    this.groupService.apiGroupGet().subscribe(
      (result) => {
        this.listData = new MatTableDataSource(result);
        this.listData.sort = this.sort;
        this.listData.paginator = this.paginator;
        this.changeDetectorRefs.detectChanges();
      },
      (error) => console.error(error)
    );
  }

  // TODO: suche auslagern als extra Komponent
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
    const dialogRef = this.dialog.open(GroupDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((value) => {
      if (value != undefined) {
        const data = this.listData.data;
        data.push(value);
        this.listData.data = data;
      }
    });
  }

  /**
   * onEdit
   */
  public onEdit(element) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = false;
    dialogConfig.data = element;
    const dialogRef = this.dialog.open(GroupDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe((value) => {
      if (value != undefined) {
        const index = this.listData.data.findIndex((x) => x.id == value.id);
        const data = this.listData.data;
        data[index] = value;
        this.listData.data = data;
      }
    });
  }

  /**
   * onDelete
   */
  public onDelete(element: GroupDTO) {
    const message = `Soll  ${element.name} wirklich gelöscht werden?`;
    const dialogData = new ConfirmDialogModel('Aktion bestätigen', message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      maxWidth: '400px',
      data: dialogData,
      autoFocus: false,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) {
        this.groupService.apiGroupIdDelete(element.id).subscribe(() => {
          const data = this.listData.data;
          const index = this.listData.data.indexOf(element);
          data.splice(index, 1);
          this.listData.data = data;
        });
      }
    });
  }
}
