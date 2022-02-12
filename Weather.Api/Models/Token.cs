namespace Weather.Api.Models
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
    }
}
