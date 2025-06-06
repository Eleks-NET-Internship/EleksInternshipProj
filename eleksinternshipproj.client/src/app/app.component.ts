import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth/auth.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  constructor(private auth: AuthService, private router: Router) {
    // ugly not secure, but should work
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const url = new URL(window.location.href);
        const token = url.searchParams.get('accessToken');

        if (token) {
          this.auth.setToken(token);

          const cleanedUrl = url.origin + url.pathname + url.hash;
          window.history.replaceState({}, '', cleanedUrl);
        }
      }
    })
  }

  ngOnInit(): void {
    //
  }
}
