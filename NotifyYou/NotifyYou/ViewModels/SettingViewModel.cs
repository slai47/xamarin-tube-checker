using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace NotifyYou.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public ICommand Github { get; private set; }

        public SettingViewModel()
        {
            Github = new Command((obj) => Device.OpenUri(new Uri("https://github.com/slai47/xamarin-tube-checker")));
        }
    }
}
