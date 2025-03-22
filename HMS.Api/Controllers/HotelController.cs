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
        public HotelController(IHotelService hotelService, IRoomService roomService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
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
        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddRoom([FromForm] RoomForCreatingDto model)
        {
            await _roomService.AddNewRoom(model);
            await _roomService.SaveRoom();
            ApiResponse response = new(ApiResponseMessage.successMessage, model, 201, isSuccess: true);
            return StatusCode(Response.StatusCode, response);
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
