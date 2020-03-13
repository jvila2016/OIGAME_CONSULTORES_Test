namespace OIGAME_CONSULTORES_Test.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public int Id { get; set; }

        public int? CustomerTypeId { get; set; }

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

        public virtual CustomerType CustomerType { get; set; }

        //public string DetectLanguageBrowser() {

        //    string lang = Request.UserLanguages[0];
        //    if (lang.ToLower().Contains("es"))
        //    {
        //        //Idioma Español
        //    }
        //    else if (lang.ToLower().Contains("en"))
        //    {
        //        //Idioma Ingles
        //    }
        //}
    }
}
