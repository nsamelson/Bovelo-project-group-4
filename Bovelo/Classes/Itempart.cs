﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    internal class ItemPart
    {
        public int quantity;
        public BikePart part;
        public int price;
        public ItemPart(BikePart part, int quantity)
        {
            this.quantity = quantity;
            this.part = part;
            this.price = getPrice();
        }

        public void increment()
        {
            quantity++;
            price = getPrice();
        }
        public void decrement()
        {
            quantity--;
            price = getPrice();
        }
        public int getPrice()
        {
            return quantity * this.price;
        }
    }
}