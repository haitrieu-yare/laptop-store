using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BUS
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        { 
            session.SetObjectAsJson(key, value);
        }

        public static List<T> GetObjectFromJson<T>(this ISession session, string key)
        {
            List<T> value = session.GetObjectFromJson<T>(key);
            return value;
        }
    }
}
