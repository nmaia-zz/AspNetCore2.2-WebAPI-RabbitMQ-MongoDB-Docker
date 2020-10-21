using System.Collections.Generic;

namespace Demo.API.ViewModels
{
    public class PersonViewModel
    {
        public PersonViewModel()
            => this.Filiation = new PersonViewModel[2];

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SkinColor { get; set; }
        public PersonViewModel[] Filiation { get; set; }
        public IEnumerable<PersonViewModel> Children { get; set; }
        public string Schooling { get; set; }        
    }
}
