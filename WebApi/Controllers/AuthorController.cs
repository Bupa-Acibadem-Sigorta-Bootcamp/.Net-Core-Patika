﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using WebApi.Applications.AuthorOperations.Commands.CreateAuthor;
using WebApi.Applications.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Applications.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.DataBaseOperations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {

            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQueryViewModel result;
            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(_context, _mapper);
            getAuthorDetailQuery.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(getAuthorDetailQuery);
            result = getAuthorDetailQuery.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateAuthorCommand([FromBody] CreateAuthorCommand.CreateAuthorCommandViewModel author)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = author;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Yazar Eklendi.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorViewModel author)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.AuthorId = id;
            command.Model = author;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Yazar Güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok("Yazar Silindi.");
        }
    }
}
