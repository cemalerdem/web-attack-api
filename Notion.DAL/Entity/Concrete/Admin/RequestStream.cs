using Notion.DAL.Entity.Abstract;
using System;

namespace Notion.DAL.Entity.Concrete.Admin
{
    public class RequestStream : BaseEntity<Guid> {
        
        public string MethodType { get; set; }
        public string Path { get; set; }
        public string StatusCode { get; set; }
        public string QueryParameter { get; set; }
        public string RequestPayload { get; set; }
        public string Result { get; set; }

    }
}