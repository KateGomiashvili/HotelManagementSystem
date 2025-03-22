using HMS.Models.Dtos.Identity;

namespace HMS.Service.Interfaces
{
    public interface IAuthService
    {
        Task Register(RegistrationRequestDto registrationRequestDto);
        //Task RegisterAdmin(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
