namespace RateAvail.Api
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return "OK";
            };
        }
    }
}