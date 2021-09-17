using System;
using System.Collections.Generic;

namespace Common
{
    public static class TextSerializer
    {
        public static string Drink_Serialization(this Drink d)
        {
            return $"Id = {d.Id} & Name = {d.Name} & Price = {d.Price} & ExpiryDay = {d.ExpiryDay.Ticks}";
        }

        public static Drink Drink_Deserialization(this string data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] pairs = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pair in pairs)
            {
                string[] p = pair.Split('=');
                if (p.Length == 2) // Key : Value
                {
                    string key = p[0].Trim();
                    string value = p[1].Trim();
                    dict[key] = value;
                }
            }
            Drink drink = new Drink();
            if (dict.ContainsKey("Id"))
            {
                drink.Id = Convert.ToInt32(dict["Id"]);
            }

            if (dict.ContainsKey("Name"))
            {
                drink.Name = dict["Name"];
            }

            if (dict.ContainsKey("Price"))
            {
                drink.Price = Convert.ToDouble(dict["Price"]);
            }

            if (dict.ContainsKey("ExpiryDay"))
            {
                drink.ExpiryDay = new DateTime(long.Parse(dict["ExpiryDay"]));
            }
            return drink;
        }
    }
}
