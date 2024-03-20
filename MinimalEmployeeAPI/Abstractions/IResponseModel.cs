namespace EmployeeAPI.Abstractions
{
    public interface IResponseModel
    {        
        public bool Success { get; set; }  
        public string Message { get; set; }
    }
}
