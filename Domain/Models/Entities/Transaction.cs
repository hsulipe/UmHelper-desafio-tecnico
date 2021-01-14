using System;

namespace Domain.Models.Entities
{
    public class Transaction : EntityBase
    {
        public Guid From { get; private set; }
        public Guid To { get; private set; }
        public double Value { get; private set; }

        // Navigation Properties
        public virtual UserAccount UserAccountFrom { get; private set; }
        public virtual UserAccount UserAccountTo { get; private set; }

        protected Transaction() : base() { }

        public Transaction(Guid from, Guid to, double value) : base()
        {
            From = from;
            To = to;
            Value = value;
            if(Value <= 0) throw new ArgumentException();
        }
    }
}
