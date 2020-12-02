using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using EnesKARTALDigiAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EnesKARTALDigiAPI.Controllers
{
    [Route("api/Post")]
    public class PostController : BaseController
    {
        readonly IPostRepository postRepository;

        public PostController(ICacheManager cacheManager,
            IPostRepository postRepository) : base(cacheManager)
        {
            this.postRepository = postRepository;
        }

        // GET api/post
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(postRepository.GetAllPosts());
        }

        // GET api/post/filter?query=mytitle
        [HttpGet("Filter")]
        public IActionResult Filter(string query)
        {
            var data = postRepository.GetAll();

            if (!string.IsNullOrEmpty(query))
                data = data.Where(x => x.Title.ToLower().Contains(query) || x.SubTitle.ToLower().Contains(query) || x.Description.ToLower().Contains(query));

            return Ok(data.Include(x => x.Comments));
        }

        // GET api/post/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var post = postRepository.GetPostById(id.Value);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST api/post
        [HttpPost]
        public IActionResult Post(Post post)
        {
            if (!ModelState.IsValid || post == null)
                return BadRequest();

            if (postRepository.Add(post))
                return Ok(post);

            return BadRequest();
        }

        // PUT api/post/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, Post post)
        {
            if (!id.HasValue)
                return BadRequest();

            if (!ModelState.IsValid || post == null)
                return BadRequest();

            var oldPost = postRepository.GetPostById(id.Value);

            if (oldPost == null)
                return NotFound();

            post.Id = oldPost.Id;
            if (postRepository.Update(post))
                return Ok(post);

            return BadRequest();
        }

        // DELETE api/post/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var post = postRepository.GetPostById(id.Value);

            if (post == null)
                return NotFound();

            if (postRepository.Delete(post))
                return Ok(post);

            return BadRequest();
        }
    }
}
