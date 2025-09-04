import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Contact, CreateContact, UpdateContact, ContactPage } from '../models/contact.model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private apiUrl = 'https://localhost:7027/api/contacts';

  constructor(private http: HttpClient) { }

  getContacts(page: number = 1, pageSize: number = 10): Observable<ContactPage> {
    const params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    return this.http.get<ContactPage>(this.apiUrl, { params });
  }

  getContact(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.apiUrl}/${id}`);
  }

  createContact(contact: CreateContact): Observable<Contact> {
    return this.http.post<Contact>(this.apiUrl, contact);
  }

  updateContact(id: number, contact: UpdateContact): Observable<Contact> {
    return this.http.put<Contact>(`${this.apiUrl}/${id}`, contact);
  }

  deleteContact(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
