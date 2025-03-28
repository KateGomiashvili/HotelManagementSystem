using HMS.Service.Exceptions;
using System.Net;

namespace HMS.Api.Middleware
{
    public class ExceptionHandlingMidlleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMidlleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            ApiResponse response = new();

            switch (ex)
            {
                case AmbigousNameException:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.Conflict);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
                case BadRequestException:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
                case NotFoundException:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
                case IncorrectPasswordException:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
                case ConflictException:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.Conflict);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
                default:
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
                    response.Message = ex.Message;
                    response.IsSuccess = false;
                    response.Result = null;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
