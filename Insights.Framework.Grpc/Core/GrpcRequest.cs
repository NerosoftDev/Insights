using System.Text.Json;

namespace Google.Protobuf;

public partial class GrpcRequest
{
    public GrpcRequest(object data)
    {
        if (data.GetType().IsClass)
        {
            Data = JsonSerializer.Serialize(data);
        }
        else
        {
            Data = data.ToString();
        }
    }
}