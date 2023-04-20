using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nerosoft.Insights.Collector.Controllers;

/// <inheritdoc />
[Route("collector/{controller=Home}/{action=Index}")]
[ApiExplorerSettings(IgnoreApi = true)]
[AllowAnonymous]
public class HomeController : Controller
{
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public IActionResult Index()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        return environment switch
        {
            "Development" => Redirect("/swagger"),
            _ => Content($"@ {DateTime.Today.Year} Nerosoft.")
        };
    }
}
