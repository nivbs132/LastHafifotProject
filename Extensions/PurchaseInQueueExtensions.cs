﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Extensions
{
    public static class PurchaseInQueueExtensions
    {
        public static ExpandPurchase GetExpandedPurchase(this PurchaseInQueue purchaseInQueue)
        {
            ExpandPurchase expandPurchase = new ExpandPurchase();

            expandPurchase.ActivityDays = StoreIdDictionary.OpeningDays[purchaseInQueue.StoreID.ActivityDays];
            expandPurchase.CreditCard = purchaseInQueue.CreditCardNumber;
            expandPurchase.InsertionDate = DateTime.Now;
            int installments;
            if (purchaseInQueue.Installments == "FULL")
            {
                expandPurchase.Installments = 1;
            }
            else if(int.TryParse(purchaseInQueue.Installments, out installments))
            {
                expandPurchase.Installments = installments;
            }
            else
            {
                expandPurchase.Installments = -1;
            }
            expandPurchase.IsValid = true;
            expandPurchase.TotalPrice = purchaseInQueue.Price;
            expandPurchase.PricePerInstallments = expandPurchase.TotalPrice / expandPurchase.Installments;
            expandPurchase.PurchaseDate = purchaseInQueue.PurchaseDate;
            expandPurchase.PurchaseId = "";
            expandPurchase.StoreId = purchaseInQueue.StoreID.StoreIdNumbers.ToString();
            expandPurchase.StoreType = StoreIdDictionary.Types[purchaseInQueue.StoreID.StoreType];
            expandPurchase.WhyInvalid = "";

            return expandPurchase;
        }
    }
}