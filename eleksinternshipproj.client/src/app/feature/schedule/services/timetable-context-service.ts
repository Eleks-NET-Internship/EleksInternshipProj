import {Injectable} from '@angular/core';
import {TimetableDto} from '../models/schedule-models';

@Injectable({ providedIn: 'root' })
export class TimetableContextService {
  currentTimetable: TimetableDto | null = null;
}
