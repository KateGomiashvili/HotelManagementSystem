using AutoMapper;
using HMS.Models.Dtos.Booking;
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
            CreateMap<BookingForCreatingDto, Booking>();
            CreateMap<Booking, BookingForGettingDto>();
            
            CreateMap<Hotel, HotelForGettingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<HotelForGettingDto, Hotel>();
            CreateMap<RegistrationRequestDto, ApplicationUser>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            
            .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Role.ToLower() == "manager" ? new Manager() : null))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
            src.Role.ToLower() == "guest"
                ? new string(src.PhoneNumber.Where(char.IsDigit).ToArray()) // Use phone number (digits only) as username
                : src.FirstName.Trim()));
            
            CreateMap<Manager, ManagerDto>()
                .ForMember(dest=> dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Room, RoomForGettingDto>();
        }
    }
}
