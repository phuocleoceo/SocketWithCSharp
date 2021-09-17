using System;
using System.Collections.Generic;

namespace Common
{
    public static class TextSerializer
    {
        // Chuyển đổi một object sang chuỗi ký tự. 
        // Chuỗi kết quả có hình thức tương tự chuỗi tham số của Http get.
        public static string Serialize(this Drink obj)
        {
            return 
                $"Id = {obj.Id} & Name = {obj.Name} & Price = {obj.Price} & ExpiryDay = {obj.ExpiryDay.Ticks}"; //ToString() cho Drink
        }

        // Chuyển đổi một chuỗi trở lại thành object
        public static Drink Deserialize(this string data)
        {
            var dict = new Dictionary<string, string>();
            var pairs = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {   
                var p = pair.Split('='); // cắt mỗi phần tử lấy mốc là ký tự =
                if (p.Length == 2) // một cặp khóa = giá_trị đúng sau khi cắt sẽ phải có 2 phần
                {
                    var key = p[0].Trim(); // phần tử thứ nhất là khóa
                    var value = p[1].Trim(); // phần tử thứ hai là giá trị
                    dict[key] = value; // lưu cặp khóa-giá trị này lại sử dụng phép toán indexing                    
                }
            }
            var obj = new Drink();
            if (dict.ContainsKey("Id"))
            {
                obj.Id = int.Parse(dict["Id"]);
            }

            if (dict.ContainsKey("Name"))
            {
                obj.Name = dict["Name"];
            }

            if (dict.ContainsKey("Price"))
            {
                obj.Price = Convert.ToDouble(dict["Price"]);
            }

            if (dict.ContainsKey("ExpiryDay"))
            {
                obj.ExpiryDay = new DateTime(long.Parse(dict["ExpiryDay"]));
            }

            return obj;
        }
    }
}