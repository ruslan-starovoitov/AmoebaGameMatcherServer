﻿// using System;
// using System.Threading.Tasks;
// using DataLayer;
// using DataLayer.Tables;
// using Microsoft.EntityFrameworkCore;
// using NetworkLibrary.NetworkLibrary.Http;
//
// namespace AmoebaGameMatcherServer.Services.Shop.ShopModel.ShopModelCreation
// {
//     public class PrizeFactoryService
//     {
//         private readonly ApplicationDbContext dbContext;
//
//         public PrizeFactoryService(ApplicationDbContext dbContext)
//         {
//             this.dbContext = dbContext;
//         }
//
//         public async Task<ProductModel> CreatePrizeProduct(int accountId)
//         {
//             bool isPlayerRecentlyPickedUpAGift = await IsPlayerRecentlyPickedUpAGift(accountId);
//             return new ProductModel
//             {
//                 Id = 1,
//                 TransactionType = TransactionTypeEnum.SoftCurrency,
//                 CurrencyTypeEnum = CostTypeEnum.Free,
//                 ImagePreviewPath = "coins5",
//                 ShopItemSize = ProductSizeEnum.Small,
//                 Name = "15",
//                 Disabled = isPlayerRecentlyPickedUpAGift
//             };
//         }
//         
//         private async Task<bool> IsPlayerRecentlyPickedUpAGift(int accountId)
//         {
//             DateTime oneDayAgo = DateTime.UtcNow - TimeSpan.FromDays(1);
//             return await dbContext.Transactions
//                 .Include(transaction => transaction.Account)
//                 .AnyAsync(transaction => transaction.AccountId== accountId 
//                                          && transaction.DateTime>oneDayAgo
//                                          && transaction.TransactionTypeId==TransactionTypeEnum.DailyPrize);
//         }
//     }
// }