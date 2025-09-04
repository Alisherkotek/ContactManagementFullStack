# Contact Management System

A full-stack web application for managing contacts with a .NET Core backend and Angular frontend.

## Features

-  View all contacts with pagination
-  Add new contacts
-  Edit existing contacts
-  Delete contacts
-  Email validation (no duplicates)
-  Responsive design

## Tech Stack

**Backend:**
- .NET 8.0
- Entity Framework Core
- SQL Server / In-Memory Database
- AutoMapper

**Frontend:**
- Angular 
- Angular Material
- TypeScript
- Reactive Forms

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 
- [Angular CLI](https://angular.io/cli)


## API Endpoints

- `GET /api/contacts` - Get all contacts (paginated)
- `GET /api/contacts/{id}` - Get contact by ID
- `POST /api/contacts` - Create new contact
- `PUT /api/contacts/{id}` - Update contact
- `DELETE /api/contacts/{id}` - Delete contact

## Project Structure

```
contact-management/
├── backend/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Repositories/
│   └── Program.cs
├── frontend/
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   ├── models/
│   │   │   └── services/
│   └── angular.json
└── README.md
```
