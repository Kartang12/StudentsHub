using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using News.Contracts.V1;
using News.Contracts.V1.Requests;
using News.Domain;
using Xunit;

namespace News.IntegrationTests
{
    public class PostsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse() 
        {
            // await AuthenticateAsync();

            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }
        
        [Fact]
        public async Task GetAllPosts_ReturnsNotFound_WhenPostsDontExistsInTheDatabase()
        {
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", "123"));
            
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // await AuthenticateAsync();
            CreatePostRequest request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            
            var createdPost = await CreatePostAsync(request);

            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString()));
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Name.Should().Be("Test post");
        }
        
        [Fact]
        public async Task Get_ReturnsAllPosts_WhenPostsExistsInTheDatabase()
        {
            CreatePostRequest request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            
            await CreatePostAsync(request);
            await CreatePostAsync(request);
            
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task UpdatePost_ReturnsNewPost()
        {
            CreatePostRequest request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            
            string id = (await CreatePostAsync(request)).Id.ToString();

            UpdatePostRequest upd = new UpdatePostRequest()
            {
                Name = "Test 1",
                Content = "test",
                Tag = "test"
            };
            
            var response = await UpdatePostAsync(id, upd);
            
            response.Name.Should().Be("Test 1");
        }
        
        [Fact]
        public async Task DeletePost_ReturnsNoContent()
        {
            var request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            string id = (await CreatePostAsync(request)).Id.ToString();

            var response = await DeletePostAsync(id);
            
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task GetPostByAuthor_ReturnsOk()
        {
            CreatePostRequest request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            await RegisterUser(request.UserName, "String1234.");
            await CreatePostAsync(request);
        
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetByAuthorName.Replace("{userName}", request.UserName));
        
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task GetPostByTag_ReturnsOk()
        {
            CreatePostRequest request = new CreatePostRequest()
            {
                Name = "Test post", 
                Content = "test", 
                UserName = "test@example.com",
                Tags = new[] {"tag_1"}
            };
            await CreatePostAsync(request);

            var response = await TestClient.GetAsync(ApiRoutes.Posts.ByTag.Replace("{tagName}", "tag_1"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}