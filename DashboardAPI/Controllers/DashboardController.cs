﻿using DashboardAPI.Models;
using DashboardAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(DashboardController));
        private readonly IDashRepos _context;
        public DashboardController(IDashRepos context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<DashBoardPosts> GetAllPosts()
        {
            _log4net.Info("Get All Posts is Called !!");
            return _context.GetAllPosts();
        }
        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            _log4net.Info("Get Post By ID is Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Post = _context.GetPostById(id);
            _log4net.Info("Post of Id " + id + " is called");
            if (Post == null)
            {
                return NotFound();
            }
            return Ok(Post);
        }

        [HttpPost]
        public async Task<IActionResult> PostDashboardPost(DashBoardPosts item)
        {
            _log4net.Info("PostDashboardPosts is called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addPost = await _context.PostDashBoardPosts(item);
            return Ok(addPost);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateDashboardPost(DashBoardPosts item, int id)
        {
            _log4net.Info("UpdateDashboardPost Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newpost = await _context.UpdatePost(item, id);
                _log4net.Info("UpdateDashboardPost with Id " + id + " Was Called !!");
                return Ok(newpost);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            _log4net.Info("DeletePost Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deletedpost = await _context.RemovePost(id);
                _log4net.Info("DeletePost with Id " + id + " Was Called !!");
                return Ok(deletedpost);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
