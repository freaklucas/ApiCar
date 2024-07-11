namespace ApiCar.Notifications;

public abstract class Notifiable
{
    private readonly List<Notification> _notifications;

    protected Notifiable()
    {
        _notifications = new();
    }

    public IReadOnlyCollection<Notification> Notifications => _notifications;

    protected void AddNotification(string property, string message)
    {
        _notifications.Add(new Notification(property, message));
    }

    protected void AddNotification(Notification notification) { 
        _notifications.Add(notification);
    }

    protected void AddNotifications(IEnumerable<Notification> notifications)
    {
        _notifications?.AddRange(notifications);
    }

    public bool IsValid => !_notifications.Any();
}