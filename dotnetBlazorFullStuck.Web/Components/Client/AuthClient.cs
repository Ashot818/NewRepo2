using dotnetBlazorFullStuck.Web.Components.Models;

namespace dotnetBlazorFullStuck.Web.Components.Client
{
    public class AuthClient
    {
        private readonly HttpClient _httpClient;

        public AuthClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["ApiBaseUrl"]);

        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">The user registration request.</param>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> RegisterAsync(UserRegisterModel request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/register", request);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The user login request.</param>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> LoginAsync(LoginFormModel request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login", request);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/user/{id}");
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Refreshes the access token using the refresh token.
        /// </summary>
        /// <param name="request">The refresh token request.</param>
        /// <returns>The response from the server as a string.</returns>
        //public async Task<string> RefreshTokenAsync(RefreshTokenRequest request)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("api/refresh-token", request);
        //    return await response.Content.ReadAsStringAsync();
        //}



        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> GetCurrentUserAsync()
        {
            var response = await _httpClient.GetAsync("api/current-user");
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>The response from the server as a string.</returns>
        public async Task<string> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/user/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}