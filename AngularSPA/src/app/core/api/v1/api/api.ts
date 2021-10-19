export * from './group.service';
import { GroupService } from './group.service';
export * from './persons.service';
import { PersonsService } from './persons.service';
export const APIS = [GroupService, PersonsService];
