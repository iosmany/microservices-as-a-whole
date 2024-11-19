using Microservices.Base.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Domain.Domain.Entities
{
    internal class Tax : Entity
    {
        protected Tax()
        {
        }
        public Tax(string code, string name, State state)
        {
            Code = code;
            Name = name;
            State = state;
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public virtual State State { get; private set; }

    }
}
