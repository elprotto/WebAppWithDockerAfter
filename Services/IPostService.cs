using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithDocker.Domain;

namespace WebApplicationWithDocker.Services
{
    public interface IPostService
    {
        List<Post> GetPosts();
        Post GetPostById(Guid postId);
        bool updatePost(Post postToUpdate);
    }
}
