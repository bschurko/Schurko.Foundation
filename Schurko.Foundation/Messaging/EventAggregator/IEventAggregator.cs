// Decompiled with JetBrains decompiler
// Type: Schurko.Foundation.Messaging.EventAggregator.IEventAggregator
// Assembly: Schurko.Foundation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1385A3BB-C317-4A00-BA85-BA0E3328BBAC
// Assembly location: E:\C Drive\nuget\Schurko.Foundation\src\lib\net7.0\Schurko.Foundation.dll

using System;


#nullable enable
namespace Schurko.Foundation.Messaging.EventAggregator
{
  public interface IEventAggregator
  {
    void Subscribe<TEvent>(Action<Type, TEvent> action);

    void Unsubscribe<TEvent>(Action<Type, TEvent> action = null);

    void Publish<TEvent>();

    void Publish<TEvent>(TEvent publishedEvent);

    void Publish<TEvent>(TEvent publishedEvent, Type target);
  }
}
