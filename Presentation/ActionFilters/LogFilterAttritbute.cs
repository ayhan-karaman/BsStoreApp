using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.LogModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NLog.Fluent;
using Services.Contracts;

namespace Presentation.ActionFilters
{
    public class LogFilterAttritbute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttritbute(ILoggerService logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
        }

        private string Log(string modelName, RouteData routeData)
        {
            var logDetails = new LogDetails
            {
                ModelName = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["action"]
            };
            if(routeData.Values.Count >= 3)
            {
                  logDetails.Id = routeData.Values["Id"];
            }

            return logDetails.ToString();
        }
    }
}