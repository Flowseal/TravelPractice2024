using WebAPI.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/author" )]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorController( IAuthorRepository authorRepository, IUnitOfWork unitOfWork )
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
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
        _unitOfWork.SaveChanges();

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
        _unitOfWork.SaveChanges();
        return Ok();
    }
}