using FS.MinhaApi.Api.Filters;
using FS.MinhaApi.Api.Formatters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FS.MinhaApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            config.Formatters.Add(new CsvMediaTypeFormatter());

            config.Filters.Add(new FillResponseWithHATEOASAttribute());

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
