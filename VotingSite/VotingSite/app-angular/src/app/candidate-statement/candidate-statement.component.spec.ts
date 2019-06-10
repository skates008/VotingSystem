import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateStatementComponent } from './candidate-statement.component';

describe('CandidateStatementComponent', () => {
  let component: CandidateStatementComponent;
  let fixture: ComponentFixture<CandidateStatementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateStatementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
