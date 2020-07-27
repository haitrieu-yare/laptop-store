using System.Collections.Generic;
using Newtonsoft.Json;

namespace BUS
{
    public class JsonUtility
    {
        public List<T> GetObjectFromJson<T>(string jsonString)
        {
            List<T> listLaptopInCart = JsonConvert.DeserializeObject<List<T>>(jsonString);
            return listLaptopInCart;
        }
        public string SetObjectAsJson<T>(List<T> listObject)
        {
            string jsonString = JsonConvert.SerializeObject(listObject);
            return jsonString;
        }
    }
}
