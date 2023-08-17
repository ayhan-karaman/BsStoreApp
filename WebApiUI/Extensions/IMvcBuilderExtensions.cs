using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUI.Utilities.Formatters;

namespace WebApiUI.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) => 
         builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        
    }
}