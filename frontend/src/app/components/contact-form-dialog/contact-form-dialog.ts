import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Contact } from '../../models/contact.model';

@Component({
  selector: 'app-contact-form-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './contact-form-dialog.html',
  styleUrl: './contact-form-dialog.css'
})
export class ContactFormDialogComponent implements OnInit {
  contactForm: FormGroup;
  isEditMode: boolean;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<ContactFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { mode: 'create' | 'edit', contact?: Contact }
  ) {
    this.isEditMode = data.mode === 'edit';

    this.contactForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/)]]
    });
  }

  ngOnInit(): void {
    if (this.isEditMode && this.data.contact) {
      this.contactForm.patchValue(this.data.contact);
    }
  }

  onSubmit(): void {
    if (this.contactForm.valid) {
      this.dialogRef.close(this.contactForm.value);
    } else {
      Object.keys(this.contactForm.controls).forEach(key => {
        this.contactForm.get(key)?.markAsTouched();
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  getErrorMessage(fieldName: string): string {
    const control = this.contactForm.get(fieldName);

    if (control?.hasError('required')) {
      return `${this.getFieldLabel(fieldName)} is required`;
    }

    if (control?.hasError('email')) {
      return 'Please enter a valid email address';
    }

    if (control?.hasError('pattern')) {
      return 'Please enter a valid phone number';
    }

    if (control?.hasError('maxlength')) {
      const maxLength = control.errors?.['maxlength'].requiredLength;
      return `${this.getFieldLabel(fieldName)} must be less than ${maxLength} characters`;
    }

    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      firstName: 'First name',
      lastName: 'Last name',
      email: 'Email',
      phoneNumber: 'Phone number'
    };
    return labels[fieldName] || fieldName;
  }
}
