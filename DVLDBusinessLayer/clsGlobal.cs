namespace DVLDBusinessLayer
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null; 
        }
    }
}
