using RefitKeycloak.Domain.Entities;

namespace RefitKeycloak.Application.Interfaces;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
}
