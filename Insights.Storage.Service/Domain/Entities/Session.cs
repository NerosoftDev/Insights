namespace Nerosoft.Insights.Storage.Domain;

public class Session : Log
{
    public string OemName { get; set; }

    public string Model { get; set; }

    public string Manufacturer { get; set; }

    public string OsName { get; set; }

    public string OsVersion { get; set; }

    public string OsBuild { get; set; }

    public string Locale { get; set; }

    public string AppVersion { get; set; }

    public string AppBuild { get; set; }

    public string CarrierName { get; set; }

    public string CarrierCountry { get; set; }
}