using ContactsAPI.Models;
using ContactsAPI.DTOs;

namespace ContactsAPI.Repositories;

public interface IContactRepository
{
    Task<ContactPageDto> GetContactsAsync(int page, int pageSize);
    Task<Contact> GetContactByIdAsync(int id);
    Task<Contact> CreateContactAsync(Contact contact);
    Task<Contact> UpdateContactAsync(Contact contact);
    Task<bool> DeleteContactAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
}