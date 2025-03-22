

namespace HMS.Models.Dtos.Identity
{
    public class RegistrationRequestDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? HotelId { get; set; }

    }
}
