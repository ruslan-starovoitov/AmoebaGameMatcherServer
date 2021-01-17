﻿using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.Shop.Sales.TransactionCreation.IncrementCreation
{
    public interface IIncrementsFactory
    {
        List<Increment> Create(ProductModel productModel);
        ResourceTypeEnum GetResourceTypeEnum();
    }
}