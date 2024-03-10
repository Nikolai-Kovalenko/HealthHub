using System.ComponentModel.DataAnnotations;

namespace HealthHub.Models.DTO.UserDTO
{
    public class AppUserDTO
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

    }
}
