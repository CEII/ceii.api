using System.Linq;
using System.Threading.Tasks;
using Ceii.Api.Data.Context;
using Ceii.Api.Data.Dtos;
using Ceii.Api.Data.Entities;
using Ceii.Api.Data.Enums;
using Ceii.Api.Data.Utils;
using Ceii.Api.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Ceii.Api.Services.Services;

public class IdentityService : IIdentityRepository
{
    private readonly CeiiDbContext _ctx;

    public IdentityService(CeiiDbContext context) => _ctx = context;

    public async Task<PaginatedList<User>> GetByRole(IdentityRole role, PagingInfo info)
    {
        var users = _ctx.Users!.Where(u => u.Role == role);

        return await PaginatedList<User>.CreateAsync(users, info);
    }

    public async Task<User> Create(User u)
    {
        var result = _ctx.Add(u);
        await _ctx.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User> GetById(string id)
    {
        var user = await _ctx!.Users!.Where(u => u.Email == id || u.OAuthId == id).FirstOrDefaultAsync();

        return user;
    }
}
