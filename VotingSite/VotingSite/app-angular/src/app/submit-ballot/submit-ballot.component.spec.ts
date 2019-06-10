import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitBallotComponent } from './submit-ballot.component';

describe('SubmitBallotComponent', () => {
  let component: SubmitBallotComponent;
  let fixture: ComponentFixture<SubmitBallotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitBallotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitBallotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
