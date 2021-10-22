import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ComponentsModule } from './components/components.module';
import { ApiModule, Configuration, ConfigurationParameters } from './client';
import { environment } from 'src/environments/environment';

export function apiConfigFactory(): Configuration {
  const params: ConfigurationParameters = {
    basePath: environment.API_BASE_PATH,
  };
  return new Configuration(params);
}

@NgModule({
  declarations: [],
  imports: [CommonModule, ComponentsModule, ApiModule],
  exports: [ComponentsModule, ApiModule],
})
export class coreModule {}
