using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Interfaces
{
    /// <summary>
    /// Служит для сбора диагностических сообщений во время выполнения фильтра
    /// </summary>
    public interface IFilterDiagnostics
    {
        IEnumerable<string> Messages { get; }
        void AddMessage(string message);
    }
}
