using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TotvsChallengePoC.Entities
{
    public class Client
    {
        public Client(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string LastName { get; set; }

    }
}
