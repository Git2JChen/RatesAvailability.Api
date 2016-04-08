﻿using Nancy;

namespace RateAvail.Api
{
    public class RatesAvailModule : NancyModule
    {
        public RatesAvailModule()
        {
            Get["/"] = parameters =>
            {
                return "Nancy.HttpStatusCode is OK";
            };

            Get["/RatesAvail"] = parameters =>
            {
                const string ratesAvail =
                    "{" +
                        "\"availabilities\":[" +
                            "{" +
                                "\"availability\":[" +
                                    "{" +
                                    "}" +
                                "]" +
                            "}" +
                        "]" +
                    "}";

                return ratesAvail;
            };
        }
    }
}