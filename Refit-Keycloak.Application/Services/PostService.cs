using RefitKeycloak.Application.Interfaces;
using RefitKeycloak.Domain.Entities;
using RefitKeycloak.Infrastructure.External;

namespace RefitKeycloak.Application.Services;

public class PostService : IPostService
{
    private readonly IExternalApi _api;

    public PostService(IExternalApi api)
    {
        _api = api;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        var response = await _api.GetPosts();

        return response.Select(x => new Post
        {
            Id = x.Id,
            Title = x.Title
        });
    }
}
