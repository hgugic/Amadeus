using AmadeusScanner.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AmadeusScanner.Common.Url
{
    public class UrlGenerator<T> where T : class
    {
        public static string GenerateUrl(T value, string baseUrl)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            foreach (PropertyInfo prop in value.GetType().GetProperties())
            {                
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                ApiGetAttribute apiGetAttribute = (ApiGetAttribute)Attribute.GetCustomAttribute(prop, typeof(ApiGetAttribute));
                var val = prop.GetValue(value, null);

                if (val == null)
                    continue;
                
                if (apiGetAttribute != null && apiGetAttribute.Ignore == false)
                {
                    if (type == typeof(DateTime))
                    {
                        queryString.Add(apiGetAttribute.Name, ((DateTime)prop.GetValue(value)).ToString("yyyy-MM-dd"));
                        continue;
                    }

                    queryString.Add(apiGetAttribute.Name, prop.GetValue(value).ToString());
                }
                else
                {
                    if (type == typeof(DateTime))
                    {
                        queryString.Add(prop.Name, ((DateTime)prop.GetValue(value)).ToString("yyyy-MM-dd"));
                        continue;
                    }

                    queryString.Add(prop.Name, prop.GetValue(value).ToString());
                }

            }

            return $@"{baseUrl}?{queryString}";
        }
    }

}
