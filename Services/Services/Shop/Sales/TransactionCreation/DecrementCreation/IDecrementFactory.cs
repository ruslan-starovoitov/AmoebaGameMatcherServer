using DataLayer.Entities.Transactions.Decrement;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public interface IDecrementFactory
    {
        CostTypeEnum GetCurrencyTypeEnum();
        Decrement Create(ProductModel productModel);
    }
}