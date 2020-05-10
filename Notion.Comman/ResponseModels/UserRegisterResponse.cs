using System;
using System.Collections.Generic;

namespace Notion.Comman.ResponseModels
{
    public class UserRegisterResponse
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Error { get; set; }
        public string Token { get; set; }
        public Dictionary<string, string> UserInfo { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}