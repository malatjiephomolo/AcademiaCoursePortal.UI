using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaCoursePortal.UI.Models;
using Blazored.LocalStorage;

namespace AcademiaCoursePortal.UI.Services
{
    public class EnrollmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _enrollmentsUrl = "https://localhost:7191/api/enrollments";

        private readonly ILocalStorageService _localStorage;

        public EnrollmentService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Get all enrollments
        public async Task<List<Enrollment>> GetEnrollmentsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_enrollmentsUrl);
                response.EnsureSuccessStatusCode();
                var enrollments = await response.Content.ReadFromJsonAsync<List<Enrollment>>();
                return enrollments ?? new List<Enrollment>(); // Return empty list if null
            }
            catch (HttpRequestException e)
            {
                // Log or handle the error as needed
                Console.WriteLine($"Request error: {e.Message}");
                return new List<Enrollment>(); // Return empty list on error
            }
        }

        // Add a new enrollment
        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            try
            {

                var request = new HttpRequestMessage(HttpMethod.Post, _enrollmentsUrl)
                {
                    Content = JsonContent.Create(enrollment) // Serialize the enrollment object
                };

                // Add custom headers
                request.Headers.Add("Authorization", $"Bearer {await _localStorage.GetItemAsync<string>("authToken")}");

                // Send the request
                var response = await _httpClient.SendAsync(request);
                Console.WriteLine(response);

            }
            catch (HttpRequestException e)
            {
                // Log or handle the error as needed
                Console.WriteLine($"Request error: {e.Message}");
            }
        }

        // Delete an enrollment by ID
        public async Task DeleteEnrollmentAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_enrollmentsUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Log or handle the error as needed
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
