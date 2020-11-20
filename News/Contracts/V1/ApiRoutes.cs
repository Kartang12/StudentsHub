namespace News.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetAll = Base + "/posts";

            public const string Update = Base + "/posts/{postId}";

            public const string Delete = Base + "/posts/{postId}";

            public const string Get = Base + "/posts/{postId}";

            public const string Create = Base + "/posts";
            
            public const string ByTag= Base + "/postsByTag/{tagName}";

            public const string GetByAuthorName = Base + "/posters/{userName}";
        }

        public static class Users
        {
            public const string GetAll = Base + "/users";
            
            public const string Get = Base + "/user/{userName}";
            
            public const string Add = Base + "/users";
            
            public const string Delete = Base + "/user/{userName}";
            
            public const string Update = Base + "/user/{userName}";
            
            public const string Change = Base + "/userData/{userName}";
        }
        
        public static class Businesses
        {
            public const string GetAll = Base + "/business";

            public const string Add = Base + "/business";

            public const string Delete = Base + "/business";
        }

        public static class Roles
        {
            public const string GetAll = Base + "/roles";

            public const string Add = Base + "/roles";

            public const string Delete = Base + "/roles";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            
            public const string Register = Base + "/identity/register";
            
            public const string Refresh = Base + "/identity/refresh";
        }
        
        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            
            public const string Get = Base + "/tags/{tagName}";

            public const string Create = Base + "/tags";
            
            public const string Delete = Base + "/tags/{tagName}";
        }
    }
}