using JsonConfigLibrary.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestyXamarin.Interfaces;
using TestyXamarin.View;
using TestyXamarin.View.ExamModePages;
using Xamarin.Forms;

namespace TestyXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadFileIfExists();
        }

        private async void LoadFileIfExists()
        {
            //load userToken to memory
            if(Application.Current.Properties.ContainsKey("userToken"))
                JSONConfigGlobal.userToken = Application.Current.Properties["userToken"] as String;
            //
            string path = DependencyService.Get<ICrossPlatformDownloadManager>().DefaultPathToDownloadedFiles;
            string finalPath = Path.Combine(path, "db.json");

            if (finalPath != null)
            {
                try
                {
                    if (Application.Current.Properties.ContainsKey("fileExists"))
                    {
                        JSONConfigGlobal.jsonConfig = JsonConvert.DeserializeObject<JSONConfig>(File.ReadAllText(finalPath));
                    }
                }
                catch (Exception)
                {
                    await DisplayAlert("Błąd!", "Plik jest uszkodzony", "OK");
                }
            }
            else
            {
                await DisplayAlert("Błąd!", "Plik bazy nie istnieje, przejdź do ustawień i pobierz go ponownie", "OK");
            }

        }

        protected override void OnAppearing()
        {
            if(JSONConfigGlobal.jsonConfig != null)
            {
                State_label.Text = "Status aplikacji: gotowa";
                MainImage_Image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(JSONConfigGlobal.jsonConfig.Logo_Base64)));
                MainText_label.Text = JSONConfigGlobal.jsonConfig.OrganisationName;
                Info_label.Text = JSONConfigGlobal.jsonConfig.OrganisationInformations;
            }
            else
            {
                State_label.Text = "Status aplikacji: niegotowa";
            }
            base.OnAppearing();
        }

        private void ToolbarSettingsItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage());
        }

        private void ToolbarAboutItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }

        private async void StartLearn_button_Clicked(object sender, EventArgs e)
        {
            StartLearn_button.IsEnabled = false;
            if (JSONConfigGlobal.jsonConfig != null)
            {
                StartLearn_button.IsEnabled = true;
                Navigation.PushAsync(new LearnModePage());
            }
            else {
                await DisplayAlert("Informacja", "Aplikacja nie jest gotowa, w celu uruchomienia testu przejdź do ekranu ustawień i wskaż link do bazy pytań", "OK");
            }
            StartLearn_button.IsEnabled = true;
        }

        private async void StartExam_button_Clicked(object sender, EventArgs e)
        {
            if ((JSONConfigGlobal.userToken != null) && (JSONConfigGlobal.userToken != ""))
            {
                StartExam_button.IsEnabled = false;
                string DownloadPath = await DependencyService.Get<ICrossPlatformDownloadManager>().DownloadFileAsync(JSONConfigGlobal.jsonConfig.ExamScriptAddres + "?operation=GETEXAM&token=" + JSONConfigGlobal.userToken, "examTemp");
                if (DownloadPath != null)
                {
                    try
                    {
                        ExamModeExamItem examModeExamItem = JsonConvert.DeserializeObject<ExamModeExamItem>(File.ReadAllText(DownloadPath));
                        StartExam_button.IsEnabled = true;
                        Navigation.PushAsync(new ExamModeQuestionsPage(examModeExamItem, JSONConfigGlobal.userToken));
                    }
                    catch (JsonSerializationException exception)
                    {
                        await DisplayAlert("Błąd", File.ReadAllText(DownloadPath), "OK");
                    }
                    catch (Exception ee)
                    {
                        await DisplayAlert("Błąd", ee.ToString(), "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Błąd!", "Link jest nieprawidłowy", "OK");
                }
                StartExam_button.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Informacja", "Przejdź do ustawień i podaj UserToken!", "OK");
            }
        }
    }
}
