using System;
using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nerosoft.Insights.Collector.Controllers;

[Route("collector/api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly MessageBus _bus;

    public LogsController(MessageBus bus)
    {
        _bus = bus;
    }

    [Consumes("application/json", "application/problem+json")]
    [HttpPost]
    public async Task<IActionResult> CollectAsync()
    {

        //var buffer = await DecompressRequest();
        //var json = Encoding.UTF8.GetString(buffer);
        //Debug.WriteLine(json);

        //var request = await ReadAsync();

        //EndpointConvention.Map<CreateOrderRequest>(new Uri("queue:create-order"));
        //await Task.Run(() => _bus.Send(json.ToString(), installId, appSecret));
        //var endpoint = await _bus.GetSendEndpoint(new Uri("queue:insights.collect"));
        //await endpoint.Send(request);

        var appSecret = Request.Headers["App-Secret"];
        var installId = Request.Headers["Install-ID"];

        var content = await Request.BodyReader.ReadAsync();
        var buffer = content.Buffer.ToArray();

        await Task.Run(() => _bus.Send(buffer, installId, appSecret));

        return Ok();
    }
}