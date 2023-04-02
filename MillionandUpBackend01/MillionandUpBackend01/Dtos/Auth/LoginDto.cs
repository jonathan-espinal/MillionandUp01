using System.ComponentModel.DataAnnotations;

namespace MillionandUpBackend01.Dtos.Auth {
    public class LoginDto {
        [Required]
        public string Username { get; set; } = null!;
    }
}
