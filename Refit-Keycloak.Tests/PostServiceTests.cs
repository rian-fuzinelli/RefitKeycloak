using Moq;
using RefitKeycloak.Application.Services;
using RefitKeycloak.Infrastructure.External;

namespace RefitKeycloak.Tests;

public class PostServiceTests
{
    [Fact
    public async Task Should_Return_Posts()
    {
        var mockApi = new Mock<IExternalApi>();

        mockApi
            .Setup(x => x.GetPosts())
            .ReturnsAsync(
                new List<PostDto>
                {
                    new PostDto { Id = 1, Title = "Teste" }
                });

        var service = new PostService(mockApi.Object);

        var result = await service.GetPosts();

        Assert.Single(result);
    }
}
