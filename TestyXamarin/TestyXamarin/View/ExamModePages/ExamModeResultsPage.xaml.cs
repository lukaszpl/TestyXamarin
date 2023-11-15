using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestyXamarin.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View.ExamModePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamModeResultsPage : ContentPage
    {
        ObservableCollection<ExamModeQuestionItemHistory> listOfQuestions;
        int ExamId;
        string userToken;
        public ExamModeResultsPage(ObservableCollection<ExamModeQuestionItemHistory> listOfQuestions, int ExamId, string userToken)
        {
            InitializeComponent();
            this.listOfQuestions = listOfQuestions;
            this.ExamId = ExamId;
            this.userToken = userToken;
            SendResult();
        }

        private async void SendResult()
        {

            var values = new Dictionary<string, string>
            {
                { "POSToperation", "SENDEXAM" },
                { "POSTtoken", userToken },
                { "POSTExamId", ExamId.ToString() },
                { "POSTdata", JsonConvert.SerializeObject(listOfQuestions, Formatting.None) }
            };
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(JSONConfigGlobal.jsonConfig.ExamScriptAddres, content);
            string responseString = await response.Content.ReadAsStringAsync();
            text_Label.Text = responseString;
     
        }
    }
}