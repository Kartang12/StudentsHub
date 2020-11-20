using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Data;
using News.Domain;
using Newtonsoft.Json;

namespace News.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;
        
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            services.RemoveAll(typeof(DataContext));
                            services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                        });
                    });
            
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<HttpResponseMessage> RegisterUser(string name, string password)
        {
            await CreateRole("User");
            var request = new
            {
                Email = name,
                Password = password,
                Role = "User"
            };
            return await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, request);
        }
        
        protected async Task<HttpResponseMessage> CreateRole(string roleName)
        {
            return await TestClient.PostAsJsonAsync(ApiRoutes.Businesses.Add, roleName);
        }
        
        protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Posts.Create, request);
            return await response.Content.ReadAsAsync<PostResponse>();
        }
        
        protected async Task<Post> UpdatePostAsync(string id, UpdatePostRequest request)
        {
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Posts.Update.Replace("{postId}", id), request);
            return await response.Content.ReadAsAsync<Post>();
        }
        
        protected async Task<HttpResponseMessage> DeletePostAsync(string id)
        {
            return await TestClient.DeleteAsync(ApiRoutes.Posts.Delete.Replace("{postId}", id));
        }
        
        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "SomePass1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.EnsureDeleted();
        }
    }
}