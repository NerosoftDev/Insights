using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Device characteristics.
/// </summary>
public class Device
{
    /// <summary>
    /// Gets or sets name of the SDK. Consists of the name of the SDK and
    /// the platform, e.g. "appcenter.ios", "hockeysdk.android".
    ///
    /// </summary>
    [JsonProperty(PropertyName = "sdkName")]
    public string SdkName { get; set; }

    /// <summary>
    /// Gets or sets version of the SDK in semver format, e.g. "1.2.0" or
    /// "0.12.3-alpha.1".
    ///
    /// </summary>
    [JsonProperty(PropertyName = "sdkVersion")]
    public string SdkVersion { get; set; }

    /// <summary>
    /// Gets or sets version of the wrapper SDK in semver format. When the
    /// SDK is embedding another base SDK (for example Xamarin.Android
    /// wraps Android), the Xamarin specific version is populated into this
    /// field while sdkVersion refers to the original Android SDK.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "wrapperSdkVersion")]
    public string WrapperSdkVersion { get; set; }

    /// <summary>
    /// Gets or sets name of the wrapper SDK. Consists of the name of the
    /// SDK and the wrapper platform, e.g. "appcenter.xamarin",
    /// "hockeysdk.cordova".
    ///
    /// </summary>
    [JsonProperty(PropertyName = "wrapperSdkName")]
    public string WrapperSdkName { get; set; }

    /// <summary>
    /// Gets or sets device model (example: iPad2,3).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "model")]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets device manufacturer (example: HTC).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "oemName")]
    public string OemName { get; set; }

    /// <summary>
    /// Gets or sets OS name (example: iOS). The following OS names are
    /// standardized (non-exclusive): Android, iOS, macOS, tvOS, Windows.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "osName")]
    public string OsName { get; set; }

    /// <summary>
    /// Gets or sets OS version (example: 9.3.0).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "osVersion")]
    public string OsVersion { get; set; }

    /// <summary>
    /// Gets or sets OS build code (example: LMY47X).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "osBuild")]
    public string OsBuild { get; set; }

    /// <summary>
    /// Gets or sets API level when applicable like in Android (example:
    /// 15).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "osApiLevel")]
    public int? OsApiLevel { get; set; }

    /// <summary>
    /// Gets or sets language code (example: en-US).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "locale")]
    public string Locale { get; set; }

    /// <summary>
    /// Gets or sets the offset in minutes from UTC for the device time
    /// zone, including daylight savings time.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "timeZoneOffset")]
    public int TimeZoneOffset { get; set; }

    /// <summary>
    /// Gets or sets screen size of the device in pixels (example:
    /// 640x480).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "screenSize")]
    public string ScreenSize { get; set; }

    /// <summary>
    /// Gets or sets application version name, e.g. 1.1.0
    ///
    /// </summary>
    [JsonProperty(PropertyName = "appVersion")]
    public string AppVersion { get; set; }

    /// <summary>
    /// Gets or sets carrier name (for mobile devices).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "carrierName")]
    public string CarrierName { get; set; }

    /// <summary>
    /// Gets or sets carrier country code (for mobile devices).
    ///
    /// </summary>
    [JsonProperty(PropertyName = "carrierCountry")]
    public string CarrierCountry { get; set; }

    /// <summary>
    /// Gets or sets the app's build number, e.g. 42.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "appBuild")]
    public string AppBuild { get; set; }

    /// <summary>
    /// Gets or sets the bundle identifier, package identifier, or
    /// namespace, depending on what the individual plattforms use,  .e.g
    /// com.microsoft.example.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "appNamespace")]
    public string AppNamespace { get; set; }

    /// <summary>
    /// Gets or sets label that is used to identify application code
    /// 'version' released via Live Update beacon running on device
    ///
    /// </summary>
    [JsonProperty(PropertyName = "liveUpdateReleaseLabel")]
    public string LiveUpdateReleaseLabel { get; set; }

    /// <summary>
    /// Gets or sets identifier of environment that current application
    /// release belongs to, deployment key then maps to environment like
    /// Production, Staging.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "liveUpdateDeploymentKey")]
    public string LiveUpdateDeploymentKey { get; set; }

    /// <summary>
    /// Gets or sets hash of all files (ReactNative or Cordova) deployed to
    /// device via LiveUpdate beacon. Helps identify the Release version on
    /// device or need to download updates in future.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "liveUpdatePackageHash")]
    public string LiveUpdatePackageHash { get; set; }

    /// <summary>
    /// Gets or sets version of the wrapper technology framework (Xamarin
    /// runtime version or ReactNative or Cordova etc...). See
    /// wrappersdkname to see if this version refers to Xamarin or
    /// ReactNative or other.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "wrapperRuntimeVersion")]
    public string WrapperRuntimeVersion { get; set; }

    /// <summary>
    /// Gets or sets flag indicating if the device is jailbroken
    /// </summary>
    [JsonProperty(PropertyName = "jailbreak")]
    public bool? Jailbreak { get; set; }
}