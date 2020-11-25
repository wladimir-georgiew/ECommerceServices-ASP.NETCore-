namespace FastServices.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "FastServices";

        public const string AdministratorRoleName = "Administrator";

        public const string EmployeeRoleName = "Employee";

        // Notification Messages
        public const string SuccessCommentPostMessage = "Success! Thank you for your feedback!";

        public const string SuccessOrderSubmitted = "Success! Submitted your order!";

        public const string DeletedCommentPostMessage = "Success! Your comment has been deleted";

        public const string ErrorCommentPostSpamMessage = "Error! You have to wait 24h before posting another comment";

        // Order Constants
        public const int HourlyFeePerWorker = 5;
    }
}
