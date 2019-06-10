import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_BASE_HREF } from '@angular/common';

import { MatPaginatorModule, MatProgressBarModule, MatSnackBarModule, MatSortModule, MatTableModule } from '@angular/material'

@NgModule({
  declarations: [
    AppComponent
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
