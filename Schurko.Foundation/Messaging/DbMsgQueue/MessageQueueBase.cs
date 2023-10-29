// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Messaging.DbMsgQueue.MessageQueueBase
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Messaging.DbMsgQueue
{
  public class MessageQueueBase
  {
    private string _identifier;

    public Guid MessageId { get; set; }

    public string Identifier
    {
      get => this._identifier == null ? string.Empty : this._identifier.Trim();
      set => this._identifier = value;
    }

    public string SegmentMsg { get; set; }

    public MessageQueueBase()
    {
    }

    public MessageQueueBase(Guid messageId, string identifier, string segmentMsg)
    {
      this.MessageId = messageId;
      this.SegmentMsg = segmentMsg;
      this.Identifier = identifier;
    }

    public MessageQueueBase(string identifier, string segmentMsg)
    {
      this.MessageId = Guid.Empty;
      this.SegmentMsg = segmentMsg;
      this.Identifier = identifier;
    }
  }
}
