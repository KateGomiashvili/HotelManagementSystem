using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string PhoneNumber {  get; set; }
        public string Email {  get; set; }
        public string IdentityNumber { get; set; }
        public Manager Manager { get; set; }
        public Guest Guest { get; set; }
    }
}
