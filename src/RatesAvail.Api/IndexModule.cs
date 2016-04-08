using System;
using Nancy;

namespace RateAvail.Api
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return "Nancy.HttpStatusCode is OK";
            };

            Get["/RatesAvail"] = parameters =>
            {
                return "{}";
            };
        }
    }
}