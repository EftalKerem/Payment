namespace Case.Entity.Domains.Enums;

public static class Enums
{
    public enum UserStatus
    {
        Active,
        Passive
    }
    public enum PaymentTransactionStatus
    {
        Success,
        Error,
        Waiting,
        Canceled
    }

    public enum PaymentTransactionType
    {
        Sale,
        Void,
        Refund
    }
}