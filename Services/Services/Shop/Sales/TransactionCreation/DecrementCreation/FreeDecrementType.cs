using DataLayer.Entities.Transactions.Decrement;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.Shop.Sales.TransactionCreation.DecrementCreation
{
    public class FreeDecrementType:IDecrementFactory
    {
        public CostTypeEnum GetCurrencyTypeEnum()
        {
            return CostTypeEnum.Free;
        }

        public Decrement Create(ProductModel productModel)
        {
            return null;
        }
    }
}