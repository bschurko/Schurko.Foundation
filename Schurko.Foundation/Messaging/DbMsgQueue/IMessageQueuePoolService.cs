// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Messaging.DbMsgQueue.IMessageQueuePoolService
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


#nullable enable
namespace Schurko.Foundation.Messaging.DbMsgQueue
{
  public interface IMessageQueuePoolService
  {
    Task<string> InMessageQueueAsync(
      string identifier,
      string message,
      int lifeInQueueSeconds = 60,
      int maxLength = 7500);

    Task<MessageQueueBase> DeMessageQueueAsync(string identifier, int numberOfMessage = 1);

    void PurgeMessageQueueAsync(int deQueueTimeoutSeconds = 60);

    Task<bool> DeleteQueueMessageAsync(Guid messageId);

    Task<bool> DeleteQueueMessagesByIdentifierAsync(string identifier);

    Task<IEnumerable<MessageQueueModel>> GetIdentifierByMessageIdAsync(string messageId);

    Task<IEnumerable<MessageQueueModel>> GetMessageIdByIdentifierAsync(string identifier);
  }
}
