using System.Collections.Generic;
using Demo.Business.Notifications;

namespace Demo.Business.Contracts
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
