using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Base.Commands
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class ExchangeAttribute : Attribute
    {
        readonly string _exchangeName;
        public string ExchangeName => _exchangeName;

        readonly bool _durable;
        public bool Durable => _durable;

        readonly bool _balanced;
        public bool Balanced => _balanced;


        public ExchangeAttribute(string exchangeName, bool durable=false, bool balanced=true)
        {
            _durable = durable;
            _balanced = balanced;
            _exchangeName = exchangeName;
        }
    }
}
