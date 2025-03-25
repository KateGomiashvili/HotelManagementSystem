using AutoMapper;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;
using HMS.Repository.Data;
using HMS.Repository.Interfaces;
using HMS.Service.Exceptions;
using HMS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Service.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

       

        public HotelService(IHotelRepository hotelRepository, IMapper mapper, ApplicationDbContext context)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _context = context;
            
        }
        public async Task AddNewHotel(HotelForCreatingDto hotelForCreatingDto)
        {
            if (hotelForCreatingDto is null)
            {
                throw new BadRequestException($"{hotelForCreatingDto} is an invalid argument");
            }
            var hotelWithSameName = await _hotelRepository.GetAsync(x => x.Name.ToLower().Trim() == hotelForCreatingDto.Name.ToLower().Trim());
            if (hotelWithSameName is not null)
            {
                throw new AmbigousNameException();
            }
            var entityData = _mapper.Map<Hotel>(hotelForCreatingDto);
            await _hotelRepository.AddAsync(entityData);
        }

        public Task DeleteHotel(int hotelId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(int hotelId)
        {
            return await _hotelRepository.ExistsAsync(h => h.Id == hotelId);
        }

        public async Task<List<HotelForGettingDto>> GetMultipleHotels(int pageNumber, int pageSize)
        {
            var result = new List<HotelForGettingDto>();
            List<Hotel> entityData = await _hotelRepository
            .GetAllAsync(pageNumber, pageSize, includeProperties: "Managers,Rooms");


            if (entityData.Any())
            {
                var mappedData = _mapper.Map<List<HotelForGettingDto>>(entityData);
                result.AddRange(mappedData);
                return result;
            }
            else
            {
                throw new NotFoundException($"Hotels not found");
            }
        }
        public async Task<List<HotelForGettingDto>> GetHotelsByFilter(
            string? cityName,
            string? countryName,
            int pageNumber,
            int pageSize)
        {
            var result = new List<HotelForGettingDto>();
            if (string.IsNullOrWhiteSpace(cityName) && string.IsNullOrWhiteSpace(countryName))
            {
                throw new BadRequestException("At least one filter (city or country) must be provided");
            }
            List<Hotel> hotels = await _hotelRepository.GetAllAsync(pageNumber, pageSize);
            var mappedData = _mapper.Map<List<HotelForGettingDto>>(hotels);
            var query = mappedData.AsQueryable();
            if (!string.IsNullOrWhiteSpace(cityName))
            {
                query = query.Where(h => h.City.Equals(cityName, StringComparison.OrdinalIgnoreCase));
            }
            if(!string.IsNullOrWhiteSpace(countryName))
            {
                query = query.Where(h => h.Country.Equals(countryName, StringComparison.OrdinalIgnoreCase));
            }
            return query.ToList();












        }


        public Task<HotelForGettingDto> GetSingleHotel(int teacherId)
        {
            throw new NotImplementedException();
        }



        public async Task SaveHotel() => await _hotelRepository.Save();

        public async Task UpdateHotel(HotelForUpdatingDto hotelForUpdatingDto)
        {
            if (hotelForUpdatingDto is null)
                throw new BadRequestException($"{hotelForUpdatingDto} is an invalid argument");

            var entityData = _mapper.Map<Hotel>(hotelForUpdatingDto);
            await _hotelRepository.Update(entityData);
        }

    }
}
