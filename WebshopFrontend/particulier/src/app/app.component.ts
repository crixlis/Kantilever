import {
  Component,
  OnChanges,
  Input,
  trigger,
  state,
  style,
  transition,
  animate
} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

    animations: [
    trigger('togglenavChanged', [
      state('true' , style({ opacity: 1, transform: 'scale(1.0)' })),
      state('false', style({ opacity: 0, transform: 'scale(0.0)'  })),
      transition('1 => 0', animate('300ms')),
      transition('0 => 1', animate('900ms'))
    ])
  ]

})


export class AppComponent implements OnChanges  {
  title = 'Kantilever!';

  @Input() isVisible : boolean = false;

  ngOnChanges() {

  }

  ToggleSideNav() {
    var sidebar = document.querySelector('#sidebar');
    if(sidebar.className == '')
    {
      sidebar.className = 'active';
    }
    else
    { 
      sidebar.className = '';
    }

    
  }
  
}
