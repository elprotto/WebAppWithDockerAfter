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

        public bool DeletePost(Guid postId)
        {
            var post = this.GetPostById(postId);

            if (post == null)
                return false;

            this.posts.Remove(post);
            return true;
        }

        public Post GetPostById(Guid postId)
        {
            return this.posts.SingleOrDefault(x => x.Id == postId);
        }

        public List<Post> GetPosts()
        {
            return this.posts;
        }

        public bool UpdatePost(Post postToUpdate)
        {
            var postExist = GetPostById(postToUpdate.Id) != null;

            if (!postExist)
                return false;

            var index = this.posts.FindIndex(x => x.Id == postToUpdate.Id);
            this.posts[index] = postToUpdate;

            return true;
        }
    }
}
