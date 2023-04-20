using Google.Protobuf;
using Grpc.Core;

namespace Nerosoft.Insights.Storage;

public class SessionGrpcService: Google.Protobuf.SessionGrpcService.SessionGrpcServiceBase
{
    public override Task<GrpcResponse> Call(GrpcRequest request, ServerCallContext context)
    {
        return base.Call(request, context);
    }
}
