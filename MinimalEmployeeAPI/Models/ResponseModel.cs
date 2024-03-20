using EmployeeAPI.Abstractions;

namespace EmployeeAPI.Models
{
    public class ResponseModel : IResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
