using System;
using System.Collections.Generic;
using System.Text;

namespace FleetMgmt.Identity.Domain.Dto
{
    public class ServiceResponse
    {
        public object Data { get; set; }
        public string Msg { get; set; }
        public bool Success { get; set; }
        public List<ErrorMessage> ErrorList { get; set; }
    }

    public class ErrorMessage
    {
        public string Error { get; set; }
        public string Value { get; set; }
    }
}
