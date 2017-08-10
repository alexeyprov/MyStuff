using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFlowTest
{
    public class OrderItem
    {
        public string Sku
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

        public decimal UnitPrice
        {
            get;
            set;
        }
    }
}
