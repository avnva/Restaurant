﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant;

public class Warehouse
{
    public int WarehouseID { get; set; }
    public int ProductID { get; set; }
    public float StockBalance { get; set; }
    public int SupplierID { get; set; }
}
