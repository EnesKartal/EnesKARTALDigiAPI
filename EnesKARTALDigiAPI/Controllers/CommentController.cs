﻿using EnesKARTALDigiAPI.Data.Models;
using EnesKARTALDigiAPI.Data.Repositories.Infra;
using EnesKARTALDigiAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EnesKARTALDigiAPI.Controllers
{
    [Route("api/Comment")]
    public class CommentController : BaseController
    {
        readonly ICommentRepository commentRepository;
        ConfigHelper Config;
        public CommentController(ICacheManager cacheManager,
           ICommentRepository commentRepository,
           ConfigHelper config) : base(cacheManager)
        {
            this.commentRepository = commentRepository;
            this.Config = config;
        }

        // GET api/comment
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(commentRepository.GetAll());
        }

        // GET api/comment/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var comment = commentRepository.GetCommentById(id.Value);

            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // POST api/comment
        [HttpPost]
        public IActionResult Post(Comment comment)
        {
            if (!ModelState.IsValid || comment == null)
                return BadRequest();

            if (commentRepository.Add(comment))
                return Ok(comment);

            return BadRequest();
        }

        // PUT api/comment/5
        [HttpPut("{id}")]
        public IActionResult Put(int? id, Comment comment)
        {
            if (!id.HasValue)
                return BadRequest();

            if (!ModelState.IsValid || comment == null)
                return BadRequest();

            var oldComment = commentRepository.GetCommentById(id.Value);

            if (oldComment == null)
                return NotFound();

            comment.Id = oldComment.Id;
            if (commentRepository.Update(comment))
                return Ok(comment);

            return BadRequest();
        }

        // DELETE api/comment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var comment = commentRepository.GetCommentById(id.Value);

            if (comment == null)
                return NotFound();

            if (commentRepository.Delete(comment))
                return Ok(comment);

            return BadRequest();
        }
    }
}
