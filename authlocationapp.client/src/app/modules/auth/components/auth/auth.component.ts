import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Country, Province } from 'src/app/core/models/location.model';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from '../success-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  form!: FormGroup;
  locationForm!: FormGroup;
  showLocation: boolean = false;
  showProvince: boolean = false;
  passwordsMatch: boolean = true;
  countries: Country[] = [];
  provinces: Province[] = [];
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.pattern('^(?=.*[A-Z])(?=.*\\d).{6,}$'),
        ],
      ],
      confirmPassword: ['', Validators.required],
      agree: [false, Validators.requiredTrue],
    });

    this.locationForm = this.fb.group({
      country: ['', Validators.required],
      province: ['', Validators.required],
    });

    this.form.get('confirmPassword')?.valueChanges.subscribe(() => {
      this.checkPasswordsMatch();
    });
  }

  checkPasswordsMatch() {
    this.passwordsMatch =
      this.form.get('password')?.value ===
      this.form.get('confirmPassword')?.value;
  }

  nextStep() {
    if (this.form.valid && this.passwordsMatch) {
      this.showLocation = true;
      this.loadCountries();
    }
  }

  loadCountries() {
    this.authService.getCountries().subscribe({
      next: (data) => {
        this.countries = data;
        this.locationForm.reset();
      },
      error: () => {
        this.showError('Failed to load countries.');
      },
    });
  }

  loadProvinces() {
    const selectedCountryId = this.locationForm.get('country')?.value;
    if (!selectedCountryId) return;

    this.authService.getProvinces(selectedCountryId).subscribe({
      next: (data) => {
        this.provinces = data;
        this.locationForm.get('province')?.setValue('');
      },
      error: () => {
        this.showError('Failed to load provinces.');
      },
    });
  }

  onCountryChange() {
    this.loadProvinces();
    this.showProvince = true;
  }

  register() {
    if (this.locationForm.valid) {
      const requestData = {
        email: this.form.get('email')?.value,
        password: this.form.get('password')?.value,
        countryId: this.locationForm.get('country')?.value,
        provinceId: this.locationForm.get('province')?.value,
      };

      this.authService.register(requestData).subscribe({
        next: (userId) => {
          this.showDialog(
            'Success',
            `User registered successfully! User ID: ${userId}`,
            'success'
          );
        },
        error: (err) => {
          if (err.status === 500) {
            this.showDialog('Error', 'Internal server error', 'error');
          } else {
            this.showDialog('Error', err.error.error, 'error');
          }
        },
      });
    }
  }

  showDialog(title: string, message: string, status: string) {
    const dialogRef = this.dialog.open(SuccessDialogComponent, {
      data: { title, message, status },
    });

    dialogRef.afterClosed().subscribe(() => {
      this.showLocation = false;
      this.showProvince = false;
      if (status === 'success') {
        this.form.reset();
      }
    });
  }

  private showError(message: string) {
    this.snackBar.open(message, 'Close', {
      duration: 3000,
      panelClass: ['error-snackbar'],
    });
  }
}
