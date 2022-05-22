using DaprDemoApp.Common;
using DaprDemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaprDemoApp.Web.Controllers
{
    public class StateManagementController : Controller
    {
        private readonly DaprHttpClient daprHttpClient;

        public StateManagementController(DaprHttpClient daprHttpClient)
        {
            this.daprHttpClient = daprHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var visitedCount = await daprHttpClient.GetStateAsync<int>("statestore", "visitedcount");

                visitedCount++;

                await daprHttpClient.PostStateAsync("statestore", "visitedcount", visitedCount);

                return View(new WithServiceHealthViewModel<int>(value: visitedCount));
            }
            catch (HttpRequestException)
            {
                return View(new WithServiceHealthViewModel<int>(false));
            }
        }
    }
}
