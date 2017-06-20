using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurosport.Kodakademi.Refactor
{
    class SimpleRecipes
    {
        #region 1. Explaining Variable

        #region Base

        public void DoStuff(string platform)
        {
            bool wasResized = true;
            if (platform.IndexOf("MAC",StringComparison.OrdinalIgnoreCase) > -1
                    && platform.IndexOf("IE", StringComparison.OrdinalIgnoreCase) > -1
                    && wasResized && wasInitialized())
            {
                //do stuff
            }
        }

        #endregion

        #region Refactored

        public void DoStuffRefactored(string platform)
        {
            bool wasResized = true;
            bool isMacOS = platform.IndexOf("MAC", StringComparison.OrdinalIgnoreCase) > -1;
            bool isIEBrowser = platform.IndexOf("IE", StringComparison.OrdinalIgnoreCase) > -1;

            if ( isMacOS && isIEBrowser && wasResized && wasInitialized())
            {
                //do stuff
            }
        }

        #endregion

        #region Helpers

        private bool wasInitialized()
        {
            return false;
        }

        #endregion

        #endregion

        #region 2. EV + Extract Method

        #region Base

        public int Quantity { get; set; }
        public double ItemPrice { get; set; }

        public double GetPrice()
        {
            //price is base price - quantity discount (over 500 items, items costs 5% less) + shipping (100€ or 10% of total cost)
            return Quantity * ItemPrice - Math.Max(0, Quantity - 500) * ItemPrice * 0.05 + Math.Min(Quantity * ItemPrice * 0.1, 100);
        }

        #endregion

        #region Refactored

        public double BasePrice { get { return Quantity * ItemPrice; } }
        /// <summary>
        /// Get the discount earned by buying Quantity
        /// </summary>
        /// <remarks>
        /// Over 500 items, items costs 5% less.
        /// </remarks>
        public double QuantityPriceDiscount { get { return Math.Max(0, Quantity - 500) * ItemPrice * 0.05; } }
        public double ShippingPrice {  get { return Math.Min(BasePrice * 0.1, 100); } }

        public double GetPriceRefactored()
        {
            return BasePrice - QuantityPriceDiscount + ShippingPrice;
        }

        #endregion

        #endregion

        #region Remove Assignments to parameters

        #region Base

        public int GetPrice(int totalPrice,int quantity, bool isFirstCustomer)
        {
            if (totalPrice > 50) totalPrice -= 2;
            if (quantity > 10) totalPrice -= 1;
            if (isFirstCustomer) totalPrice -= 4;
            return totalPrice;
        }

        #endregion

        #region Refactored

        public int GetPriceRefactored(int totalPrice,int quantity, bool isFirstCustomer)
        {
            int res = totalPrice;
            if (totalPrice > 50) res -= 2;
            if (quantity > 10) res -= 1;
            if (isFirstCustomer) res -= 4;
            return res;
        }

        #endregion


        #endregion
    }
}
