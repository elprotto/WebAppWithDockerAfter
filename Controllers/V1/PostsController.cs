using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplicationWithDocker.Contracts.V1;
using WebApplicationWithDocker.Contracts.V1.Requests;
using WebApplicationWithDocker.Contracts.V1.Responses;
using WebApplicationWithDocker.Domain;

namespace WebApplicationWithDocker.Controllers.V1
{
    public class PostsController : Controller
    {
        private List<Post> posts;
        public PostsController()
        {
            this.posts = new List<Post>();
            for (int i = 0; i < 100; i++)
            {
                this.posts.Add(new Post { Id = Guid.NewGuid().ToString()});
            }
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(this.posts);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };
            if (string.IsNullOrEmpty(post.Id))
                post.Id = Guid.NewGuid().ToString();

            this.posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{post.Id}", post.Id);

            var response = new PostResponse { Id = post.Id };
            return Created(locationUri, response);
        }
    }
}
