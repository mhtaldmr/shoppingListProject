namespace ShoppingList.Client.Data
{
    public class BaseResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; } = false;
    }
}