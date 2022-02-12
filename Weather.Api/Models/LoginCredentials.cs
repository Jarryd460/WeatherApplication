namespace Weather.Api.Models
{
    public class LoginCredentials
    {
        /// <summary>
        /// Username
        /// </summary>
        /// <example>JonDoe</example>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>JonDoe123$</example>
        public string Password { get; set; }
    }
}
