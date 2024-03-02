using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthHub.Models
{
    public class MedCard
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser FK_UserId { get; set; }

        public string Desc { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateUserId { get; set; }
        [ForeignKey("CreateUserId")]
        public virtual AppUser FK_CreateUserId { get; set; }

        public DateTime? ChangeDate { get; set; }

        public string ChangeUserId { get; set; }
        [ForeignKey("ChangeUserId")]
        public virtual AppUser FK_ChangeUserId { get; set; }

    }
}
