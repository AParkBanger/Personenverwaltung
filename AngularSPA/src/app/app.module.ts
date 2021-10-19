import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedModule } from './shared/shared.module';
import { MaterialModule } from './material/material.module';
import { NavComponent } from './nav/nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { HomeComponent } from './home/home.component';
import { PersonComponent } from './person/person.component';
import {
  ApiModule,
  Configuration,
  ConfigurationParameters,
  GroupService,
  PersonsService,
} from './core/api/v1';
import { HttpClientModule } from '@angular/common/http';
import { PersonDialogComponent } from './person/person-dialog/person-dialog.component';
import { environment } from 'src/environments/environment';

export function apiConfigFactory(): Configuration {
  const params: ConfigurationParameters = {
    basePath: environment.basePath,
  };
  return new Configuration(params);
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    PersonComponent,
    PersonDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    MaterialModule,
    LayoutModule,
    HttpClientModule,
    ApiModule.forRoot(apiConfigFactory),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
