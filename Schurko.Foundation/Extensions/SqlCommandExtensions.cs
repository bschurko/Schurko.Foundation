// Decompiled with JetBrains decompiler
// Type: PNI.Extensions.SqlCommandExtensions
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;


#nullable enable
namespace PNI.Extensions
{
  public static class SqlCommandExtensions
  {
    public static string AsString(this SqlParameterCollection col)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (col != null)
      {
        foreach (SqlParameter sqlParameter in (DbParameterCollection) col)
        {
          if (((DbParameter) sqlParameter).Direction != ParameterDirection.ReturnValue)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(", \r\n");
            string format = "{0}={1}";
            switch (sqlParameter.SqlDbType)
            {
              case SqlDbType.Char:
              case SqlDbType.DateTime:
              case SqlDbType.NChar:
              case SqlDbType.NText:
              case SqlDbType.NVarChar:
              case SqlDbType.Text:
              case SqlDbType.VarChar:
              case SqlDbType.Xml:
              case SqlDbType.Date:
              case SqlDbType.DateTime2:
                format = "{0}='{1}'";
                break;
            }
            object obj = ((DbParameter) sqlParameter).Value;
            if (obj == DBNull.Value)
              format = "{0}=null";
            if (obj is bool)
              obj = (object) ((bool) obj ? 1 : 0);
            if (((DbParameter) sqlParameter).Direction == ParameterDirection.InputOutput || ((DbParameter) sqlParameter).Direction == ParameterDirection.Output)
              format = string.Format("out {0}", (object) format);
            stringBuilder.AppendFormat(format, (object) ((DbParameter) sqlParameter).ParameterName, obj);
          }
        }
      }
      if (stringBuilder.Length > 0)
        stringBuilder.Append("\r\n");
      return stringBuilder.ToString();
    }

    public static bool SafeAdd(
      this SqlParameterCollection col,
      string paramName,
      object paramValue)
    {
      if (col == null || ((DbParameterCollection) col).Count == 0)
        return false;
      if (!paramName.StartsWith("@"))
        paramName = "@" + paramName;
      bool flag = false;
      if (((DbParameterCollection) col).Contains(paramName))
      {
        ((DbParameter) col[paramName]).Value = paramValue;
        flag = true;
      }
      return flag;
    }

    public static string AsSQL(this SqlCommand cmd)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string format = "EXEC {0}\r\n {1}";
      stringBuilder.AppendFormat("-- {0}\r\n --{1}\r\n", (object) ((DbConnection) cmd.Connection).ConnectionString, (object) ((DbCommand) cmd).CommandType);
      stringBuilder.AppendFormat("USE {0}\r\n", (object) ((DbConnection) cmd.Connection).Database);
      if (((DbParameterCollection) cmd.Parameters).Contains("@RETURN_VALUE"))
      {
        stringBuilder.Append("DECLARE @RETURN_VALUE int\r\n");
        format = "EXEC\t@return_value = {0}\r\n {1}\r\nSELECT\t'Return Value' = @return_value";
      }
      stringBuilder.AppendFormat(format, (object) ((DbCommand) cmd).CommandText, (object) cmd.Parameters.AsString());
      return stringBuilder.ToString();
    }

    public static int ReturnValue(this SqlCommand cmd)
    {
      if (cmd == null)
        throw new ArgumentNullException(nameof (cmd), "The command object is null, cannot call the Extension method ReturnValue()");
      return ((DbCommand) cmd).CommandType != CommandType.StoredProcedure || !((DbParameterCollection) cmd.Parameters).Contains("@RETURN_VALUE") ? 0 : (int) ((DbParameter) cmd.Parameters["@RETURN_VALUE"]).Value;
    }

    public static void ValidateReturnValue(this SqlCommand cmd)
    {
      int num = cmd.ReturnValue();
      if (num != 0)
        throw new Exception(string.Format("Sql Error code returned for {0} - [{1}]\n\rSQL - {2}", (object) ((DbCommand) cmd).CommandText, (object) num, (object) cmd.AsSQL()));
    }

    public static int GetIdentifier(this SqlCommand cmd, string identifierField)
    {
      if (string.IsNullOrEmpty(identifierField))
        throw new ArgumentNullException(nameof (identifierField));
      if (!((DbParameterCollection) cmd.Parameters).Contains(identifierField))
        throw new IndexOutOfRangeException("The specified field is not contained within the Command.Parameters collection : " + identifierField);
      if (!identifierField.StartsWith("@"))
        identifierField = "@" + identifierField;
      object obj = ((DbParameter) cmd.Parameters[identifierField]).Value;
      return obj != DBNull.Value ? (int) obj : throw new InvalidOperationException("Idenfifier field NULL");
    }
  }
}
