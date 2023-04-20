using System.Text.Json;

namespace Google.Protobuf;

public partial class GrpcResponse
{
    public GrpcResponse(object value)
    {
        if (value.GetType().IsClass)
        {
            Data = JsonSerializer.Serialize(value);
        }
        else
        {
            Data = value.ToString();
        }
    }
}