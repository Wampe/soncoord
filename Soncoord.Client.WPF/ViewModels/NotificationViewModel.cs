using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Soncoord.Infrastructure.Events;
using System.Collections.ObjectModel;

namespace Soncoord.Client.WPF.ViewModels
{
    public class NotificationViewModel : BindableBase
    {
        public NotificationViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<NotificationEvent>().Subscribe(ShowNotification);
            Close = new DelegateCommand<string>(OnCloseExecute);

            Notifications = new ObservableCollection<string>();
        }
        
        public DelegateCommand<string> Close { get; set; }
        public ObservableCollection<string> Notifications { get; set; }

        private void OnCloseExecute(string item)
        {
            Notifications.Remove(item);
        }

        private void ShowNotification(string notificationText)
        {
            Notifications.Add(notificationText);
        }
    }
}
