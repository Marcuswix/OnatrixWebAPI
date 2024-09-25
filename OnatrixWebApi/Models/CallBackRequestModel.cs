using System.ComponentModel.DataAnnotations;

namespace OnatrixWebAPI.Models
{
    public class CallBackRequestModel
    {
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Message { get; set; } = null!;
    }
}
