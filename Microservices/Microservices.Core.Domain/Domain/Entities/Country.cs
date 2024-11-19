using Microservices.Base.Domain;

namespace Microservices.Core.Domain.Domain.Entities
{
    internal class Country : Entity
    {
        protected Country()
        {
        }

        public Country(string code, string name)
        {
            Code= code;
            Name = name;
        }

        public string Code { get; set; }
        public string? Name { get; private set; }

        readonly List<State> _states = new List<State>();
        public IReadOnlyCollection<State> States => _states.AsReadOnly();

    }
}
