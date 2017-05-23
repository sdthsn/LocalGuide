using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CityViews
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "City View Api");
                    c.RootUrl(req => new Uri(req.RequestUri, HttpContext.Current.Request.ApplicationPath ?? string.Empty).ToString());
                    //c.IncludeXmlComments(System.Web.Hosting.HostingEnvironment.MapPath("/bin/ApiServices.XML"));

                    //Include OAuth Implicit flow for Swagger

                    //var oauthEndpoint = ConfigurationManager.AppSettings["mulesoftOauthEndpoint"];

                    //c.OAuth2("oauth2")
                    //.Description("OAuth2 Implicit Grant")
                    //.Flow("implicit")
                    //.AuthorizationUrl(oauthEndpoint)
                    //.Scopes(scopes =>
                    //{
                    //    scopes.Add("ImpactDTLApi", "Test Api using Oauth");
                    //});

                    //c.OperationFilter<ApplyOAuthSecurity>();

                })
                .EnableSwaggerUi();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

        }
    }
}
