import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_BASE_HREF } from '@angular/common';

import { MatPaginatorModule, MatProgressBarModule, MatSnackBarModule, MatSortModule, MatTableModule } from '@angular/material';
import { LoginComponent } from './login/login.component';
import { HelpComponent } from './help/help.component';
import { SettingsComponent } from './settings/settings.component';
import { BallotComponent } from './ballot/ballot.component';
import { ListComponent } from './list/list.component';
import { CandidateStatementComponent } from './candidate-statement/candidate-statement.component';
import { SubmitBallotComponent } from './submit-ballot/submit-ballot.component'

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HelpComponent,
    SettingsComponent,
    BallotComponent,
    ListComponent,
    CandidateStatementComponent,
    SubmitBallotComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule
  ],
  providers: [{ provide: APP_BASE_HREF, useValue: '/' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
