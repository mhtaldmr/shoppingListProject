namespace ShoppingList.Application.ViewModels.Response.MainResponse
{
    public static class Result
    {
        public static Result<T> Fail<T>(T data, string message) => new(data, message, true); 
        public static Result<T> Success<T>(T data, string message) => new(data, message, false); 
    }

    public class Result<T>
    {
        public Result(T data, string message, bool error)
        {
            Data = data;
            Message = message;
            Error = error;
        }

        public T Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; } = false;
    }
}