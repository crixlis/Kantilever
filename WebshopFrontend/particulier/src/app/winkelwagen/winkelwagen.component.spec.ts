/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { WinkelwagenComponent } from './winkelwagen.component';

describe('WinkelwagenComponent', () => {
  let component: WinkelwagenComponent;
  let fixture: ComponentFixture<WinkelwagenComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WinkelwagenComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WinkelwagenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
