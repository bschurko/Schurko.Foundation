// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.ReflectionExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


#nullable enable
namespace PNI.Extensions
{
  public static class ReflectionExtensions
  {
    private const string NullAsString = "<null />";
    private static readonly object TypeDefLock = new object();
    private static readonly Dictionary<Type, FieldInfo[]> TypeDefinitions = new Dictionary<Type, FieldInfo[]>();

    public static T GetAttribute<T>(this MemberInfo property) where T : Attribute => ReflectionExtensions.GetCustomAttribute(typeof (T), property) as T;

    private static object GetCustomAttribute(Type attributeType, MemberInfo property)
    {
      object[] customAttributes = property.GetCustomAttributes(true);
      for (int index = 0; index < customAttributes.Length; ++index)
      {
        if (customAttributes[index].GetType() == attributeType || customAttributes[index].GetType().GetInterface(attributeType.Name) != (Type) null)
          return customAttributes[index];
      }
      return (object) null;
    }

    public static string ReflectToString(this object target) => target.ReflectToString(0);

    public static string ReflectToString(this object target, int recursion) => target.ReflectToString(recursion, ", ");

    public static string ReflectToString(this object target, int recursion, string delimiter)
    {
      if (target == null)
        return "<null />";
      Type type = target.GetType();
      if (type == typeof (string))
        return (string) target;
      MethodInfo method = type.GetMethod("ToString", Type.EmptyTypes);
      if (method.DeclaringType != typeof (object) && method.GetAttribute<ReflectToStringIgnoreAttribute>() == null)
        return target.ToString();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(type.FullName);
      stringBuilder.Append("[");
      int length = stringBuilder.Length;
      if (target is IEnumerable)
      {
        foreach (object target1 in (IEnumerable) target)
          stringBuilder.Append(target1.ReflectToString());
      }
      else
      {
        foreach (FieldInfo fieldInfo in ReflectionExtensions.GetFieldInfo(type))
        {
          if (stringBuilder.Length > length)
            stringBuilder.Append(delimiter);
          stringBuilder.Append(fieldInfo.Name);
          stringBuilder.Append("=");
          object target2 = fieldInfo.GetValue(target);
          if (target2 == null)
            stringBuilder.Append("<null />");
          else if (recursion > 0)
            stringBuilder.Append(target2.ReflectToString(recursion - 1, delimiter));
          else
            stringBuilder.Append(target2.ToString());
        }
      }
      stringBuilder.Append("]");
      return stringBuilder.ToString();
    }

    private static IEnumerable<FieldInfo> GetFieldInfo(Type objectType)
    {
      lock (ReflectionExtensions.TypeDefLock)
      {
        FieldInfo[] fieldInfo;
        if (ReflectionExtensions.TypeDefinitions.TryGetValue(objectType, out fieldInfo))
          return (IEnumerable<FieldInfo>) fieldInfo;
      }
      FieldInfo[] array = ((IEnumerable<FieldInfo>) objectType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<FieldInfo>((Func<FieldInfo, bool>) (fi => fi.GetAttribute<ReflectToStringIgnoreAttribute>() == null)).ToArray<FieldInfo>();
      lock (ReflectionExtensions.TypeDefLock)
      {
        if (!ReflectionExtensions.TypeDefinitions.ContainsKey(objectType))
          ReflectionExtensions.TypeDefinitions.Add(objectType, array);
      }
      return (IEnumerable<FieldInfo>) array;
    }
  }
}
