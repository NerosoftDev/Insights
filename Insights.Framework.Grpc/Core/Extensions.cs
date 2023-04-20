using System.ComponentModel;
using System.Text.Json;

namespace Google.Protobuf;

public static class Extensions
{
    public static TValue GetResult<TValue>(this GrpcResponse response)
    {
        return Convert<TValue>(response.Data);
    }

    public static GrpcResponse SetResult<TValue>(this GrpcResponse response, TValue value)
    {
        if (value == null)
        {
            return response;
        }

        if (typeof(TValue).IsClass)
        {
            response.Data = JsonSerializer.Serialize(value);
        }
        else
        {
            response.Data = value.ToString();
        }

        return response;
    }

    public static TValue GetProperty<TValue>(this GrpcRequest request, string key)
    {
        if (request == null)
        {
            throw new NullReferenceException();
        }

        if (request.Property == null || request.Property.Count == 0)
        {
            throw new NullReferenceException();
        }

        return request.Property.TryGetValue(key, out var value) ? Convert<TValue>(value) : default;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="request"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static GrpcRequest SetProperty<TValue>(this GrpcRequest request, string key, TValue value)
    {
        request.Property.TryAdd(key, Convert(value));
        return request;
    }

    public static TValue GetData<TValue>(this GrpcRequest request)
    {
        if (request?.Data == null)
        {
            throw new NullReferenceException();
        }

        return Convert<TValue>(request.Data);
    }

    public static GrpcRequest SetData<TValue>(this GrpcRequest request, TValue value)
    {
        request.Data = Convert(value);
        return request;
    }

    private static string Convert<TValue>(TValue content)
    {
        if (content == null)
        {
            return string.Empty;
        }

        return typeof(TValue).IsClass ? JsonSerializer.Serialize(content) : content.ToString();
    }

    private static TValue Convert<TValue>(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            return default;
        }

        if (typeof(TValue).IsEnum)
        {
            return (TValue)Enum.Parse(typeof(TValue), content);
        }

        if (!typeof(TValue).IsClass)
        {
            return (TValue)TypeDescriptor.GetConverter(typeof(TValue))
                                         .ConvertFrom(content);
        }

        return JsonSerializer.Deserialize<TValue>(content);
    }
}