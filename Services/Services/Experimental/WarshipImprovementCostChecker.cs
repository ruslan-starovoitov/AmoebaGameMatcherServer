﻿using System;
using NetworkLibrary.Http.Lobby;
using NetworkLibrary.NetworkLibrary.Http;

namespace Services.Services.Experimental
{
    public enum FaultReason
    {
        InsufficientSoftCurrency,
        InsufficientWarshipPowerPoints,
        MaximumLevelAlreadyReached
    }
    public class WarshipImprovementCostChecker
    {
        public bool CanAPurchaseBeMade(int softCurrency, int warshipPowerLevel,  int warshipPowerPoints,
            out FaultReason? faultReason)
        {
            // Console.WriteLine($"{nameof(warshipPowerLevel)} {warshipPowerLevel} {nameof(warshipPowerPoints)} {warshipPowerPoints}");
            //Достать цену улучшения
            WarshipImprovementModel improvementModel = WarshipPowerScale.GetModel(warshipPowerLevel);

            if (improvementModel == null)
            {
                Console.WriteLine("У корабля уже максимальный уровень");
                faultReason = FaultReason.MaximumLevelAlreadyReached;
                return false;
            }

            if (softCurrency < improvementModel.SoftCurrencyCost)
            {
                faultReason = FaultReason.InsufficientSoftCurrency;
                return false;
            }
            
            if (warshipPowerPoints < improvementModel.PowerPointsCost)
            {
                faultReason = FaultReason.InsufficientWarshipPowerPoints;
                return false;
            }

            faultReason = null;
            return true;
        }


        public WarshipImprovementModel GetImprovementModel(int warshipPowerLevel)
        {
            WarshipImprovementModel improvementModel = WarshipPowerScale.GetModel(warshipPowerLevel);
            return improvementModel;
        }
    }
}