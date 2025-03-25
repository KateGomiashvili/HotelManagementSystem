using AutoMapper;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Identity;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Dtos.UserDto;
using HMS.Models.Entities;
using Microsoft.AspNetCore.Routing.Constraints;
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
            //CreateMap<Hotel, HotelForGettingDto>().ReverseMap();
            //CreateMap<Hotel, HotelForGettingDto>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Map Hotel.Id to HotelForGettingDto.HotelId
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Map Hotel.Name to HotelForGettingDto.Name
            //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating)) // Map Hotel.Rating to HotelForGettingDto.Rating
            //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country)) // Map Hotel.Country to HotelForGettingDto.Country
            //.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City)) // Map Hotel.City to HotelForGettingDto.City
            //.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)) // Map Hotel.Address to HotelForGettingDto.Address
            //.ForMember(dest => dest.Managers, opt => opt.MapFrom(src => src.Managers)) // Map Hotel.Managers to HotelForGettingDto.Managers
            //.ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms)); // Map Hotel.Rooms to HotelForGettingDto.Rooms
            CreateMap<Hotel, HotelForGettingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
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
            CreateMap<Manager, ManagerDto>()
                .ForMember(dest=> dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Room, RoomForGettingDto>();
        }
    }
}
