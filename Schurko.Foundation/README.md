# Schurko.Foundation
A foundation of helper/utility classes, along with services and extensions,

## Caching

Caching class that can use in memory storage or file storage.
Caching includes a tag system to store and keep track of cache items.

## Crypto
Classes to encrypt data or objects. Additionally there are classes to manage certificates and RSA encryption.

## Dapper Service
A dapper helper class for executing stored procedures and raw SQL code.
Plenty of extensions that wrap the core dapper library into one easy service.

## Process Manager
A process manager class for executing applications and tools.

## Hash Util
Utility class for hashing data.

## Settings and Configuration
Simplifies interaction with AppSettings.json and AppSetting.config.

StaticConfigurationManager.cs
AppConfigSettings.cs
 
## File IO Utility Classes
Utility classes that extend the current .NET file/IO classes for both directories and files.

Helpers\FileUtil.cs
Helpers\IOUtility.cs

## Array and CVS Utility
Helper classes where you can parse CVS files and manage them.
Additionally there is an array utility class for extending array functionality.

## CryptoManager 
A simple class to encrypt and decrypt data using AES encryption.

Helpers\CryptoManager.cs

## Heartbeat Writer and Retry Algorithm
A heartbeat diagnostic utility for writing a timestamp at regular intervals to a heartbeat file.
Additionally, the retry algorithm can be used to throttle retry attempts using Fibonacci as the backend algorithm.
Helpers\RetryAlgorithm.cs
Helpers\HeartbeatWriter.cs

## Object Dumper
Takes any object and strips it down to a basic string message that can be used for logs or diagnostics.
Helpers\ObjectDumper.cs

## Extended Interlocked
Simple class for thread safe ATOMIC increment and deincrement values.
Helpers\ExtendedInterlocked.cs 

## Enviroment Util
A class for retriving basic information about a computers CPU and drives.
Helpers\EnvironmentUtility.cs

## String Extensions and Utils
A powerful class full of extensions and utilities for the string object.
Helpers\StringUtility.cs

## Logger
A simple class for creating and retriving the .NET core Logger class.
Logging\Log.cs

## Network Util and Sockets wrapper class
A powerful number of classes that simplify the creation of a socket based server and a socket client for communicating with a socket server.
Additionally, a Network Utility class for getting basic network information.

# Design Patterns and Wrapper classes

Patterns\Composite.cs
Patterns\Factory.cs
Patterns\IRepository.cs
Patterns\NodeT.cs
Patterns\Repository.cs
Patterns\Singleton.cs

Queue\IQueueProcessor.cs
Queue\IQueueRepository.cs
Queue\Queues.cs

Utils\HardwareInfo.cs

Web\UrlBuilder.cs

Xml\XmlSerializerUtils.cs
Xml\XmlUtils.cs


Concurrent\WorkerPool\Administrator.cs
Concurrent\WorkerPool\IAdministrator.cs
Concurrent\WorkerPool\IWorker.cs
Concurrent\WorkerPool\Worker.cs


Identity\Auth\AppUser.cs
Identity\Auth\AppUserBase.cs
Identity\Auth\AppUserStore.cs
Identity\Auth\ClaimsExtension.cs
Identity\Auth\IdentityExtensions.cs
Identity\Impersonation\ICredentialProvider.cs
Identity\Impersonation\SecurityImpersonation.cs

IoC\DI\IoC.cs

IoC\MEF\DependencyInjector.cs
IoC\MEF\Extensions.cs
IoC\MEF\ResolveEntityException.cs

Messaging\DbMsgQueue\IMessageQueuePoolService.cs
Messaging\DbMsgQueue\MessageQueueBase.cs
Messaging\DbMsgQueue\MessageQueueModel.cs
Messaging\DbMsgQueue\MessageQueuePoolService.cs

Messaging\EventAggregator\EventAggregator.cs
Messaging\EventAggregator\IEventAggregator.cs

Messaging\RabbitMQ\RabbitMQService.cs

Messaging\Redis\RedisService.cs

Network\Sockets\SocketBase.cs
Network\Sockets\SocketClient.cs
Network\Sockets\SocketDatagramClient.cs
Network\Sockets\SocketDatagramServer.cs
Network\Sockets\SocketServer.cs
Network\Sockets\SocketServerAsync.cs
Network\Sockets\SocketServerBase.cs


Scheduler\Interfaces\IScheduleSettings.cs
Scheduler\Interfaces\JobEntry.cs
Scheduler\Scheduler\Scheduler.cs
Concurrent\WorkerPool\Models\IJob.cs

Identity\Auth\Identity\AppIdentity.cs

Extensions\ArrayExtensions.cs
Extensions\ByteArrayExtensions.cs
Extensions\CalendarRules.cs
Extensions\DataRowExtensions.cs
Extensions\DataSetExtensions.cs
Extensions\DateTimeExtensions.cs
Extensions\DictionaryExtensions.cs
Extensions\DirectoryExtensions.cs
Extensions\DrawingExtensions.cs
Extensions\EnumerableExtensions.cs
Extensions\EnumeratorExtensions.cs
Extensions\EnumeratorWrapper.cs
Extensions\EventExtensions.cs
Extensions\ExceptionExtensions.cs
Extensions\IfStatement.cs
Extensions\ImageExtensions.cs
Extensions\IntExtensions.cs
Extensions\ObjectExtensions.cs
Extensions\QueryableCacheExtensions.cs
Extensions\ReadOnlyDictionary.cs
Extensions\ReflectionExtensions.cs
Extensions\ReflectToStringIgnoreAttribute.cs
Extensions\SerializeExtensions.cs
Extensions\SqlCommandExtensions.cs
Extensions\StreamExtensions.cs
Extensions\StringExtensions.cs
Extensions\TransformExtension.cs
Extensions\WeekNum.cs
Extensions\XmlReaderExtend.cs
Extensions\IO\Extensions.cs