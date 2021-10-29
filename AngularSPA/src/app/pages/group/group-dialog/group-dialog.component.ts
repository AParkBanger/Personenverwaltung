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

  constructor(
    private groupService: GroupService,
    private personService: PersonsService,
    private dialogRef: MatDialogRef<GroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: GroupDTO
  ) {
    if (this.data != null) {
      this.group = new GroupDTO(this.data);
    }
    this.personService.apiPersonsGet().subscribe((result) => {
      this.persons = result as PersonDTO[];
    });

    console.log(this.group.persons);
  }

  ngOnInit(): void {
    this.form = this.getFormGroup();
  }

  public onSubmit() {
    if (this.group.id == 0) {
      this.group = new GroupDTO(this.form.value);
      console.log(this.group);
      console.log(this.group);
      this.groupService.apiGroupPost(this.group).subscribe(
        (result) => {
          this.onClose(result);
        },
        (error) => {
          console.error(error);
        }
      );
    } else {
      this.group = new GroupDTO(this.form.value);
      this.groupService.apiGroupIdPut(this.group.id, this.group).subscribe(
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

  public getFormGroup(): FormGroup {
    const formGroup: FormGroup = new FormGroup({
      id: new FormControl(this.group.id, []),
      name: new FormControl(this.group.name, []),
      persons: new FormControl(this.group.persons, []),
    });

    return formGroup;
  }
}
