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
    LoginId: new FormControl(null, [Validators.required]),
    LoginPin: new FormControl(null, [Validators.required])
  });

  constructor(private http: HttpClient) {
    this.model = JSON.parse((<any>document.getElementById("model")).value);
  }

  ngOnInit() {
  }

  submit() {
    return this.http.post("Home/ngIndex", this.formGroup.value).subscribe(
      (data: any) => {
        debugger;
      },
      (error: any) => {
        debugger;

      });


  }

}
