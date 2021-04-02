using DashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Repositories
{
    public class DashRepos:IDashRepos
    {
        private readonly CommunityGateDatabaseContext _context;

        public DashRepos()
        {

        }
        public DashRepos(CommunityGateDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<DashBoardPosts> GetAllPosts()
        {
            return _context.DashBoardPosts.ToList();
        }
        public DashBoardPosts GetPostById(int id)
        {
            DashBoardPosts item = _context.DashBoardPosts.Find(id);
            return item;
        }

        public async Task<DashBoardPosts> PostDashBoardPosts(DashBoardPosts item)
        {
            DashBoardPosts post = null;
            if (item == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                post = new DashBoardPosts() {
                    DashBody = item.DashBody,
                    DashIntendedFor = item.DashIntendedFor,
                    DashTime = DateTime.Now,
                    DashTitle = item.DashTitle,
                    DashType = item.DashType,
                    ResidentId = item.ResidentId,
                    ResidentName=item.ResidentName
                    
                };
                await _context.DashBoardPosts.AddAsync(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<DashBoardPosts> RemovePost(int id)
        {
            DashBoardPosts post = await _context.DashBoardPosts.FindAsync(id);
            if (post == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                _context.DashBoardPosts.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<DashBoardPosts> UpdatePost(DashBoardPosts item, int id)
        {
            DashBoardPosts tempPost = await _context.DashBoardPosts.FindAsync(id);
            tempPost.DashBody = item.DashBody;
            tempPost.DashIntendedFor = item.DashIntendedFor;
            tempPost.DashTitle = item.DashTitle; 
            tempPost.DashType = item.DashType;
            await _context.SaveChangesAsync();

            return tempPost;
        }
    }
}
