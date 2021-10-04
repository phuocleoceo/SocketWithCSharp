using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URI
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://xuanthulab.net/lap-trinh/csharp/?page=3#acff";
            Uri uri = new Uri(url);
            
            Type uritype = typeof(Uri);
            uritype.GetProperties().ToList().ForEach(property => {
                Console.WriteLine($"{property.Name,15} {property.GetValue(uri)}");
            });
            Console.WriteLine($"Segments: {string.Join(",", uri.Segments)}");

            Console.ReadKey();
        }
    }
}
