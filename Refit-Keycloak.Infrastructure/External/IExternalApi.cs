using Refit;

namespace RefitKeycloak.Infrastructure.External;

public interface IExternalApi
{
    [Get("/posts")]
    Task<List<PostDto>> GetPosts();
}
