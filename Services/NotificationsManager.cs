using Plugin.LocalNotification;

namespace Namiokai.Services;

public class NotificationsManager
{
    public NotificationsManager()
    {

    }

    public void Init()
    {
        var request = new NotificationRequest
        {
            NotificationId = 1337,
            Title = "Subscribe to my channel",
            Subtitle = "Hello",
            Description = "It's me",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(5),
                NotifyRepeatInterval = TimeSpan.FromDays(1),

            },
            Image = new NotificationImage { ResourceName = "mantelis.png" },
            CategoryType = NotificationCategoryType.Reminder
        };


        LocalNotificationCenter.Current.Show(request);
    }
}


