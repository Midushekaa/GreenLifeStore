using System;

namespace GreenLifeStore.Sub_class
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public override string ToString() => StatusName;
    }
}