// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Messaging.Redis.RedisService
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;


#nullable enable
namespace Schurko.Foundation.Messaging.Redis
{
  public class RedisService
  {
    private ConnectionMultiplexer _redisConnection;
    private IDatabase _db;

    private string _hostName { get; set; }

    private string _port { get; set; }

    public RedisService(string hostName, string port)
    {
      this._hostName = hostName;
      this._port = port;
      this._redisConnection = ConnectionMultiplexer.Connect(string.Format("{0}:{1}", (object) hostName, (object) port), (TextWriter) null);
      this._db = this._redisConnection.GetDatabase(-1, (object) null);
    }
        
    public void SetStringValue(string key, string value) => this._db.StringSet(key, value, new TimeSpan?(), false, (When) 0, (CommandFlags) 0);

    public string GetStringValue(string key) => this._db.StringGet(key);

    public bool SetObjectValue<T>(string key, T value)
    {
      string str = JsonConvert.SerializeObject((object) value);
      return this._db.StringSet(key, str, new TimeSpan?(), false, (When) 0, (CommandFlags) 0);
    }

    public T? GetObjectData<T>(string key) where T : class => JsonConvert.DeserializeObject<T>(this._db.StringGet(key));
  }
}
