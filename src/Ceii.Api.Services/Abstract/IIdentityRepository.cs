using System.Threading.Tasks;
using Ceii.Api.Data.Dtos;
using Ceii.Api.Data.Entities;
using Ceii.Api.Data.Enums;
using Ceii.Api.Data.Utils;

namespace Ceii.Api.Services.Abstract;

/// <summary>
/// Abstract implementation for definition of services
/// </summary>
public interface IIdentityRepository
{
    Task<PaginatedList<User>> GetByRole(IdentityRole role, PagingInfo info);

    Task<User> Create(User u);
}