using AutoMapper;
using ContactsAPI.Models;
using ContactsAPI.DTOs;
using ContactsAPI.Repositories;

namespace ContactsAPI.Services;

public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ContactPageDto> GetContactsAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100; // Maximum page size limit

            return await _repository.GetContactsAsync(page, pageSize);
        }

        public async Task<ContactDto> GetContactByIdAsync(int id)
        {
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null)
                return null;

            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto)
        {
            if (await _repository.EmailExistsAsync(createContactDto.Email))
            {
                throw new InvalidOperationException($"Email {createContactDto.Email} is already in use");
            }

            var contact = _mapper.Map<Contact>(createContactDto);
            var createdContact = await _repository.CreateContactAsync(contact);
            return _mapper.Map<ContactDto>(createdContact);
        }

        public async Task<ContactDto> UpdateContactAsync(int id, UpdateContactDto updateContactDto)
        {
            var existingContact = await _repository.GetContactByIdAsync(id);
            if (existingContact == null)
                return null;

            // Check if new email is already in use by another contact
            if (existingContact.Email != updateContactDto.Email)
            {
                if (await _repository.EmailExistsAsync(updateContactDto.Email, id))
                {
                    throw new InvalidOperationException($"Email {updateContactDto.Email} is already in use");
                }
            }

            _mapper.Map(updateContactDto, existingContact);
            var updatedContact = await _repository.UpdateContactAsync(existingContact);
            return _mapper.Map<ContactDto>(updatedContact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            return await _repository.DeleteContactAsync(id);
        }
    }