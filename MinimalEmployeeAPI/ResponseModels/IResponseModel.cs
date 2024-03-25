namespace EmployeeAPI.ResponseModels
{
    public interface IResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
