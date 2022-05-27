﻿namespace StockTrader.Domain.Models
{
    public class Account : DomainObject
    {
        public int Id { get; set; }
        public User AccountHolder { get; set; }
        public double Balance { get; set; }

        public ICollection<AssetTransaction> AssetTransactions { get; set; }
    }
}