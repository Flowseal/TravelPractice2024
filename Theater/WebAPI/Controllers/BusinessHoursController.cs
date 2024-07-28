using WebAPI.Contracts;
using Domain.Models;
using Domain.Repositories;
using Infrastructure.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api/business_hours" )]
public class BusinessHoursController : ControllerBase
{
    private readonly IBusinessHoursRepository _businessHoursRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BusinessHoursController( IBusinessHoursRepository businessHoursRepository, IUnitOfWork unitOfWork )
    {
        _businessHoursRepository = businessHoursRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAllBusinessHours()
    {
        IReadOnlyList<BusinessHours> businessHours = _businessHoursRepository.GetAll();
        return Ok( businessHours );
    }

    [HttpGet, Route( "{id:int}" )]
    public IActionResult GetBusinessHours( [FromRoute] int id )
    {
        BusinessHours businessHours = _businessHoursRepository.GetById( id );
        if ( businessHours is null )
        {
            return NotFound( $"No such business hours with Id = {id} exists" );
        }

        return Ok( businessHours );
    }

    [HttpGet, Route( "theater/{id:int}" )]
    public IActionResult GetBusinessHoursForTheater( [FromRoute] int id )
    {
        List<BusinessHours> businessHours = _businessHoursRepository.GetByTheaterId( id );
        return Ok( businessHours );
    }

    [HttpPost]
    public IActionResult CreateBusinessHours( [FromBody] CreateBusinessHours businessHours )
    {
        BusinessHours newBusinessHours = new( businessHours.Day, businessHours.OpenTime, businessHours.CloseTime, businessHours.TheaterId, businessHours.ValidFrom, businessHours.ValidThrough );
        _businessHoursRepository.Add( newBusinessHours );
        _unitOfWork.SaveChanges();

        return Ok( newBusinessHours );
    }

    [HttpDelete, Route( "{id:int}" )]
    public IActionResult DeleteBusinessHours( [FromRoute] int id )
    {
        BusinessHours businessHours = _businessHoursRepository.GetById( id );
        if ( businessHours is null )
        {
            return NotFound( $"No such business hours with Id = {id} exists" );
        }

        _businessHoursRepository.Remove( businessHours );
        _unitOfWork.SaveChanges();
        return Ok();
    }
}