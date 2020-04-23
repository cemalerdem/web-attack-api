namespace Notion.Comman.ResponseModels
{
    public class UserLoginResponse
    {
        public int Id {get;set;}
        public string Email {get;set;}
        public string Message {get;set;}
        public bool IsSuccess {get;set;}
        public string Token {get;set;}
    }
}