using System.ComponentModel.DataAnnotations;

namespace Demo.API.ViewModels
{
    public class FilterObjectViewModel
    {
        public string Region { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string SkinColor { get; set; }
        public string Schooling { get; set; }        
        public bool IsGrouped { get; set; }
    }
}
