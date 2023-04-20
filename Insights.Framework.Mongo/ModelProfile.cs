using System.Linq.Expressions;
using System.Reflection;

namespace Nerosoft.Insights.Framework;

public class ModelProfile
{
    private ModelKeyBuilder _keyBuilder;
    internal string KeyName => _keyBuilder?.Key;

    internal Type KeyType => _keyBuilder?.Type;

    internal string CollectionName { get; private set; }

    /// <summary>
    /// 是否自动生成Id
    /// </summary>
    internal bool AutoGenerateId => _keyBuilder?.AutoGenerateId ?? true;

    internal bool? BypassDocumentValidation { get; set; }

    public ModelKeyBuilder HasKey(string key, Type type = null)
    {
        _keyBuilder = new ModelKeyBuilder(key, type);
        return _keyBuilder;
    }

    public ModelProfile ToCollection(string name)
    {
        CollectionName = name;
        return this;
    }

    public ModelProfile BypassValidation(bool value)
    {
        BypassDocumentValidation = value;
        return this;
    }
}

public class ModelProfile<TModel> : ModelProfile
{
    private readonly Dictionary<string, ModelPropertyBuilder> _properties = new();

    internal IReadOnlyDictionary<string, ModelPropertyBuilder> Properties => _properties;

    public ModelKeyBuilder HasKey(Expression<Func<TModel, object>> selector)
    {
        var property = GetProperty(selector);
        return HasKey(property.Name, property.PropertyType);
    }

    public ModelPropertyBuilder HasProperty(Expression<Func<TModel, object>> selector)
    {
        var property = GetProperty(selector);
        var builder = new ModelPropertyBuilder(property.Name, property.PropertyType);
        _properties.Add(property.Name, builder);
        return builder;
    }

    /// <summary>
    /// Extracts the property from a property expression.
    /// </summary>
    /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
    /// <param name="expression">The property expression (e.g. p =&gt; p.PropertyName)</param>
    /// <returns>The name of the property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="expression" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the expression is:<br />
    /// Not a <see cref="MemberExpression" /><br />
    /// The <see cref="MemberExpression" /> does not represent a property.<br />
    /// Or, the property is static.</exception>
    public static PropertyInfo GetProperty<T>(Expression<Func<T>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        if (expression.Body is not MemberExpression memberExpression)
        {
            throw new ArgumentException("The expression is not a member access expression.", nameof(expression));
        }

        var property = memberExpression.Member as PropertyInfo;
        if (property == null)
        {
            throw new ArgumentException("The member access expression does not access a property.", nameof(expression));
        }

        return property;

        //var getMethod = property.GetMethod;
        //if (getMethod.IsStatic)
        //{
        //    throw new ArgumentException("The referenced property is a static property.", nameof(expression));
        //}

        //return memberExpression.Member.Name;
    }

    /// <summary>
    /// Extracts the property from a property expression.
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expression)
    {
        return GetProperty<T, object>(expression);
    }

    /// <summary>
    /// Extracts the property from a property expression.
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private static PropertyInfo GetProperty<T, TResult>(Expression<Func<T, TResult>> expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        PropertyInfo result;

        if (expression.Body.NodeType == ExpressionType.Convert)
        {
            result = ((MemberExpression)((UnaryExpression)expression.Body).Operand).Member as PropertyInfo;
        }
        else
        {
            result = ((MemberExpression)expression.Body).Member as PropertyInfo;
        }

        if (result != null)
        {
            return result;
        }

        throw new ArgumentException($"Expression '{expression}' does not refer to a property.");
    }
}