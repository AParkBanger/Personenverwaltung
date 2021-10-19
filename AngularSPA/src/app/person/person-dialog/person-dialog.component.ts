import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { PersonsService } from 'src/app/core/api/v1';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Models}

@Component({
  selector: 'app-person-dialog',
  templateUrl: './person-dialog.component.html',
  styleUrls: ['./person-dialog.component.scss'],
})
export class PersonDialogComponent implements OnInit {
  private personService: PersonsService;

  public dialogRef: MatDialogRef<PersonDialogComponent>;

  constructor() {}

  ngOnInit(): void {}

  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    birth: new FormControl(''),
    sex: new FormControl('1'),
  });

  initializeFormGroup() {
    this.form.setValue({
      $key: null,
      firstName: '',
      lastName: '',
      birth: '',
      sex: '1'
    });
  }

  onSubmit() { 

    this.personService.apiPersonsPost()
    this.onClose();
  }

  onClose(){
    this.dialogRef.close();
  }
}
