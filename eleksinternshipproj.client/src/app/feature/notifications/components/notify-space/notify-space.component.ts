import { Component } from '@angular/core';
import { SpaceNotificationDTO } from '../../models/notifications-models';
import { SpaceDto, SpaceRenameDto } from '../../../spaces/models/spaces-models';
import { MatDialogRef } from '@angular/material/dialog';
import { SpacesService } from '../../../spaces/services/spaces.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-notify-space',
  templateUrl: './notify-space.component.html',
  styleUrl: './notify-space.component.css'
})
export class NotifySpaceComponent {
  notification: SpaceNotificationDTO = { title: "", message: "", spaceId: 0 };
  spaces: SpaceRenameDto[] = [];

  constructor(
    private dialogRef: MatDialogRef<NotifySpaceComponent>,
    private spacesServise: SpacesService,
  ) {

  }

  ngOnInit(): void {
    this.spacesServise
      .getSpacesWhereAdmin()
      .pipe(
        map((spaces: SpaceDto[]) =>
          spaces.map(space => ({
            id: space.id,
            name: space.name
          } as SpaceRenameDto))
        )
      )
      .subscribe(mappedSpaces => {
        this.spaces = mappedSpaces;
      })
      ;
  }

  save(): void {
    const titleTrimmed = this.notification.title.trim();
    const messageTrimmed = this.notification.message.trim();
    const id = this.notification.spaceId;
    if (titleTrimmed.length > 0 && messageTrimmed.length > 0) {
      this.dialogRef.close({ title: titleTrimmed, message: messageTrimmed, spaceId: id } as SpaceNotificationDTO);
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
