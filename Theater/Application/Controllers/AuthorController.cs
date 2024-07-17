using Application.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route( "api/author" )]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController( IAuthorRepository authorRepository )
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public IActionResult GetAllAuthors()
    {
        IReadOnlyList<Author> authors = _authorRepository.GetAll();
        return Ok( authors );
    }

    [HttpGet, Route( "{id:int}" )]
    public IActionResult GetAuthor( [FromRoute] int id )
    {
        Author author = _authorRepository.GetById( id );
        if ( author is null )
        {
            return NotFound( $"No such author with Id = {id} exists" );
        }

        return Ok( author );
    }

    [HttpPost]
    public IActionResult CreateAuthor( [FromBody] CreateAuthor author )
    {
        Author newAuthor = new( author.Name, author.BirthDate );
        _authorRepository.Add( newAuthor );
        _authorRepository.SaveChanges();

        return Ok( newAuthor );
    }

    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeleteAuthor( [FromRoute] int id )
    {
        Author author = _authorRepository.GetById( id );
        if ( author is null )
        {
            return NotFound( $"No such author with Id = {id} exists" );
        }

        _authorRepository.Remove( author );
        _authorRepository.SaveChanges();
        return Ok();
    }
}