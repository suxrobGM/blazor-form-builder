namespace Radzen;

public static class NotificationServiceExtensions
{
    public static void NotifySuccess(this NotificationService notificationService, string? message)
    {
        notificationService.Notify(NotificationSeverity.Success, "Success", message);
    }
    
    public static void NotifyError(this NotificationService notificationService, string? message)
    {
        notificationService.Notify(NotificationSeverity.Error, "Error", message);
    }
}
