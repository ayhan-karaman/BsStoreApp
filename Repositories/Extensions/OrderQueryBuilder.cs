using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            // Gelen parametre üzerinde değerlerimizi split metodu ile virgül'e göre bölüyoruz
              var orderParams = orderByQueryString.Trim().Split(',');
               // Nesne classmızın property bilgirine Reflection yardımı ile erişim sağlıyoruz
              var propertyInfos = typeof(T)
              .GetProperties(BindingFlags.Public | BindingFlags.Instance);

              // Özelleştirilmiz bir query builder oluşturmak için StringBuilder'den yararlanıyoruz
              var orderQueryBuilder = new StringBuilder();

              // orderParams  gezinerek özelleştirilmiz bir query builder oluşturacağız
              foreach (var param in orderParams)
              {

                   
                    if(string.IsNullOrWhiteSpace(param))
                        continue;
                    
                    var propertyFromQueryName = param.Split(' ')[0];

                    var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                    if(objectProperty is null)
                        continue;
                    var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                    orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
              }
              var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
              return orderQuery;
        }
    }
}