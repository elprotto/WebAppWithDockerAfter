using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplicationWithDocker.Contracts.V1;
using WebApplicationWithDocker.Domain;

namespace WebApplicationWithDocker.Controllers.V1
{
    public class Posts : Controller
    {
        private List<Post> posts;
        public Posts()
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
    }
}
