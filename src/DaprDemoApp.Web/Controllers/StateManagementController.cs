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
                var refreshedCount = await daprHttpClient.GetStateAsync<int>("statestore", "refreshedcount");

                refreshedCount++;

                await daprHttpClient.PostStateAsync("statestore", "refreshedcount", refreshedCount);

                return View(new WithServiceHealthViewModel<int>(value: refreshedCount));
            }
            catch (HttpRequestException)
            {
                return View(new WithServiceHealthViewModel<int>(false));
            }
        }
    }
}
