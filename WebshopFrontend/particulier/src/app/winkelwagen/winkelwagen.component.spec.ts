/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WinkelWagenComponent } from './winkelwagen.component';

describe('ProductComponent', () => {
  let component: WinkelWagenComponent;
  let fixture: ComponentFixture<WinkelWagenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WinkelWagenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelWagenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
