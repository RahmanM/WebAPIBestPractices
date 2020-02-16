using Dotnet.Core.Todos.Data;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dotnet.Core.Todos.Api.Action_Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachingAttribute : ActionFilterAttribute
    {
        private readonly string _CacheName;
        private bool _FoundInCache = false;

        public CachingAttribute(string cacheName)
        {
            _CacheName = cacheName;
            Cache = new CachingService();
        }

        public IAppCache Cache { get; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cached = Cache.Get<IEnumerable<TodoDto>>(_CacheName);

            if(cached != null)
            {
                _FoundInCache = true;
                context.Result = new ObjectResult(cached);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!_FoundInCache)
            {
                var todos = context.Result;
                //Cache.Add<IEnumerable<TodoDto>>(_CacheName, todos);
            }
        }
    }

}
