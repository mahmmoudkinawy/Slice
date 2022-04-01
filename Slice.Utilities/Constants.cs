namespace Slice.Utilities;
public class Constants
{
    //Roles in our app
    public const string ManagerRole = "Manager";
    public const string FrontDeskRole = "Front";
    public const string KitchenRole = "Kitchen";
    public const string CustomerRole = "Customer";


    //Payment status
    public const string StatusPending = "PendingPayment";
    public const string StatusSubmitted = "SubmittedPaymentApproved";
    public const string StatusRejected = "RejectedPayment";

    public const string StatusInProcess = "Being Prepared";
    public const string StatusReady = "Ready for Pickup";
    public const string StatusCompleted = "Completed";
    public const string StatusCancelled = "Cancelled";
    public const string StatusRefunded = "Refunded";

    public const string SessionCart = "SessionCart";
}
