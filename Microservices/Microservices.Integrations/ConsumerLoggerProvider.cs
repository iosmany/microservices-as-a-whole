using Microservices.Base.Commands;
using Microservices.Integrations.Masstransit.Consumers;
using System.Reflection;

namespace Microservices.Integrations.Masstransit
{
    using Microservices.Base.Persistence;

    sealed class ConsumerLoggerProvider
    {
        readonly OrderConsumerFactory consumerFactory = new();

        readonly Type readCommitted = typeof(IReadCommittedLevel);
        readonly Type readUnCommitted = typeof(IReadCommittedLevel);
        readonly Type snapShot = typeof(ISnapshotLevel);

        IsolationLevel DetermineIsolation(Type type)
        {
            if (type.Implements(readCommitted))
                return IsolationLevel.ReadCommitted;

            if (type.Implements(readUnCommitted))
                return IsolationLevel.ReadUncommitted;

            if (type.Implements(snapShot))
                return IsolationLevel.Snapshot;

            return IsolationLevel.Serializable;
        }

        Type GetCommandType(Type type)
        {
            var generic = type.GetInterfaces()
                       .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandProcessor<>));
            if (generic is null)
                throw new ArgumentNullException();
            return generic.GetGenericArguments().First();
        }

        public IEnumerable<(Type typo, Type impl)> Execute(IEnumerable<Assembly> assemblies)
        {
            var commandProcessors = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandProcessor<>)))
                .Select(x => new Registration(x, DetermineIsolation(x), GetCommandType(x)))
                .ToList();

            foreach (var _type in commandProcessors)
            {
                yield return consumerFactory.Create(_type.Cmd, _type.IsolationLevel);
            }
        }

        record Registration(Type ImplementationType, IsolationLevel IsolationLevel, Type Cmd);
    }

    public static class TypeExtensions
    {
        public static bool Implements(this Type type, Type interfaceType)
            => interfaceType.IsAssignableFrom(type);
    }

}
