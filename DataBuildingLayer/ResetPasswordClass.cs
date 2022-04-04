

namespace DataBuildingLayer
{
    public class ResetPasswordClass
    {
        public int Uid { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public class PassErrorMessages
    {
        public string FullNameMessage { get; set; }
        public string UserNameMessage { get; set; }
        public string PasswordMessage { get; set; }
        public string ConfPasswordMessage { get; set; }

    }

}
