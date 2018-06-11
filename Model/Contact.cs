using System.ComponentModel.DataAnnotations;

namespace AspNetCore.ControllersAsFilters.Sample.Model
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
    }
}
