using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models.DTO.MedCardDTO
{
    public class MedCardDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Desc { get; set; }

        public DateTime? ChangeDate { get; set; }
          public string ChangeUserId { get; set; }
    }
}
