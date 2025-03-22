using AutoMapper;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Identity;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
            CreateMap<Hotel, HotelForUpdatingDto>().ReverseMap();
            CreateMap<RoomForCreatingDto, Room>().ReverseMap();
            CreateMap<RoomForGettingDto, Room>().ReverseMap();
            CreateMap<HotelForCreatingDto, Hotel>().ReverseMap();
            CreateMap<RegistrationRequestDto, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
            //    src.Role.ToLower() == "guest"
            //        ? new string(src.PhoneNumber.Where(char.IsDigit).ToArray()) // Use phone number (digits only)
            //        : (src.Email) // Use email for other roles
            //))
            //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            //.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            //.ForMember(dest => dest.IdentityNumber, opt => opt.MapFrom(src => src.IdentityNumber))
            //.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            //.ForMember(dest => dest.Guest, opt => opt.MapFrom(src => src.Role.ToLower() == "guest" ? new Guest() : null))
            .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Role.ToLower() == "manager" ? new Manager() : null))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
            src.Role.ToLower() == "guest"
                ? new string(src.PhoneNumber.Where(char.IsDigit).ToArray()) // Use phone number (digits only) as username
                : src.FirstName.Trim())); 
            //.ForMember(dest => dest.UserName, options => options.MapFrom(src => src.Email))
            //.ForMember(dest => dest.NormalizedUserName, options => options.MapFrom(src => src.Email.ToUpper()))
            //.ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
            //.ForMember(dest => dest.NormalizedEmail, options => options.MapFrom(src => src.Email.ToUpper()));
        }
    }
}
