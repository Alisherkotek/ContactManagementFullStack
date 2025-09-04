namespace ContactsAPI.DTOs;

public class ContactPageDto
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public List<ContactDto> Items { get; set; }
}