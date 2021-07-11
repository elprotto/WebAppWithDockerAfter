using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplicationWithDocker.Contracts.V1;
using WebApplicationWithDocker.Contracts.V1.Requests;
using WebApplicationWithDocker.Contracts.V1.Responses;
using WebApplicationWithDocker.Domain;
using WebApplicationWithDocker.Services;

namespace WebApplicationWithDocker.Controllers.V1
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = await this.postService.GetPostByIdAsync(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid postId, 
            [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };
            if(await this.postService.UpdatePostAsync(post))
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deletedPost = await this.postService.DeletePostAsync(postId);

            if (deletedPost)
                return NoContent();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post();
            if (postRequest != null)
                post.Name = postRequest.Name;

            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();

            await this.postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{post.Id}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUri, response);
        }
    }
}
