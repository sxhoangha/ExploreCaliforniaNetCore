using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExploreCalifornia.api
{
    [Route("api/posts/{postKey}/comments")]
    public class CommentsController : Controller
    {
        private readonly BlogDataContext _db;
        public CommentsController(BlogDataContext db)
        {
            _db = db;
        }

        // GET: api/<controller>
        [HttpGet]
        public IQueryable<Comment> Get(string postKey)
        {
            return _db.Comments.Where(c => c.Post.Key == postKey);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Comment Get(long id)
        {
            return _db.Comments.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<controller>
        [HttpPost]
        public Comment Post(string postKey, [FromBody]Comment comment) //From body of the http request
        {
            var post = _db.Posts.FirstOrDefault(p => p.Key == postKey);

            if (post==null)
            {
                return null;
            }

            comment.Post = post;
            comment.Posted = DateTime.Now;
            comment.Author = User.Identity.Name;

            _db.Comments.Add(comment);
            _db.SaveChanges();

            return comment; //return the saved comment back to client

            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Comment updatedComment) //haven't implemented put and delete in html frontend
        {
            var comment = _db.Comments.FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Body = updatedComment.Body;
            _db.SaveChanges();
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.Id == id);
            if (comment != null)
            {
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }

        }
    }
}
