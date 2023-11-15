using JsonConfigLibrary.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestyXamarin.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }
        private void LoadSettings()
        {
            if (Application.Current.Properties.ContainsKey("DownloadLink"))
            {
                DownloadLink_editor.Text = Application.Current.Properties["DownloadLink"] as String;
            }
            if (Application.Current.Properties.ContainsKey("userToken"))
            {
                UserToken_editor.Text = Application.Current.Properties["userToken"] as String;
            }
        }

        private async void Save_button_Clicked(object sender, EventArgs e)
        {
            Save_button.IsEnabled = false;

            string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;

            string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(DownloadLink_editor.Text, "db.json");

            if (DownloadPath != null)
            {
                try
                {
                    JSONConfigGlobal.jsonConfig = JsonConvert.DeserializeObject<JSONConfig>(File.ReadAllText(DownloadPath));
                    //safe json to app Properties
                    Application.Current.Properties["fileExists"] = true;
                    Application.Current.Properties["userToken"] = UserToken_editor.Text;
                    JSONConfigGlobal.userToken = UserToken_editor.Text;
                    //
                    Application.Current.Properties["DownloadLink"] = DownloadLink_editor.Text;
                    JSONConfigGlobal.DownloadLink = DownloadLink_editor.Text;
                    Application.Current.SavePropertiesAsync();
                    await Navigation.PopAsync();

                }
                catch(Exception)
                {
                    await DisplayAlert("Błąd!", "Plik jest uszkodzony", "OK");
                }
            }
            else
            {
                await DisplayAlert("Błąd!", "Link jest nieprawidłowy, czy użyto linku z https?", "OK");
            }
            
        }
    }
}