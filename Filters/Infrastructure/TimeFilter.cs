using Filters.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Filters.Infrastructure
{
    /// <summary>
    /// Гибридный фильтр действий/результатов, хранит инфу о времени.
    /// Фильтры которые опираются на DI применятются через другой аттрибут,
    /// поэтому мы наследуемся от интерфейсов.
    /// </summary>
    public class TimeFilter : IAsyncActionFilter, IAsyncResultFilter
    {
        private ConcurrentQueue<double> _actionTimes = new ConcurrentQueue<double>();
        private ConcurrentQueue<double> _resultTimes = new ConcurrentQueue<double>();
        private IFilterDiagnostics _diagnostics;
        public TimeFilter(IFilterDiagnostics diagnostics)
        {
            _diagnostics = diagnostics;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var timer = Stopwatch.StartNew();
            await next();
            timer.Stop();
            _actionTimes.Enqueue(timer.Elapsed.TotalMilliseconds);
            _diagnostics.AddMessage($@"Action time: {timer.Elapsed.TotalMilliseconds} ms Average: {_actionTimes.Average():F2}");
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var timer = Stopwatch.StartNew();
            await next();
            timer.Stop();
            _resultTimes.Enqueue(timer.Elapsed.TotalMilliseconds);
            _diagnostics.AddMessage($@"Result time: {timer.Elapsed.TotalMilliseconds} ms Average: {_resultTimes.Average():F2}");
        }
    }
}
