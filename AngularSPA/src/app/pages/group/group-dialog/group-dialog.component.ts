import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GroupDTO, GroupService } from 'src/app/@core/client';

@Component({
  selector: 'app-group-dialog',
  templateUrl: './group-dialog.component.html',
  styleUrls: ['./group-dialog.component.scss'],
})
export class GroupDialogComponent implements OnInit {
  public group: GroupDTO = new GroupDTO({ id: 0 });
  public form: FormGroup = new FormGroup({});

  constructor(
    private groupService: GroupService,
    private dialogRef: MatDialogRef<GroupDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: GroupDTO
  ) {
    if (this.data != null) {
      this.group = new GroupDTO(this.data);
    }
  }

  ngOnInit(): void {
    this.group = new GroupDTO(this.data);
  }

  public onSubmit() {
    if (this.group.id == 0) {
      this.group = new GroupDTO(this.form.value);
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
}
