using HMS.Models.Dtos.Booking;
using HMS.Models.Dtos.Hotels;
using HMS.Models.Dtos.Rooms;
using HMS.Models.Entities;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Manager")]  //create hotel
        [HttpPost]
        public async Task<IActionResult> AddHotel([FromForm] HotelForCreatingDto model)  //Add hotel
        {
            await _hotelService.AddNewHotel(model);
            await _hotelService.SaveHotel();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 201, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelForUpdatingDto model)  //Update hotel
        {
            await _hotelService.UpdateHotel(model);
            await _hotelService.SaveHotel();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] int hotelId)  //delete hotel
        {
            await _hotelService.DeleteHotel(hotelId);
            await _hotelService.SaveHotel();
            ApiResponse response = new(ApiResponseMessage.successMessage, hotelId, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> Gethotels([FromQuery] int pageNumber, [FromQuery] int pageSize)  //get all hotels
        {
            var result = await _hotelService.GetMultipleHotels(pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{hotelId}")]  //get single hotel
        public async Task<IActionResult> GetSingleHotelById([FromRoute]int hotelId)
        {
            var result = await _hotelService.GetSingleHotel(hotelId);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);

        }

        [HttpGet("search")]  //Get hotels by location
        public async Task<IActionResult> GetHotelsByLocation([FromQuery] string? city, [FromQuery] string? country, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _hotelService.GetHotelsByFilter(city, country, pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize(Roles = "Guest")]
        [HttpPost("reserve")]  //add new reservation
        public async Task<IActionResult> Reserve([FromBody] BookingForCreatingDto model)
        {

            await _bookingService.AddNewReservation(model);
            await _bookingService.SaveBooking();

            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, model, 201, true);
            return StatusCode(201, response);
        }
        [Authorize(Roles = "Guest")]
        [HttpPut("reserve")]  //Update reservation
        public async Task<IActionResult> UpdateReservation([FromForm] int bookingId, [FromForm] DateTime newCheckinDate, [FromForm] DateTime newcheckOutDate)
        {
            await _bookingService.UpdateReservation(bookingId, newCheckinDate, newcheckOutDate);
            await _bookingService.SaveBooking();
            var result = await _bookingService.GetSingleReservation(bookingId);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("searchReservations")]   //get reservations by filter
        public async Task<IActionResult> SearchReservations(
            [FromQuery] string? guestId, [FromQuery] int? roomId, [FromQuery] DateTime? checkInDate, [FromQuery] DateTime? checkOutDate, int pageNumber, int pageSize)
        {

            var result = await _bookingService.GetReservationsByFilter(guestId, roomId, checkInDate, checkOutDate, pageNumber, pageSize);


            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, result, 201, true);
            return StatusCode(201, response);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("{hotelId}/rooms")]  //add room
        public async Task<IActionResult> AddRoom(int hotelId, [FromBody] RoomForCreatingDto model)
        {

            await _roomService.AddNewRoom(model);
            await _roomService.SaveRoom();

            ApiResponse response = new ApiResponse(ApiResponseMessage.successMessage, model, 201, true);
            return StatusCode(201, response);
        }
        [HttpGet("{hotelId}/rooms")]  //get rooms
        public async Task<IActionResult> GetRooms(int hotelId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = await _roomService.GetRoomsByHotelIdAsync(hotelId, pageNumber, pageSize);
            ApiResponse response = new(ApiResponseMessage.successMessage, result, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{hotelId}/rooms")]  //update room
        public async Task<IActionResult> UpdateRoom(RoomForUpdateDto model)
        {
            await _roomService.UpdateRoom(model);
            await _roomService.SaveRoom();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 200, isSuccess: true);
            return StatusCode(response.StatusCode, response);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{hotelId}/rooms")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            await _roomService.DeleteRoom(roomId);
            await _roomService.SaveRoom();
            ApiResponse response = new(ApiResponseMessage.successMessage, roomId, 204, isSuccess: true);
            return StatusCode(response.StatusCode, response);


        }
    }
}
