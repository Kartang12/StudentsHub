using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Contracts.V1.Responses;
using News.Domain;
using Xunit;

namespace News.IntegrationTests
{
    public class UsersControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_FromEmptyDB()
        {
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetAll);
            
            (await response.Content.ReadAsAsync<List<Branch>>()).Count.Should().Be(0);
        }
        
        [Fact]
        public async Task CreateUser()
        {
            await CreateRole("User");
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task CreateSameUser()
        {
            await CreateRole("User");
            await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task CreateMultipleUsers()
        {
            await CreateRole("User");
            await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string1@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetAll);
            (await response.Content.ReadAsAsync<List<UserDataResponse>>()).Count.Should().Be(2);
        }
        
        [Fact]
        public async Task CreateUserWithWrongModel()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "string@example.com",
                Password = "String1234.",
                BusinessType = "User"
            });
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task CreateUserWithWrongData()
        {
            await CreateRole("User");
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.Add, new UserRegistrationRequest()
            {
                Email = "stringamplom",
                Password = "Stri",
                BusinessType = "User"
            });
            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}