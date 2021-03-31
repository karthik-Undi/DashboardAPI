using DashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Repositories
{
    public interface IDashRepos
    {
        public IEnumerable<DashBoardPosts> GetAllPosts();
        public DashBoardPosts GetPostById(int id);
        public Task<DashBoardPosts> PostDashBoardPosts(DashBoardPosts item);
        public Task<DashBoardPosts> RemovePost(int id);
        public Task<DashBoardPosts> UpdatePost(DashBoardPosts item, int id);





    }
}
