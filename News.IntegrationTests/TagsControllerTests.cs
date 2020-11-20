using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using News.Contracts.V1;
using News.Domain;
using Xunit;

namespace News.IntegrationTests
{
    public class TagsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllTags_FromEmptyDB_ReturnsEmpty() 
        {
            var response = await TestClient.GetAsync(ApiRoutes.Tags.GetAll);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Branch>>()).Should().BeEmpty();
        }
        
        [Fact]
        public async Task CreateTag_ReturnsCreated() 
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Tags.Create, new {tagName = "string"});

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
 
        [Fact]
        public async Task GetAllTags_Returns2() 
        {
            await TestClient.PostAsJsonAsync(ApiRoutes.Tags.Create, new {tagName = "string"});
            await TestClient.PostAsJsonAsync(ApiRoutes.Tags.Create, new {tagName = "string1"});
            
            var response = await TestClient.GetAsync(ApiRoutes.Tags.GetAll);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Branch>>()).Count.Should().Be(2);
        }
        
        [Fact]
        public async Task Delete_Tag() 
        {
            await TestClient.PostAsJsonAsync(ApiRoutes.Tags.Create, new {tagName = "string1"});
            var response = await TestClient.DeleteAsync(ApiRoutes.Tags.Delete.Replace("{tagName}", "string1"));
            
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task DeleteNull_Tag() 
        {
            var response = await TestClient.DeleteAsync(ApiRoutes.Tags.Delete.Replace("{tagName}", "string1"));
            
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task GetTagByName() 
        {
            await TestClient.PostAsJsonAsync(ApiRoutes.Tags.Create, new {tagName = "string1"});
            var response = await TestClient.GetAsync(ApiRoutes.Tags.Get.Replace("{tagName}", "string1"));
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}