import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/shared/header.component';
import { FooterComponent } from './components/shared/footer.component';
import { AuthService } from './core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, FooterComponent, CommonModule],
  template: `
    <div class="d-flex flex-column min-vh-100">
      <app-header *ngIf="showHeader"></app-header>
      <main class="flex-grow-1">
        <router-outlet></router-outlet>
      </main>
      <app-footer *ngIf="showHeader"></app-footer>
    </div>
  `
})
export class App {
  title = 'SymptomCheckerFrontend';
  showHeader = false;

  constructor(private authService: AuthService) {
    this.authService.currentUser$.subscribe(user => {
      this.showHeader = !!user;
    });
  }
}
