﻿namespace News.Contracts.V1
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
            public const string Refresh = Base + "/identity/refresh";
        }

        public static class Users
        {
            public const string GetAll = Base + "/users";
            public const string Get = Base + "/user/{email}";
            public const string Add = Base + "/users";
            public const string Delete = Base + "/user/{id}";
            public const string Update = Base + "/user/{email}";
            public const string Change = Base + "/userData/{email}";
        }

        public static class Groups
        {
            public const string GetAll = Base + "/groups";
            public const string Get = Base + "/groups/{groupName}";
            public const string Create = Base + "/groups";
            public const string Delete = Base + "/groups/{groupName}";
        }

        public static class Subjects
        {
            public const string GetAll = Base + "/subjects";
            public const string Get = Base + "/subjects/{subName}";
            public const string GetByUser = Base + "/subjectsByUserId/{userId}";
            public const string Create = Base + "/subjects";
            public const string Delete = Base + "/subjects/{subName}";
            public const string Update = Base + "/subjects/{subName}";
        }

        public static class Excersises
        {
            public const string GetAll = Base + "/excersises";
            public const string Save = Base + "/excersises/save";
            public const string GetById = Base + "/excersise/{id}";
            public const string Get = Base + "/excersises/{subjectName}";
            public const string Create = Base + "/excersises";
            public const string Delete = Base + "/excersises/{id}";
            public const string Update = Base + "/excersises/{id}";
            public const string GetMarks = Base + "/marks/{id}";

        }
    }
}