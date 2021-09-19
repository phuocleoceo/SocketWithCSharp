using System;

namespace Common
{
    [Serializable]
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
    }
}
