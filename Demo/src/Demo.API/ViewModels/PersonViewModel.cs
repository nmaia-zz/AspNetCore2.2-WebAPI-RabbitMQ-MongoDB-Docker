using Demo.API.ViewModels.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.API.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {
            Filiation = new string[2];
            Children = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string SkinColor { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [FiliationArrayLenghtValidation(ErrorMessage = "The filiation field must have two names, one for the father and another for the mother).")]
        public string[] Filiation { get; set; }

        public IEnumerable<string> Children { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Schooling { get; set; }        
    }
}
