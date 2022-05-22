using DaprDemoApp.Common;
using DaprDemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DaprDemoApp.Web.Controllers;

public class ServiceInvocationController : Controller
{
    private readonly DaprHttpClient daprHttpClient;
    private readonly ILogger<ServiceInvocationController> _logger;

    public ServiceInvocationController(DaprHttpClient daprHttpClient, ILogger<ServiceInvocationController> logger)
    {
        this.daprHttpClient = daprHttpClient;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var weatherForecasts = await daprHttpClient.InvokeServiceAsync<IEnumerable<WeatherForecast>>("daprdemoapi", "weatherforecast");

            return View(new WithServiceHealthViewModel<IEnumerable<WeatherForecast>>(value: weatherForecasts));
        }
        catch (HttpRequestException)
        {
            return View(new WithServiceHealthViewModel<IEnumerable<WeatherForecast>>(isDependentServiceHealthy: false));
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
