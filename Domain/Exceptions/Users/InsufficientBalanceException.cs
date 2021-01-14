using System;
using System.Collections.Generic;
using System.Text;
using Domain.Exceptions.Base;

namespace Domain.Exceptions.Users
{
    public class InsufficientBalanceException : DomainExceptionBase
    {
        public InsufficientBalanceException() : base()
        {
            
        }
    }
}
