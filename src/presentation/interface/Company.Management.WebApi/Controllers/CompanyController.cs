using Microsoft.AspNetCore.Mvc;
using MediatR;
using Company.Management.Application.Companies.Commands;
using Company.Management.Application.Companies.Dtos;
using Company.Management.Application.Companies.Extensions;
using Company.Management.Application.Companies.Queries;
using Company.Management.Application.Companies.Requests;
using Company.Management.Application.Companies.Response;
using System.Net;
using Company.Management.Application.Companies.Events;
using Company.Management.Application.Companies.Interfaces.Projections;

namespace Company.Management.WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class CompanyController(
    IMediator mediator,
    ICompanyCreatedProjectionHandler createdProjectionHandler,
    ICompanyUpdatedProjectionHandler updatedProjectionHandler,
    ICompanyDeletedProjectionHandler deletedProjectionHandler
) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly ICompanyCreatedProjectionHandler _createdProjectionHandler = createdProjectionHandler;
    private readonly ICompanyUpdatedProjectionHandler _updatedProjectionHandler = updatedProjectionHandler;
    private readonly ICompanyDeletedProjectionHandler _deletedProjectionHandler = deletedProjectionHandler;

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(CompanyDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCompany(Guid id)
        => Ok(await _mediator.Send(new GetCompanyByIdQuery(id)));

    [HttpGet]
    [Route("companies:paginated")]
    [ProducesResponseType(typeof(CompanyResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCompanies([FromQuery] FetchCompaniesRequest request)
        => Ok(await _mediator.Send(request.ToFetchCompaniesQuery()));

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand request)
    {
        var result = await _mediator.Send(request);

        var @event = new CompanyCreatedEvent(result.Id, result.Name, result.SizeType);
        await _createdProjectionHandler.Handle(@event);

        return Created($"{Url.Action()}/{result.Id}", default);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateCompany([FromRoute] Guid id, [FromBody] UpdateCompanyRequest request)
    {
        var command = new UpdateCompanyCommand(id, request.Name, request.SizeType);

        var result = await _mediator.Send(command);

        var @event = new CompanyUpdatedEvent(result.Id, result.Name, result.SizeType);
        await _updatedProjectionHandler.Handle(@event);

        return Ok(result);
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _mediator.Send(new DeleteCompanyCommand(id));

        var @event = new CompanyDeletedEvent(id);
        await _deletedProjectionHandler.Handle(@event);

        return NoContent();
    }
}
