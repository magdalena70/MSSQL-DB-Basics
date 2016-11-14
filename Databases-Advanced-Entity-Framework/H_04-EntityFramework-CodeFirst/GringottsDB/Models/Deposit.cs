﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GringottsDB.Models
{
    [ComplexType]
    public class Deposit
    {
        [MaxLength(20)]
        public string Group { get; set; }

        public DateTime StartDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Interest { get; set; }

        public decimal Charge { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsExpired { get; set; }
    }
}
