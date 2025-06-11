import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth.service';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrl: './auth-callback.component.css'
})
export class AuthCallbackComponent {
  constructor(
    private route: ActivatedRoute,
    private auth: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: (params) => {
        const token = params['accessToken'];
        const returnUrl = params['returnUrl'] || '/spaces';

        if (token) {
          this.auth.setToken(token);

          this.router.navigateByUrl(returnUrl);
        } else {
          this.router.navigate(['/login']);
        }
      }
    })
  }
}
