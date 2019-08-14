
namespace Veterinaria.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Pet
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Race { get; set; }

        public Owner Owner { get; set; }

        public PetType PetType { get; set; }

        [Display(Name = "Born")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Born { get; set; }

        public string Remarks { get; set; }

        public ICollection<History> Histories { get; set; }

        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";

        [Display(Name = "Nacimiento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => Born.ToLocalTime();
    }

}
