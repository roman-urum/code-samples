using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalsService.DomainLogic.Tests.Exceptions
{
    public class AddDocumentDbItemException:Exception
    {
        public AddDocumentDbItemException(): base() { }
        public AddDocumentDbItemException(string message): base(message) { }
        public AddDocumentDbItemException(string message, Exception innerException): base(message, innerException) { }
    }
}
