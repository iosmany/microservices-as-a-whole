using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Core.Public
{
    static class Exchanges
    {
        public const string ReceiveOrder = "Core.Public";
        public const string ReceiveRequestResponse = "Core.Interactive";
        public const string ReceiveEvents = "Core.Events";
    }
}
