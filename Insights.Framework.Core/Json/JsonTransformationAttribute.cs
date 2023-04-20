using System;

namespace Nerosoft.Insights.Framework;

/// <summary>
/// Instructs the Microsoft.Rest.Serialization.TransformationJsonConverter to 
/// transform properties of the type based on dot convention.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class JsonTransformationAttribute : Attribute
{
}