namespace DNQ.DataFeed.Api;

public static class ApiEndPoint
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string VersionBase = $"{ApiBase}/v1";
        public static class Sites
        {
            private const string Base = $"{VersionBase}/sites";

            public const string Create = Base;

            public const string Update = $"{Base}/{{id}}";
        }
    }
}
