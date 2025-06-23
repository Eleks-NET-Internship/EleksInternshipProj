import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotifySpaceComponent } from './notify-space.component';

describe('NotifySpaceComponent', () => {
  let component: NotifySpaceComponent;
  let fixture: ComponentFixture<NotifySpaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NotifySpaceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotifySpaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
