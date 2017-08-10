using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFlowTest
{
    public class Order
    {
        public int OrderId
        {
            get;
            set;
        }

        public string Carrier
        {
            get;
            set;
        }

        public IEnumerable<OrderItem> Items
        {
            get;
            set;
        }
    }
}
