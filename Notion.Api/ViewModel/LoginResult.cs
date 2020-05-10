using System;
using System.Collections.Generic;

namespace Notion.Api.ViewModel
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public Dictionary<string, string> UserInfo { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

   
}