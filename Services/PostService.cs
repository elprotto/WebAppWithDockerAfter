using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithDocker.Domain;

namespace WebApplicationWithDocker.Services
{
    public class PostService : IPostService
    {
        private readonly List<Post> posts;
        public PostService()
        {
            this.posts = new List<Post>();
            for (int i = 0; i < 100; i++)
            {
                this.posts.Add(new Post
                {
                    Id = Guid.NewGuid(),
                    Name = $"Post Name { i }"
                });
            }
        }
        public Post GetPostById(Guid postId)
        {
            return this.posts.SingleOrDefault(x => x.Id == postId);
        }

        public List<Post> GetPosts()
        {
            return this.posts;
        }
    }
}
