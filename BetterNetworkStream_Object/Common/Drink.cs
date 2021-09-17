using System;
using System.Collections.Generic;

namespace Common
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime ExpiryDay { get; set; }

        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Price}\t{ExpiryDay}";            
        }

        public string Serialization_Text()
        {
            return $"Id = {Id} & Name = {Name} & Price = {Price} & ExpiryDay = {ExpiryDay.Ticks}";
        }

        public void Deserialization_Text(string data)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string[] pairs = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                string[] p = pair.Split('=');
                if (p.Length == 2) // Key : Value
                {
                    var key = p[0].Trim();
                    var value = p[1].Trim();
                    dict[key] = value;
                }
            }
            if (dict.ContainsKey("Id"))
            {
                this.Id = int.Parse(dict["Id"]);
            }

            if (dict.ContainsKey("Name"))
            {
                this.Name = dict["Name"];
            }

            if (dict.ContainsKey("Price"))
            {
                this.Price = Convert.ToDouble(dict["Price"]);
            }

            if (dict.ContainsKey("ExpiryDay"))
            {
                this.ExpiryDay = new DateTime(long.Parse(dict["ExpiryDay"]));
            }
        }
    }
}
