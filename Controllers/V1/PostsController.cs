using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult GetAll()
        {
            return Ok(this.postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]Guid postId)
        {
            var post = this.postService.GetPostById(postId);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };
            if(this.postService.UpdatePost(post))
                return Ok(post);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var deletedPost = this.postService.DeletePost(postId);

            if (deletedPost)
                return NoContent();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post();
            if (postRequest != null)
                post.Id = postRequest.Id;

            if (post.Id == Guid.Empty)
                post.Id = Guid.NewGuid();

            this.postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{post.Id}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };
            return Created(locationUri, response);
        }
    }
}
