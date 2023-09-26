using CTBS.API.Movies;
using Ogooreck.API;
using static Ogooreck.API.ApiSpecification;

namespace CTBS.Tests;

public class AddMovieTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly ApiSpecification<Program> API;

    public AddMovieTests(TestWebApplicationFactory webApplicationFactory) =>
        API = ApiSpecification<Program>.Setup(webApplicationFactory);

    [Theory]
    [MemberData(nameof(ValidRequests))]
    public Task ValidRequest_ShouldReturn_201(AddMovieRequest validRequest) =>
        API.Given()
            .When(
                POST,
                URI("/api/movies/"),
                BODY(validRequest)
            )
            .Then(CREATED);

    [Theory]
    [MemberData(nameof(InvalidRequests))]
    public Task InvalidRequest_ShouldReturn_400(AddMovieRequest invalidRequest) =>
        API.Given()
            .When(
                POST,
                URI("/api/movies"),
                BODY(invalidRequest)
            )
            .Then(BAD_REQUEST);
    
    private const string ValidTitle = "VALID_TITLE";
    private static int ValidDuration => 90;
    private const string ValidDescription = "VALID_DESCRIPTION";

    public static TheoryData<AddMovieRequest> ValidRequests = new()
    {
        new AddMovieRequest(ValidDuration, ValidDescription, ValidTitle, Genre.Action),
        new AddMovieRequest(ValidDuration, ValidDescription, ValidTitle, Genre.Comedy)
    };

    public static TheoryData<AddMovieRequest> InvalidRequests = new()
    {
        new AddMovieRequest(0,ValidDescription, null, Genre.Drama),
        new AddMovieRequest(60, "", ValidTitle, Genre.Crime),
        new AddMovieRequest(ValidDuration, null, ValidTitle, Genre.Horror ),
    };
}