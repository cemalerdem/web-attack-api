using Notion.Comman.Enums;
using System;

namespace Notion.Comman.Dtos
{
    public class RequestDto
    {
        public DateTime CreatedAtUTC { get; set; }
        public string MethodType { get; set; }
        public string Path { get; set; }
        public string StatusCode { get; set; }
        public string QueryParameter { get; set; }
        public string RequestPayload { get; set; }
        public string CreatedBy { get; set; }
    }
}