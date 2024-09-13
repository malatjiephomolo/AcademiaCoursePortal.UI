using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcademiaCoursePortal.UI.Models;

namespace AcademiaCoursePortal.Client.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _coursesUrl = "api/courses";

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all courses
        public async Task<List<Course>> GetCoursesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_coursesUrl);
                response.EnsureSuccessStatusCode();
                var courses = await response.Content.ReadFromJsonAsync<List<Course>>();
                return courses ?? new List<Course>(); // Handle null response
            }
            catch (HttpRequestException e)
            {
                // Handle or log the error
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Handle or log the error
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }

        // Get a single course by ID
        public async Task<Course> GetCourseByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_coursesUrl}/{id}");
                response.EnsureSuccessStatusCode();
                var course = await response.Content.ReadFromJsonAsync<Course>();
                return course ?? new Course(); // Handle null response
            }
            catch (HttpRequestException e)
            {
                // Handle or log the error
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Handle or log the error
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }

        // Add a new course
        public async Task AddCourseAsync(Course course)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_coursesUrl, course);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Handle or log the error
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Handle or log the error
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }

        // Update an existing course
        public async Task UpdateCourseAsync(int id, Course course)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_coursesUrl}/{id}", course);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Handle or log the error
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Handle or log the error
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }

        // Delete a course by ID
        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_coursesUrl}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                // Handle or log the error
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                // Handle or log the error
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }
    }
}
