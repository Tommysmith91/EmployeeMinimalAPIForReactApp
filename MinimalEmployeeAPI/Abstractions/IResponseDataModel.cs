namespace EmployeeAPI.Abstractions
{
    public interface IResponseDataModel<T> : IResponseModel
    {
       public T Data { get; set; }
    }
}
