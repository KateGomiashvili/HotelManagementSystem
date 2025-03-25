

using AutoMapper;
using Azure.Core;
using HMS.Models.Dtos.Identity;
using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Service.Exceptions;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private const string _adminRole = "Admin";
        private const string _managerRole = "Manager";
        private const string _guestRole = "GuestRole";
        public AuthService
            (
                ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IJwtTokenGenerator jwtTokenGenerator,
                IMapper mapper
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.UserName.ToLower().Trim() == loginRequestDto.UserName.ToLower().Trim());

            if (user is not null)
            {
                var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (!isValid)
                {
                    throw new IncorrectPasswordException();
                } 


                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                LoginResponseDto result = new() { Token = token };
                return result;
            }
            else
            {
                throw new NotFoundException($"User {loginRequestDto.UserName} not found");
            }
        }
        public async Task Register(RegistrationRequestDto registrationRequestDto)
        {
            
            if (registrationRequestDto == null)
            {
                throw new ArgumentNullException(nameof(registrationRequestDto));
            }

            
            var user = _mapper.Map<ApplicationUser>(registrationRequestDto);

            user.UserName = registrationRequestDto.Email; 
            user.Email = registrationRequestDto.Email;
            if (registrationRequestDto.Role == "Manager")
            {
                Manager managerUser = new Manager();
                managerUser.UserId = user.Id;
                managerUser.HotelId =  registrationRequestDto.HotelId ?? 1;
                user.Manager = managerUser;
            }
            else if (registrationRequestDto.Role == "Guest")
            {
                Guest guestUser = new Guest();
                guestUser.UserId = user.Id;
                user.Guest = guestUser;
            }

            // Create user with password
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (!result.Succeeded)
            {
                // Log or handle errors
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"User creation failed: {errors}");
            }

            // Ensure the role exists before assigning it
            if (!await _roleManager.RoleExistsAsync(registrationRequestDto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(registrationRequestDto.Role));
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(user, registrationRequestDto.Role);

        }

    

        



        public async Task RegisterAdmin(RegistrationRequestDto registrationRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registrationRequestDto);
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = await _context.ApplicationUsers
                    .FirstOrDefaultAsync(x => x.Email.ToLower().Trim() == registrationRequestDto.Email.ToLower().Trim());

                if (userToReturn is not null)
                {
                    if (!await _roleManager.RoleExistsAsync(_adminRole))
                        await _roleManager.CreateAsync(new IdentityRole(_adminRole));

                    await _userManager.AddToRoleAsync(userToReturn, _adminRole);
                }
            }
            else
            {
                throw new InvalidOperationException(result.Errors.FirstOrDefault().Description);
            }
        }
    }
}




