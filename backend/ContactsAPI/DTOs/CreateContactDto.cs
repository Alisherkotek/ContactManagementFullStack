using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.DTOs;

public class CreateContactDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters")]
    public string FirstName { get; set; }
        
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
    public string LastName { get; set; }
        
    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }
        
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
}