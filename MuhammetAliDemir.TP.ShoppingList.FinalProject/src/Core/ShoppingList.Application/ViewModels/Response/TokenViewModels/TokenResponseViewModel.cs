namespace ShoppingList.Application.ViewModels.Response.TokenViewModels
{
    public class TokenResponseViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
