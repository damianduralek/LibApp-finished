using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;
        }

        // GET api/books/
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = repository.GetBooks()
               .ToList();

            return Ok(books);
        }

        [Authorize(Roles = "Owner,StoreManager")]
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookInDb = repository.GetBookById(id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            repository.DeleteBook(id);
            repository.Save();

            return NoContent();
        }

        // GET /api/books/{id}
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook(int id)
        {
            var book = repository.GetBookById(id);
            if (book == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return Ok(_mapper.Map<BookDto>(book));
        }

        // POST /api/books
        [Authorize(Roles = "Owner,StoreManager")]
        [HttpPost]
        public IActionResult CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDto);
            repository.AddBook(book);
            repository.Save();

            bookDto.Id = book.Id;

            return CreatedAtRoute(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        // PUT /api/books/{id}
        [Authorize(Roles = "Owner,StoreManager")]
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var bookInDb = repository.GetBookById(id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            _mapper.Map(bookDto, bookInDb);

            repository.UpdateBook(bookInDb);
            repository.Save();

            return Ok(bookInDb);
        }
    }
}
