using WebAPI.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/composition" )]
public class CompositionController : ControllerBase
{
    private readonly ICompositionRepository _compositionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompositionController( ICompositionRepository compositionRepository, IUnitOfWork unitOfWork )
    {
        _compositionRepository = compositionRepository;
        _unitOfWork = unitOfWork;
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
        _unitOfWork.SaveChanges();

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
        _unitOfWork.SaveChanges();
        return Ok();
    }
}