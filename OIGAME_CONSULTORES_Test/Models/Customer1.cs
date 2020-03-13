using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OIGAME_CONSULTORES_Test.Models
{
    public class Customer1
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^[A-Z0-9a-z.a-z;]*$", ErrorMessage = "Solo Alfa Numericos / Alpha Numeric Only")]
        public string Passport { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public bool Active { get; set; }

        
    }
}
