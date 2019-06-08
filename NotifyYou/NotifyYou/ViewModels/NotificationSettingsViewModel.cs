using System;
using NotifyYou.Models;

namespace NotifyYou.ViewModels
{
    public class NotificationSettingsViewModel : BaseViewModel
    {
        public NotificationSetting Setting { get; set; }

        public bool IsActive { 
        get {
                return Setting.Active;
        }
        set {
                Setting.Active = value;
                OnPropertyChanged(nameof(IsActive));
                SaveSettings();
         }
        }

        public bool SoundOn
        {
            get
            {
                return Setting.Sound;
            }
            set
            {
                Setting.Sound = value;
                OnPropertyChanged(nameof(SoundOn));
                SaveSettings();
            }
        }

        public bool VibrateOn
        {
            get
            {
                return Setting.Vibrate;
            }
            set
            {
                Setting.Vibrate = value;
                OnPropertyChanged(nameof(VibrateOn));
                SaveSettings();
            }
        }

        public NotificationSettingsViewModel(NotificationSetting setting)
        {
            Setting = setting;
        }

        private void SaveSettings()
        {
            App.ChannelsDatastore.AddUpdate(Setting);
        }
    }
}
