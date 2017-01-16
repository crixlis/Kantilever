import { Component} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

export class AppComponent {

  ToggleSideNav() {
    var sidebar = document.querySelector('#sidebar');

    if(sidebar.className == '' || !sidebar.className)
    {
      sidebar.className = 'active';
    }
    else
    { 
      sidebar.className = '';
    }
  }
  
}
