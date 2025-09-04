using AutoMapper;
using ContactsAPI.Models;
using ContactsAPI.DTOs;

namespace ContactsAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Contact to ContactDto
            CreateMap<Contact, ContactDto>();
            
            // CreateContactDto to Contact
            CreateMap<CreateContactDto, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            
            // UpdateContactDto to Contact
            CreateMap<UpdateContactDto, Contact>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}