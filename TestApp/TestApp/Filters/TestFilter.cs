using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace TestApp.Filters
{
    public class TestFilter : IAsyncPageFilter
    {
        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            throw new NotImplementedException();
        }
    }
}
