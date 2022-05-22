using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;

namespace DaprDemoApp.Common;

public class DaprHttpClient
{
    private readonly HttpClient httpClient;

    public DaprHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient;

        var daprHttpPort = configuration.GetValue<string>("DAPR_HTTP_PORT") ?? "3500";

        httpClient.BaseAddress = new Uri($"http://localhost:{daprHttpPort}/");
    }

    public async Task<T?> InvokeServiceAsync<T>(string serviceName, string methodName)
    {
        var serviceRequestUri = $"v1.0/invoke/{serviceName}/method/{methodName}";

        var httpResponseMessage = await httpClient.GetAsync(serviceRequestUri);

        httpResponseMessage.EnsureSuccessStatusCode();

        return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> GetStateAsync<T>(string storeName, string stateName)
    {
        string stateRequestUri = $"v1.0/state/{storeName}/{stateName}";

        var httpResponseMessage = await httpClient.GetAsync(stateRequestUri);

        httpResponseMessage.EnsureSuccessStatusCode();

        if (httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            return default;

        return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
    }

    public async Task PostStateAsync<T>(string storeName, string stateName, T value)
    {
        var stateRequestUri = $"v1.0/state/{storeName}";

        var httpResponseMessage = await httpClient.PostAsJsonAsync(stateRequestUri, new[] { new { key = stateName, value } });

        httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task PublishAsync<T>(string brokerName, string topic, T message)
    {
        var publishRequestUri = $"v1.0/publish/{brokerName}/{topic}";

        var httpResponseMessage = await httpClient.PostAsJsonAsync(publishRequestUri, message);

        httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task InvokeBindingAsync<T>(string bindingName, T smtpPayload)
    {
        var bindingRequestUri = $"v1.0/bindings/{bindingName}";

        var httpResponseMessage = await httpClient.PostAsJsonAsync(bindingRequestUri, smtpPayload);

        httpResponseMessage.EnsureSuccessStatusCode();
    }

}
