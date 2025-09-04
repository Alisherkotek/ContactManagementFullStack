using ContactsAPI.Data;
using Microsoft.EntityFrameworkCore;
using ContactsAPI.Models;
using ContactsAPI.DTOs;

namespace ContactsAPI.Repositories;

public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ContactPageDto> GetContactsAsync(int page, int pageSize)
        {
            var query = _context.Contacts.AsQueryable();
            
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            
            var contacts = await query
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ContactDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToListAsync();

            return new ContactPageDto
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Items = contacts
            };
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            contact.CreatedAt = DateTime.UtcNow;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            contact.UpdatedAt = DateTime.UtcNow;
            _context.Entry(contact).State = EntityState.Modified;
            _context.Entry(contact).Property(x => x.CreatedAt).IsModified = false;
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var query = _context.Contacts.Where(c => c.Email == email);
            
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);
            
            return await query.AnyAsync();
        }
    }