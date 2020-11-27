namespace FastServices.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "FastServices";

        public const string AdministratorRoleName = "Administrator";

        public const string EmployeeRoleName = "Employee";

        // Notification Messages
        public const string SuccessCommentPostMessage = "Success! Thank you for your feedback";

        public const string SuccessOrderSubmitted = "Success! Submitted your order";

        public const string SuccessComplaintSubmitted = "Success! You have submitted your complaint";

        public const string DeletedCommentPostMessage = "Your comment has been deleted";

        public const string ErrorCommentPostSpamMessage = "Error! You have to wait 24h before posting another comment";

        public const string ErrorOrderSubmitOneOrderAtATime = " You can have only 1 active order at a time";

        public const string ErrorOrderNotEnoughAvailableEmployees = "There are currently no available employees for this date. Try again with different date";

        public const string ErrorEmployeeSubmitOrder = "Employees cannot submit orders!";

        // Order Constants
        public const int HourlyFeePerWorker = 5;
    }
}
