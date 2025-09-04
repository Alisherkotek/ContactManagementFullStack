using ContactsAPI.DTOs;

namespace ContactsAPI.Services;

public interface IContactService
{
    Task<ContactPageDto> GetContactsAsync(int page, int pageSize);
    Task<ContactDto> GetContactByIdAsync(int id);
    Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto);
    Task<ContactDto> UpdateContactAsync(int id, UpdateContactDto updateContactDto);
    Task<bool> DeleteContactAsync(int id);
}