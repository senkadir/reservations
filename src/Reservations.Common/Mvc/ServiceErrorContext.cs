using System.Collections.Generic;

namespace Reservations.Common.Mvc
{
    public class ServiceErrorContext
    {
        public string Code { get; set; }

        public List<ServiceError> Errors { get; set; }
    }

    public class ServiceError
    {
        public string Exception { get; set; }

        public string ErrorMessage { get; set; }
    }
}
