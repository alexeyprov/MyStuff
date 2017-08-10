using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFlowTest
{
    public class ShippingDetail
    {
        public int ShipId
        {
            get;
            set;
        }

        public IEnumerable<ShippingItem> Items
        {
            get;
            set;
        }
    }
}
