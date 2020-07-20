using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BUS
{
    public class JsonUtility
    {
        public List<T> GetObjectFromJson<T>(string jasonString)
        {
            List<T> listLaptopInCart = JsonConvert.DeserializeObject<List<T>>(jasonString);
            return listLaptopInCart;
        }
        public string SetObjectAsJson<T>(List<T> listObject)
        {
            string jasonString = JsonConvert.SerializeObject(listObject);
            return jasonString;
        }
    }
}
