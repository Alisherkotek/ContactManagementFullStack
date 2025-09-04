export interface Contact {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
  createdAt?: Date;
  updatedAt?: Date;
}

export interface CreateContact {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
}

export interface UpdateContact {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  email: string;
}

export interface ContactPage {
  currentPage: number;
  pageSize: number;
  totalPages: number;
  totalCount: number;
  items: Contact[];
}
