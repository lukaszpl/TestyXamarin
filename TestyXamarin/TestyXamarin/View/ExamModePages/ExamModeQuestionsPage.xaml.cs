using JsonConfigLibrary.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View.ExamModePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExamModeQuestionsPage : ContentPage
    {
        //
        string userToken;
        int actualPosition = 0;
        private ExamModeExamItem examItem; //exam object, json readonly
        ObservableCollection<ExamModeQuestionItemHistory> listOfQuestions = new ObservableCollection<ExamModeQuestionItemHistory>(); //local copy for presentation and save answers

        public ExamModeQuestionsPage(ExamModeExamItem examItem, string userToken)
        {
            InitializeComponent();
            this.examItem = examItem;
            this.Title = examItem.ExamName;
            this.userToken = userToken;
            LoadQuestions();
            //load first question
            UpdateLayout();
        }

        
        private void LoadQuestions()
        {
            for (int i = 0; i <= examItem.Questions.Count - 1; i++)
            {
                ExamModeQuestionItem question = examItem.Questions[i];
                listOfQuestions.Add(new ExamModeQuestionItemHistory(question.Id, question.Question, question.Base64Img, question.Answers));
            }
        }

        private void UpdateLayout()
        {
            this.Title = examItem.ExamName + " " + (actualPosition+1) + "/" + (examItem.Questions.Count);
            Question_label.Text = listOfQuestions[actualPosition].Question;
            //
            if ((listOfQuestions[actualPosition].Base64Img != null) && (listOfQuestions[actualPosition].Base64Img != ""))
                Question_image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(listOfQuestions[actualPosition].Base64Img)));
            else
                Question_image.Source = null;
            //
            Answers_listView.ItemsSource = listOfQuestions[actualPosition].Answers;
        }

        //
        private async void Next_button_Clicked(object sender, EventArgs e)
        {
            if (actualPosition < examItem.Questions.Count - 1)
            {
                actualPosition++;
                UpdateLayout();
                //Debug.WriteLine(actualPosition);
                if (actualPosition == examItem.Questions.Count - 1)
                    Next_button.Text = "Zakończ";
                else
                    Next_button.Text = "Dalej";
            }
            else
            {
                await Navigation.PushAsync(new ExamModeResultsPage(listOfQuestions, examItem.Id, userToken));
                Navigation.RemovePage(this);
            }
            //

        }
        private void Back_button_Clicked(object sender, EventArgs e)
        {
            if (actualPosition > 0)
            {
                actualPosition--;
                UpdateLayout();
                Next_button.Text = "Dalej";
            }
        }
    }

    public class ExamModeQuestionItemHistory : ExamModeQuestionItem
    {
        [JsonIgnore] //ignorowanie atrybutu przy serializacji - jest zbędny
        public ImageSource QuestionImageSource
        {
            get { return Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Base64Img))); }
            set { }
        }
        public ExamModeQuestionItemHistory(int id, string question, string base64Img, ObservableCollection<ExamModeAnswerItem> answers) : base(id, question, base64Img, answers){}
    }
}