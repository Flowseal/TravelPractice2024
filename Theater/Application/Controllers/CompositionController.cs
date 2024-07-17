using Application.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route( "api/composition" )]
public class CompositionController : ControllerBase
{
    private readonly ICompositionRepository _compositionRepository;

    public CompositionController( ICompositionRepository compositionRepository )
    {
        _compositionRepository = compositionRepository;
    }

    [HttpGet]
    public IActionResult GetAllCompositions()
    {
        List<Composition> compositions = _compositionRepository.GetAll();
        return Ok( compositions );
    }

    [HttpGet, Route( "{id:int}" )]
    public IActionResult GetComposition( [FromRoute] int id )
    {
        Composition composition = _compositionRepository.GetById( id );
        if ( composition is null )
        {
            return NotFound( $"No such composition with Id = {id} exists" );
        }

        return Ok( composition );
    }

    [HttpPost]
    public IActionResult CreateComposition( [FromBody] CreateComposition composition )
    {
        Composition newComposition = new( composition.Name, composition.Description, composition.CharactersInfo, composition.AuthorId );
        _compositionRepository.Add( newComposition );
        _compositionRepository.SaveChanges();

        return Ok( newComposition );
    }

    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeleteComposition( [FromRoute] int id )
    {
        Composition composition = _compositionRepository.GetById( id );
        if ( composition is null )
        {
            return NotFound( $"No such composition with Id = {id} exists" );
        }

        _compositionRepository.Remove( composition );
        _compositionRepository.SaveChanges();
        return Ok();
    }
}