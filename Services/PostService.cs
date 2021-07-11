using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationWithDocker.Data.Migrations;
using WebApplicationWithDocker.Domain;

namespace WebApplicationWithDocker.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext dataContext;

        public PostService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await this.GetPostByIdAsync(postId);
            this.dataContext.Posts.Remove(post);
            var deleted = await this.dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await this.dataContext.Posts.AddAsync(post);
            var created = await this.dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            var postList = await this.GetPostsAsync();
            return postList.SingleOrDefault(x => x.Id == postId);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await this.dataContext.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            this.dataContext.Posts.Update(postToUpdate);
            var updated = await this.dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}
