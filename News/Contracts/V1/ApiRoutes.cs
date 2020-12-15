namespace News.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

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
        }

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/user/{id}";
            public const string Add = Base + "/user";
            public const string Delete = Base + "/user/{id}";
            public const string Update = Base + "/user/{id}";
            //public const string Change = Base + "/userData/{id}";
        }

        public static class Forms
        {
            public const string GetAll = Base + "/forms";
            public const string Get = Base + "/form/{id}";
            public const string Create = Base + "/form";
            public const string Update = Base + "/form";
            public const string Delete = Base + "/form/{id}";
        }

        public static class Subjects
        {
            public const string GetAll = Base + "/subjects";
            public const string Get = Base + "/subject/{id}";
            public const string Create = Base + "/subject";
            public const string Update = Base + "/subject";
            public const string Delete = Base + "/subject/{id}";

            public const string ForTeatchers = Base + "/subjectsForTeatcher/{id}";
            public const string ForStudent = Base + "/subjectsForStudent/{id}";
        }

        public static class Excersises
        {
            public const string GetAll = Base + "/exercises";
            public const string Get = Base + "/exercise/{id}";
            public const string Save = Base + "/exercise/save";
            public const string GetBySubject = Base + "/exercises/{subjectId}";

            public const string Create = Base + "/exercise";
            public const string Update = Base + "/exercise";
            public const string Delete = Base + "/exercise/{id}";

            public const string GetMarks = Base + "/marks/{id}";
            public const string CHeckExercise = Base + "/checkExercise";


        }
    }
}