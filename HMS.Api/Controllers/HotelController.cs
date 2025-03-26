﻿using HMS.Models.Dtos.Booking;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;
        public HotelController(IHotelService hotelService, IRoomService roomService, IBookingService bookingService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromForm] HotelForCreatingDto model)
        {
            await _hotelService.AddNewHotel(model);
            await _hotelService.SaveHotel();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelForUpdatingDto model)
        {
            await _hotelService.UpdateHotel(model);
            await _hotelService.SaveHotel();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet]
        public async Task<IActionResult> Gethotels([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _hotelService.GetMultipleHotels(pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetHotelsByLocation([FromQuery] string? city, [FromQuery] string? country, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _hotelService.GetHotelsByFilter(city, country, pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve([FromBody] BookingForCreatingDto model)
        {

            await _bookingService.AddNewReservation(model);
            await _bookingService.SaveBooking();

            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, model, 201, true);
            return StatusCode(201, response);
        }
        [HttpGet("searchReservations")]
        public async Task<IActionResult> SearchReservations(
            [FromQuery] string? guestId, [FromQuery] int? roomId, [FromQuery] DateTime? checkInDate, [FromQuery] DateTime? checkOutDate, int pageNumber, int pageSize)
        {

            var result = await _bookingService.GetReservationsByFilter(guestId, roomId, checkInDate, checkOutDate, pageNumber, pageSize);


            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, result, 201, true);
            return StatusCode(201, response);
        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddRoom(int hotelId, [FromBody] RoomForCreatingDto model)
        {
            // await _hotelService.ExistsAsync(hotelId); // If hotel does not exist, exception will be thrown automatically

            await _roomService.AddNewRoom(model);
            await _roomService.SaveRoom();

            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, model, 201, true);
            return StatusCode(201, response);
        }
        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetRooms(int hotelId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _roomService.GetRoomsByHotelIdAsync(hotelId, pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
    }
}
