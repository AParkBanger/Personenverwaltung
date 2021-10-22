import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PersonDTO, PersonsService } from 'src/app/@core/client';
import { FormGroup } from '@angular/forms';
@Component({
  selector: 'app-person-dialog',
  templateUrl: './person-dialog.component.html',
  styleUrls: ['./person-dialog.component.scss'],
})
export class PersonDialogComponent implements OnInit {
  public person: PersonDTO = new PersonDTO({ id: 0 });
  public form: FormGroup = new FormGroup({});

  constructor(
    private personService: PersonsService,
    private dialogRef: MatDialogRef<PersonDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: PersonDTO
  ) {
    if (this.data != null) {
      this.person = new PersonDTO(this.data);
    }
  }

  ngOnInit(): void {
    this.form = this.person.getFormGroup();
  }

  public onSubmit() {
    if (this.person.id == 0) {
      this.person = new PersonDTO(this.form.value);
      this.personService.apiPersonsPost(this.person).subscribe(
        (result) => {
          this.onClose(result);
        },
        (error) => {
          console.error(error);
        }
      );
    } else {
      this.person = new PersonDTO(this.form.value);
      this.personService.apiPersonsIdPut(this.person.id, this.person).subscribe(
        (result) => {
          console.log(result);
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
