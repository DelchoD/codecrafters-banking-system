namespace BankingManagementSystem.Core.Constants
{
    public static class ValidationConstants
    {
        //Customer
        public const int CustomerFirstNameMinLength = 2;
        public const int CustomerFirstNameMaxLength = 50;
        public const int CustomerMiddleNameMinLength = 2;
        public const int CustomerMiddleNameMaxLength = 50;
        public const int CustomerLastNameMinLength = 2;
        public const int CustomerLastNameMaxLength = 50;
        public const int CustomerIdNumberMinLength = 10;
        public const int CustomerIdNumberMaxLength = 10;
        public const int CustomerAddressMinLength = 10;
        public const int CustomerAddressMaxLength = 300;
        public const int CustomerPhoneMaxLength = 10;
        public const int CustomerPhoneMinLength = 3;
        public const int CustomerPasswordMinLength = 6;






        //Account
        public const int AccountIBANMinLength = 5;
        public const int AccountIBANMaxLength = 34;
        public const int AccoutNameMinLength = 4;
        public const int AccountNameMaxLength = 120;


        //Transaction
        public const int TransactionReasonMinLength = 5;
        public const int TransactionReasonMaxLength = 500;
    }
}
