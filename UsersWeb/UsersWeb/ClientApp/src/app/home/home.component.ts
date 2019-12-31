import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserComponent } from './user.component';
import { ResponseMessage } from './responseMessage.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public User: UserComponent;
  public UsersId: number[];
  public username: string = "";
  public firstname: string = "";
  public lastname: string = "";
  public dateOfBirth: Date;
  public displayUsernameError: boolean = false;
  public displayResponseMessage: boolean = false;
  public responseMessage: ResponseMessage;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.http.get<number[]>("/controller/getusersid").subscribe(result => {
      this.UsersId = result;
    }, error => console.error(error));
  }

  getUser(userId: any) {
    this.http.get<UserComponent>("/controller/getUser/" + userId).subscribe(result => {
      this.User = result;
      this.displayUsernameError = false;
      this.displayResponseMessage = false;
      this.username = this.User.username;
      this.firstname = this.User.firstName;
      this.lastname = this.User.lastName;
      this.dateOfBirth = this.User.dateOfBirth;
    }, error => console.error(error));
  }

  onFocusOut() {
    if ((this.firstname != undefined && this.firstname != "" && this.lastname != undefined && this.lastname != "" ) && (this.username.includes(this.firstname) || this.username.includes(this.lastname))) {
      this.displayUsernameError = true;
    }
    else {
      this.displayUsernameError = false;
    }
  }

  submitUser() {
    this.User.username = this.username;
    this.User.firstName = this.firstname;
    this.User.lastName = this.lastname;
    this.User.dateOfBirth = this.dateOfBirth;

    this.http.post<ResponseMessage>("/controller/updateuser/", this.User).subscribe(result => {
      this.responseMessage = result
      if (this.responseMessage.isSuccessfull) {
        this.displayResponseMessage = true;

      }
      else {
        this.displayResponseMessage = true;
      }
    }, error => console.error(error));
  }

  modified() {
    this.displayResponseMessage = false;
  }

  getColor() {
    if (this.responseMessage.isSuccessfull) {
      return "green";
    }
    else {
      return "red";
    }
  }
}
