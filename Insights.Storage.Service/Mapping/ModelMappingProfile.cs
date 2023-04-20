using AutoMapper;
using Nerosoft.Insights.Framework;
using Nerosoft.Insights.Storage.Domain;

namespace Nerosoft.Insights.Storage.Mapping;

public class ModelMappingProfile : Profile
{
    public ModelMappingProfile()
    {
        CreateMap<Models.Binary, Binary>();
        CreateMap<Models.Thread, Domain.Thread>();
        CreateMap<Models.Exception, Domain.Exception>();
        CreateMap<Models.StackFrame, StackFrame>();

        CreateMap<Models.StartSessionLog, Session>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Manufacturer, options => options.MapFrom(src => src.Device.OemName))
            .ForMember(dest => dest.OemName, options => options.MapFrom(src => src.Device.OemName))
            .ForMember(dest => dest.Model, options => options.MapFrom(src => src.Device.Model))
            .ForMember(dest => dest.OsName, options => options.MapFrom(src => src.Device.OsName))
            .ForMember(dest => dest.OsVersion, options => options.MapFrom(src => src.Device.OsVersion))
            .ForMember(dest => dest.OsBuild, options => options.MapFrom(src => src.Device.OsBuild))
            .ForMember(dest => dest.Locale, options => options.MapFrom(src => src.Device.Locale))
            .ForMember(dest => dest.AppVersion, options => options.MapFrom(src => src.Device.AppVersion))
            .ForMember(dest => dest.AppBuild, options => options.MapFrom(src => src.Device.AppBuild))
            .ForMember(dest => dest.CarrierName, options => options.MapFrom(src => src.Device.CarrierName))
            .ForMember(dest => dest.CarrierCountry, options => options.MapFrom(src => src.Device.CarrierCountry));

        CreateMap<Models.EventLog, Event>()
            .ForMember(dest => dest.SessionId, options => options.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Properties, options => options.MapFrom(GetEventProperties));

        CreateMap<Models.HandledErrorLog, Error>()
            .ForMember(dest => dest.SessionId, options => options.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Type, options => options.MapFrom(src => "HandledError"))
            .ForMember(dest => dest.Id, options => options.ConvertUsing(new NullableGuidConverter(), src => src.Id))
            .ForMember(dest => dest.Properties, options => options.MapFrom(GetEventProperties));

        CreateMap<Models.ManagedErrorLog, Error>()
            .ForMember(dest => dest.SessionId, options => options.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Type, options => options.MapFrom(src => "ManagedError"))
            .ForMember(dest => dest.Id, options => options.ConvertUsing(new NullableGuidConverter(), src => src.Id));

        CreateMap<Models.PageLog, Page>()
            .ForMember(dest => dest.SessionId, options => options.MapFrom(src => src.Sid))
            .ForMember(dest => dest.Properties, options => options.MapFrom(GetEventProperties));
    }

    private static List<Property> GetEventProperties(Models.EventLog source, Log destination)
    {
        var properties = source.Properties ??= new Dictionary<string, string>();
        if (source.TypedProperties?.Count > 0)
        {
            foreach (var property in source.TypedProperties)
            {
                if (properties.ContainsKey(property.Name))
                {
                    continue;
                }

                properties.Add(property.Name, property.Value);
            }
        }

        {
        }

        return properties.Select(t => new Property { Name = t.Key, Value = t.Value }).ToList();
    }

    private static List<Property> GetEventProperties(Models.LogWithProperties source, Log destination)
    {
        var properties = source.Properties ??= new Dictionary<string, string>();
        if (source.Properties?.Count > 0)
        {
            foreach (var property in source.Properties)
            {
                if (properties.ContainsKey(property.Key))
                {
                    continue;
                }

                properties.Add(property.Key, property.Value);
            }
        }

        {
        }

        return properties.Select(t => new Property { Name = t.Key, Value = t.Value }).ToList();
    }
}