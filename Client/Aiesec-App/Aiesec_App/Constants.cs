using System;

namespace Aiesec_App
{
    internal class Constants
    {
        // URL of REST service
        public static string RestUrl = "https://besrilankan.herokuapp.com/";
        // Credentials that are hard coded into the REST service

        public static string URL_COMPLAIN = "complain";
        public static string URL_SIGNIN = "auth/signin";
        public static string URL_USER = "user";
        public static string URL_SIGNUP = "auth/signup";
        public static string URL_COUNTRIES = "country";
        public static string URL_EVENTS = "projectevent";
        public static string URL_PROJECTS = "project";
        public static string URL_REPLY = "complainreply";
        public static string CLOUDINARY_URL = "https://api.cloudinary.com/v1_1/huufvofso/image/upload";
    }
}