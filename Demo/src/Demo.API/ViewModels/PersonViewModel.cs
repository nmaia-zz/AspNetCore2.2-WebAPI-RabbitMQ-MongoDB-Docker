using System.Collections.Generic;

namespace Demo.API.ViewModels
{
    public class PersonViewModel : EntityBaseViewModel
    {
        public PersonViewModel()
        {
            Filiation = new PersonViewModel[2];
            Children = new List<PersonViewModel>();
        }
            

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SkinColor { get; set; }
        public PersonViewModel[] Filiation { get; set; }
        public IEnumerable<PersonViewModel> Children { get; set; }
        public string Schooling { get; set; }        
    }
}
