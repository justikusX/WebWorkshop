using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebWorkshop.Services;

public sealed class GraphQLClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
    };

    public GraphQLClient(HttpClient http) => _http = http;

    public async Task<T> QueryAsync<T>(
        string query,
        object? variables = null,
        CancellationToken ct = default)
    {
        var payload = new GraphQLRequest
        {
            query = query,
            variables = variables
        };

        using var response = await _http.PostAsJsonAsync("", payload, JsonOpts, ct);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(ct);
            throw new Exception($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}. Body: {body}");
        }

        var result = await response.Content.ReadFromJsonAsync<GraphQLResponse<T>>(JsonOpts, ct);

        if (result is null)
            throw new Exception("Empty GraphQL response");

        if (result.errors is { Length: > 0 })
            throw new Exception(string.Join("; ", result.errors.Select(e => e.message)));

        if (result.data is null)
            throw new Exception("GraphQL response contains no data");

        return result.data;
    }

    
    private sealed class GraphQLRequest
    {
        public string query { get; set; } = "";
        public object? variables { get; set; }
    }

    
    private sealed class GraphQLResponse<T>
    {
        public T? data { get; set; }
        public GraphQLError[]? errors { get; set; }
    }

    private sealed class GraphQLError
    {
        public string message { get; set; } = "";
    }
}
