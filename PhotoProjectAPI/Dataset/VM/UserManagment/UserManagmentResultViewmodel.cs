namespace PhotoProjectAPI.Dataset.VM.UserManagment
{
    public class UserManagmentResultViewmodel
    {
        public string Token { get; set; }
        public string TokenRefreshment { get; set; }

        public DateTime ExpiringDate { get; set; }
    }
}
