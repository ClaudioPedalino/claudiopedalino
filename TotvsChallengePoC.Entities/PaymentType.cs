using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotvsChallengePoC.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string Description { get; set; }
    }
}
