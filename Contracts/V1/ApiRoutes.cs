namespace WebApplicationWithDocker.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string root = "api";
        public const string version = "v1";
        public const string _base = root +"/" + version;
        public static class Posts
        {
            public const string GetAll = _base + "/posts";
        }
    }
}
