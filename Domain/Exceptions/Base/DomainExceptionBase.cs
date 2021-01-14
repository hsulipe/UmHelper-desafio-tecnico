using System;

namespace Domain.Exceptions.Base
{
    public abstract class DomainExceptionBase : Exception
    {
        protected DomainExceptionBase() : base()
        {

        }

        protected DomainExceptionBase(string message) : base(message)
        {
            
        }

        protected DomainExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}
