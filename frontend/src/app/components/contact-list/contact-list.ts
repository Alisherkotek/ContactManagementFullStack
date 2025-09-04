import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { ContactService } from '../../services/contact.service';
import { Contact } from '../../models/contact.model';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatTooltipModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './contact-list.html',
  styleUrl: './contact-list.css'
})
export class ContactListComponent implements OnInit {
  contacts: Contact[] = [];
  totalContacts = 0;
  pageSize = 10;
  currentPage = 1;
  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'phoneNumber', 'actions'];
  loading = false;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private contactService: ContactService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadContacts();
  }

  loadContacts(): void {
    this.loading = true;
    this.contactService.getContacts(this.currentPage, this.pageSize)
      .subscribe({
        next: (data: any) => {
          this.contacts = data.items;
          this.totalContacts = data.totalCount;
          this.loading = false;
        },
        error: (error: any) => {
          console.error('Error loading contacts:', error);
          this.snackBar.open('Error loading contacts. Check if backend is running.', 'Close', { duration: 5000 });
          this.loading = false;
        }
      });
  }

  onPageChange(event: PageEvent): void {
    this.pageSize = event.pageSize;
    this.currentPage = event.pageIndex + 1;
    this.loadContacts();
  }

  async openAddDialog(): Promise<void> {
    const { ContactFormDialogComponent } = await import('../contact-form-dialog/contact-form-dialog');

    const dialogRef = this.dialog.open(ContactFormDialogComponent, {
      width: '500px',
      data: { mode: 'create' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactService.createContact(result).subscribe({
          next: () => {
            this.snackBar.open('Contact created successfully', 'Close', { duration: 3000 });
            this.loadContacts();
          },
          error: (error: any) => {
            console.error('Error creating contact:', error);
            this.snackBar.open(error.error?.message || 'Error creating contact', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  async openEditDialog(contact: Contact): Promise<void> {
    const { ContactFormDialogComponent } = await import('../contact-form-dialog/contact-form-dialog');

    const dialogRef = this.dialog.open(ContactFormDialogComponent, {
      width: '500px',
      data: { mode: 'edit', contact: { ...contact } }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactService.updateContact(contact.id, result).subscribe({
          next: () => {
            this.snackBar.open('Contact updated successfully', 'Close', { duration: 3000 });
            this.loadContacts();
          },
          error: (error: any) => {
            console.error('Error updating contact:', error);
            this.snackBar.open(error.error?.message || 'Error updating contact', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }

  async deleteContact(contact: Contact): Promise<void> {
    const { ConfirmDialogComponent } = await import('../confirm-dialog/confirm-dialog');

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Confirm Delete',
        message: `Are you sure you want to delete ${contact.firstName} ${contact.lastName}?`
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.contactService.deleteContact(contact.id).subscribe({
          next: () => {
            this.snackBar.open('Contact deleted successfully', 'Close', { duration: 3000 });
            this.loadContacts();
          },
          error: (error: any) => {
            console.error('Error deleting contact:', error);
            this.snackBar.open('Error deleting contact', 'Close', { duration: 3000 });
          }
        });
      }
    });
  }
}
