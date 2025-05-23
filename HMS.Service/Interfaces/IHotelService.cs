﻿

using HMS.Models.Dtos.Hotels;

namespace HMS.Service.Interfaces
{
    public interface IHotelService
    {
        Task<List<HotelForGettingDto>> GetMultipleHotels(int pageNumber, int pageSize);
        Task<List<HotelForGettingDto>> GetHotelsByFilter(string? cityName, string? countryName, int pageNumber, int pageSize);
        Task<HotelForGettingDto> GetSingleHotel(int teacherId);
        Task AddNewHotel(HotelForCreatingDto hotelForCreatingDto);
        Task UpdateHotel(HotelForUpdatingDto hotelForUpdatingDto);
        Task DeleteHotel(int hotelId);
        Task SaveHotel();
        Task<bool> ExistsAsync(int value);
    }
}
