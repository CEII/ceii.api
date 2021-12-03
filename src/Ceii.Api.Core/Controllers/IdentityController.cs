using System;
using System.Threading.Tasks;
using Ceii.Api.Data.Dtos;
using Ceii.Api.Data.Entities;
using Ceii.Api.Data.Enums;
using Ceii.Api.Data.Utils;
using Ceii.Api.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Ceii.Api.Core.Controllers;

[ApiController]
public class IndentityController : ControllerBase
{
    private readonly IIdentityRepository _repo;

    public IndentityController(IIdentityRepository repo) => _repo = repo;

    [HttpGet("{role}")]
    public async Task<ActionResult<PaginatedList<User>>> GetByRole(IdentityRole role, [FromQuery] PagingInfo info)
    {
        try
        {
            return await _repo.GetByRole(role, info);
        } 
        catch(Exception ex)
        {
            var mssg = $"Error while fetching users: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}";

            Log.Error(ex, mssg);
            return BadRequest(mssg);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(string id)
    {
        try
        {
            return await _repo.GetById(id);
        }
        catch(Exception ex)
        {
            var mssg = $"Error while fetching user by id: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}";

            Log.Error(ex, mssg);
            return BadRequest(mssg);
        }
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> Create(User u)
    {
        try
        {
            return await _repo.Create(u);
        } 
        catch(Exception ex)
        {
            var mssg = $"Error while creating users: {(ex.InnerException is null ? ex.Message : ex.InnerException.Message)}";

            Log.Error(ex, mssg);
            return BadRequest(mssg);
        }
    }
}