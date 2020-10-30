using System.ComponentModel.DataAnnotations;

namespace Demo.API.ViewModels
{
    public class ResearchViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Region { get; set; }
        
        public PersonViewModel Person { get; set; }
    }
}
