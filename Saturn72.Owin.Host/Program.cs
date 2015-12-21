
using System;
using System.Net.Http;
using System.Web.Mvc;
using FluentValidation.Mvc;
using Microsoft.Owin.Hosting;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Logging;
using Saturn72.Web.Framework;
using Saturn72.Web.UI;

namespace Saturn72.Owin.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                PostStartup();

                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }

        private static void PostStartup()
        {
            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new Saturn72ValidatorFactory()));
            ApplicationStratLog();
        }

        private static void ApplicationStratLog()
        {
            try
            {
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("Application started");
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }
    }
}
