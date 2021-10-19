import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Person, PersonsService } from 'src/app/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-person-dialog',
  templateUrl: './person-dialog.component.html',
  styleUrls: ['./person-dialog.component.scss'],
})
export class PersonDialogComponent implements OnInit {
  public person: Person = new Person({ id: 0 }); // Normalerweise wäre es hier besser wenn es hier ein dto gäbe ohne ID, dadruch müsste ich sie nicht 0 setzen
  public form: FormGroup = new FormGroup({});

  constructor(
    private personService: PersonsService,
    private dialogRef: MatDialogRef<PersonDialogComponent>
  ) {
    this.form = this.person.getFormGroup();
  }

  ngOnInit(): void {}

  public onSubmit() {
    console.log(this.form.value);

    const createdPerson = new Person(this.form.value);
    this.personService.apiPersonsPost(createdPerson).subscribe();
    this.form.reset();
    this.onClose();
  }

  public onClose() {
    this.form.reset();
    this.dialogRef.close();
  }
}
