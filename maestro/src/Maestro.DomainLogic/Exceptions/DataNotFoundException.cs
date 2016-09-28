using System;
using Maestro.Common.Extensions;

namespace Maestro.DomainLogic.Exceptions
{
    /// <summary>
    /// Raise this exception when service need info of entity 
    /// which not exists in database.
    /// </summary>
    public class DataNotFoundException : Exception
    {
        private const string ExceptionMessageTemplate = "{0} with id {1} not exists.";

        public DataNotFoundException(string message)
            : base(message)
        {
        }

        public DataNotFoundException(string entityName, Guid id)
            : base(ExceptionMessageTemplate.FormatWith(entityName, id))
        {
        }
    }
}
