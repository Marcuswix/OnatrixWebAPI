namespace OnatrixWebApi.Models
{
    public class ContactFormToSendModel
    {
        public string? FormName { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
