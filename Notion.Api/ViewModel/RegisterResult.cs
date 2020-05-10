using System;
using System.Collections.Generic;

namespace Notion.Api.ViewModel
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Dictionary<string, string> UserInfo { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}   