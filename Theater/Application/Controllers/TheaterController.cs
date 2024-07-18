using Application.Contracts;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route( "api/theater" )]
public class TheaterController : ControllerBase
{
    private readonly ITheaterRepository _theaterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TheaterController( ITheaterRepository theaterRepository, IUnitOfWork unitOfWork )
    {
        _theaterRepository = theaterRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAllTheaters()
    {
        List<Theater> theaters = _theaterRepository.GetAll();
        return Ok( theaters );
    }

    [HttpGet, Route( "{id:int}" )]
    public IActionResult GetTheater( [FromRoute] int id )
    {
        Theater theater = _theaterRepository.GetById( id );
        if ( theater is null )
        {
            return NotFound( $"No such theater with Id = {id} exists" );
        }

        return Ok( theater );
    }

    [HttpPost]
    public IActionResult CreateTheater( [FromBody] CreateTheater theater )
    {
        Theater newTheater = new( theater.Name, theater.OpeningDate, theater.Address, theater.Description, theater.PhoneNumber );
        _theaterRepository.Add( newTheater );
        _unitOfWork.SaveChanges();

        return Ok( newTheater );
    }

    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeleteTheater( [FromRoute] int id )
    {
        Theater theater = _theaterRepository.GetById( id );
        if ( theater is null )
        {
            return NotFound( $"No such theater with Id = {id} exists" );
        }

        _theaterRepository.Remove( theater );
        _unitOfWork.SaveChanges();
        return Ok();
    }

    [HttpPut, Route( "{id:int}" )]
    public IActionResult UpdateTheater( [FromRoute] int id, [FromBody] UpdateTheater theater )
    {
        Theater existingTheater = _theaterRepository.GetById( id );
        if ( existingTheater is null )
        {
            return NotFound( $"No such theater with Id = {id} exists" );
        }

        Theater updateTheater = new( theater.Name, existingTheater.OpeningDate, existingTheater.Address, theater.Description, theater.PhoneNumber );

        existingTheater = _theaterRepository.Update( id, updateTheater );
        _unitOfWork.SaveChanges();
        return Ok( existingTheater );
    }
}