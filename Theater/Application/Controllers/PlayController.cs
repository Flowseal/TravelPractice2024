using Application.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route( "api/play" )]
public class PlayController : ControllerBase
{
    private readonly IPlayRepository _playRepository;

    public PlayController( IPlayRepository playRepository )
    {
        _playRepository = playRepository;
    }

    [HttpGet]
    public IActionResult GetAllPlays()
    {
        List<Play> plays = _playRepository.GetAll();
        return Ok( plays );
    }

    [HttpGet, Route( "{id:int}" )]
    public IActionResult GetPlay( [FromRoute] int id )
    {
        Play play = _playRepository.GetById( id );
        if ( play is null )
        {
            return NotFound( $"No such play with Id = {id} exists" );
        }

        return Ok( play );
    }

    [HttpPost]
    public IActionResult CreatePlay( [FromBody] CreatePlay play )
    {
        Play newPlay = new( play.Name, play.Description, play.StartDate, play.EndDate, play.Price, play.TheaterId, play.CompositionId );
        _playRepository.Add( newPlay );
        _playRepository.SaveChanges();

        return Ok( newPlay );
    }

    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeletePlay( [FromRoute] int id )
    {
        Play play = _playRepository.GetById( id );
        if ( play is null )
        {
            return NotFound( $"No such play with Id = {id} exists" );
        }

        _playRepository.Remove( play );
        _playRepository.SaveChanges();
        return Ok();
    }

    [HttpPost, Route( "range" )]
    public IActionResult GetPlaysInDatesRange( [FromBody] DateRange dateRange )
    {
        List<Play> plays = _playRepository.GetPlaysInDatesRange( dateRange.Start, dateRange.End );
        List<PlayBriefInfo> playBriefs = plays.Select( p => new PlayBriefInfo
        {
            Name = p.Name,
            Description = p.Description,
            CompositionDescription = p.Composition.Description,
            CharactersInfo = p.Composition.CharactersInfo,
            Price = p.Price
        } ).ToList();

        GetPlaysInRange playsInRange = new GetPlaysInRange
        {
            StartDate = dateRange.Start,
            EndDate = dateRange.End,
            PlayBriefInfos = playBriefs
        };

        return Ok( playsInRange );
    }
}