using Filters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    public class DefaultFilterDiagnostics : IFilterDiagnostics
    {
        private List<string> _messages = new List<string>();
        public IEnumerable<string> Messages => _messages;
        public void AddMessage(string message) => 
            _messages.Add(message);
    }
}
