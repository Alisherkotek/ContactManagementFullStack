using Microsoft.AspNetCore.Mvc;
using ContactsAPI.DTOs;
using ContactsAPI.Services;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        // Gets all contacts with pagination
        [HttpGet]
        [ProducesResponseType(typeof(ContactPageDto), 200)]
        public async Task<IActionResult> GetContacts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var contacts = await _contactService.GetContactsAsync(page, pageSize);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contacts");
                return StatusCode(500, new { message = "An error occurred while fetching contacts" });
            }
        }

        // Gets a specific contact by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetContact(int id)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(id);
                if (contact == null)
                    return NotFound(new { message = $"Contact with ID {id} not found" });

                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting contact {ContactId}", id);
                return StatusCode(500, new { message = "An error occurred while fetching the contact" });
            }
        }

        // Creates a new contact
        [HttpPost]
        [ProducesResponseType(typeof(ContactDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactDto createContactDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var contact = await _contactService.CreateContactAsync(createContactDto);
                return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating contact");
                return StatusCode(500, new { message = "An error occurred while creating the contact" });
            }
        }

        // Updates an existing contact
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContactDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] UpdateContactDto updateContactDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var contact = await _contactService.UpdateContactAsync(id, updateContactDto);
                if (contact == null)
                    return NotFound(new { message = $"Contact with ID {id} not found" });

                return Ok(contact);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating contact {ContactId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the contact" });
            }
        }

        // Deletes a contact
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var result = await _contactService.DeleteContactAsync(id);
                if (!result)
                    return NotFound(new { message = $"Contact with ID {id} not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting contact {ContactId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the contact" });
            }
        }
    }
}