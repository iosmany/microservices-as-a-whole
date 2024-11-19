using Microservices.Base.Domain;

namespace Microservices.Core.Domain.Domain.Entities
{
    internal class State : Entity
    {
        protected State()
        {
        }

        public State(string name, string code, Country country)
        {
            Name = name;
            Code = code;
            Country = country;
        }

        public string Name { get; private set; }
        public string Code { get; private set; }
        public virtual Country Country { get; private set; }

        readonly List<Tax> _taxes = new List<Tax>();
        public virtual IReadOnlyCollection<Tax> Taxes => _taxes.AsReadOnly();
    }
}
