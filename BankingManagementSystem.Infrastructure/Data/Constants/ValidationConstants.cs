namespace BankingManagementSystem.Infrastructure.Data.Constants
{
    public static class ValidationConstants
    {
        //Customer
        public const int CustomerFirstNameMaxLength = 50;
        public const int CustomerMiddleNameMaxLength = 50;
        public const int CustomerLastNameMaxLength = 50;
        public const int CustomerIdNumberMaxLength = 10;
        public const int CustomerAddressMaxLength = 300;
        public const int CustomerPasswordMinLength = 6;


        //Account
        public const int AccountIBANMaxLength = 34;
        public const int AccountNameMaxLength = 120;


        //Transaction
        public const int TransactionReasonMaxLength = 500;
    }
}
