using System.Reflection;

namespace MuseDashMirror.Extensions.CollectionExtensions;

/// <summary>
///     Collection Extension methods for <see cref="Type" />.
/// </summary>
public static class TypeCollectionExtension
{
    /// <summary>
    ///     Get an IEnumerable of <see cref="FieldInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="FieldInfo" /></param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <typeparam name="T">Attribute Type</typeparam>
    /// <returns>A sequence of FieldInfo that match the given attribute type</returns>
    public static IEnumerable<FieldInfo> GetFieldInfosFromTypesByAttribute<T>(this IEnumerable<Type> types,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) where T : Attribute
    {
        return types.SelectMany(type => type.GetFields(flags))
            .Where(f => f.GetCustomAttributes<T>().Any());
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="FieldInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="FieldInfo" /></param>
    /// <param name="attributeType">Attribute Type</param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <returns>A sequence of FieldInfo that match the given attribute type</returns>
    public static IEnumerable<FieldInfo> GetFieldInfosFromTypesByAttribute(this IEnumerable<Type> types, Type attributeType,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        return types.SelectMany(type => type.GetFields(flags))
            .Where(f => f.GetCustomAttributes(attributeType).Any());
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="MemberInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="MemberInfo" /></param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <typeparam name="T">Attribute Type</typeparam>
    /// <returns>A sequence of MemberInfo that match the given attribute type</returns>
    public static IEnumerable<MemberInfo> GetMemberInfosFromTypesByAttribute<T>(this IEnumerable<Type> types,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) where T : Attribute
    {
        return types.SelectMany(type => type.GetMembers(flags))
            .Where(m => m.GetCustomAttributes<T>().Any());
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="MemberInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="MemberInfo" /></param>
    /// <param name="attributeType">Attribute Type</param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <returns>A sequence of MemberInfo that match the given attribute type</returns>
    public static IEnumerable<MemberInfo> GetMemberInfosFromTypesByAttribute(this IEnumerable<Type> types, Type attributeType,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        return types.SelectMany(type => type.GetMembers(flags))
            .Where(m => m.GetCustomAttributes(attributeType).Any());
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="MethodInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="MethodInfo" /></param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <typeparam name="T">Attribute Type</typeparam>
    /// <returns>A sequence of MethodInfo that match the given attribute type</returns>
    public static IEnumerable<MethodInfo> GetMethodInfosFromTypesByAttribute<T>(this IEnumerable<Type> types,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) where T : Attribute
    {
        return types.SelectMany(type => type.GetMethods(flags)
            .Where(m => m.GetCustomAttributes<T>().Any()));
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="MethodInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="MethodInfo" /></param>
    /// <param name="attributeType">Attribute Type</param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <returns>A sequence of MethodInfo that match the given attribute type</returns>
    public static IEnumerable<MethodInfo> GetMethodInfosFromTypesByAttribute(this IEnumerable<Type> types, Type attributeType,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        return types.SelectMany(type => type.GetMethods(flags)
            .Where(m => m.GetCustomAttributes(attributeType).Any()));
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="PropertyInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="PropertyInfo" /></param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <typeparam name="T">Attribute Type</typeparam>
    /// <returns>A sequence of PropertyInfo that match the given attribute type</returns>
    public static IEnumerable<PropertyInfo> GetPropertyInfosFromTypesByAttribute<T>(this IEnumerable<Type> types,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) where T : Attribute
    {
        return types.SelectMany(type => type.GetProperties(flags))
            .Where(p => p.GetCustomAttributes<T>().Any());
    }

    /// <summary>
    ///     Get an IEnumerable of <see cref="PropertyInfo" /> from an IEnumerable of <see cref="Type" /> by attribute type
    /// </summary>
    /// <param name="types">The type to extract <see cref="PropertyInfo" /></param>
    /// <param name="attributeType">Attribute Type</param>
    /// <param name="flags">
    ///     Defaults are <see cref="BindingFlags.Static" /> | <see cref="BindingFlags.Public" /> | <see cref="BindingFlags.NonPublic" />
    /// </param>
    /// <returns>A sequence of PropertyInfo that match the given attribute type</returns>
    public static IEnumerable<PropertyInfo> GetPropertyInfosFromTypesByAttribute(this IEnumerable<Type> types, Type attributeType,
        BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
    {
        return types.SelectMany(type => type.GetProperties(flags))
            .Where(p => p.GetCustomAttributes(attributeType).Any());
    }
}