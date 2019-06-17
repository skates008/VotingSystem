import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  model: any = null;

  formGroup = new FormGroup({
    ElectionId: new FormControl(),
    LoginId: new FormControl(null,
      [Validators.required, Validators.minLength(8), Validators.maxLength(8), Validators.pattern('\\d+')]),
    LoginPin: new FormControl(null,
      [Validators.required, Validators.minLength(4), Validators.maxLength(4), Validators.pattern('\\d+')])
  });

  validationMessages = {
    LoginId: {
      Required: 'Please enter the PIN number.',
      MinLength: 'Your PIN number must be at least 8 digits.',
      MaxLength: 'Your PIN number should only be 8 digits.',
      RegularExpression: 'Your PIN number must only be numbers.',
    },
    LoginPin: {
      Required: 'Please enter the last 4 digits of your SSN.',
      MinLength: 'The last 4 digits of your SSN should be at least 4 digits.',
      MaxLength: 'The last 4 digits of your SSN should only be 4 digits.',
      RegularExpression: 'The last 4 digits of your SSN should only be numbers.'
    }
  };

  constructor(private http: HttpClient) {
    this.model = JSON.parse((document.getElementById('model') as any).value);
    this.formGroup.patchValue(this.model);
  }

  ngOnInit() {
  }

  submitted: boolean = false;
  errorMessage: string = '';

  submit() {
    this.submitted = true;
    this.errorMessage = '';

    if (this.formGroup.valid) {
      return this.http.post('Home/ngIndex', this.formGroup.value).subscribe(
        (value: any) => {
          if (value.error) {
            this.errorMessage = value.error;
          }
        },
        (response: any) => {
          if (response.status === 200 && response.statusText === 'OK') {
            if (response.url != null) {
              location.href = response.url;
            }
          }
        });
    }
  }

}
