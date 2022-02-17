import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
  GroupDTO,
  GroupService,
  PersonDTO,
  PersonsService,
} from 'src/app/@core/client';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-group-dialog',
  templateUrl: './group-dialog.component.html',
  styleUrls: ['./group-dialog.component.scss'],
})
export class GroupDialogComponent implements OnInit {
  public group: GroupDTO = new GroupDTO({ id: 0 });
  public form: FormGroup = new FormGroup({});
  public persons: PersonDTO[];
  public selectedPersonIds: number[];

  constructor(
    private groupService: GroupService,
    private personService: PersonsService,
    private dialogRef: MatDialogRef<GroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: GroupDTO
  ) {
    if (this.data != null) {
      this.group = new GroupDTO(this.data);
      this.selectedPersonIds = this.group.persons.map(({ id }) => id);
    } else {
      this.selectedPersonIds = [];
    }

    this.personService.apiPersonsGet().subscribe((result) => {
      this.persons = result as PersonDTO[];
    });
  }

  ngOnInit(): void {
    this.form = this.group.getFormGroup();
  }

  public onSubmit() {
    this.group = new GroupDTO(this.form.value);
    this.group.persons = [];

    this.selectedPersonIds.forEach((x) =>
      this.group.persons.push(
        new PersonDTO(this.persons.find((y) => y.id == x))
      )
    );

    if (this.group.id == 0) {
      console.log(this.group);
      console.log(this.group);
      this.groupService
        .apiGroupPost(this.group.name, this.selectedPersonIds)
        .subscribe(
          (result) => {
            this.onClose(result);
          },
          (error) => {
            console.error(error);
          }
        );
    } else {
      this.groupService
        .apiGroupIdPut(this.group.id, this.group.name, this.selectedPersonIds)
        .subscribe(
          (result) => {
            this.onClose(result);
          },
          (error) => {
            console.error(error);
          }
        );
    }
  }

  public onClose(result?) {
    this.form.reset();
    this.dialogRef.close(result);
  }
}
