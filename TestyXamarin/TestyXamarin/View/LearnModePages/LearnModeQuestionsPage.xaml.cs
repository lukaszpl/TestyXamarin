using JsonConfigLibrary.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View.LearnModePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnModeQuestionsPage : ContentPage
    {
        int numberOfQuestionsForExam = 0;
        //
        int actualPosition = 0;
        private ExamItem examItem; //exam object, json readonly
        ObservableCollection<QuestionItemHistory> listOfQuestions = new ObservableCollection<QuestionItemHistory>(); //local copy for presentation and save answers

        public LearnModeQuestionsPage(ExamItem examItem)
        {
            this.examItem = examItem;
            InitializeComponent();
            SetMaxQuestionsToShow();
            this.Title = examItem.ExamName;
            LoadQuestions();
            //load first question
            UpdateLayout();
        }

        private void SetMaxQuestionsToShow()
        {
            if(examItem.Questions.Count <= 40)
            {
                numberOfQuestionsForExam = examItem.Questions.Count - 1;
            }
            else
            {
                numberOfQuestionsForExam = 40;
            }
        }

        private void LoadQuestions()
        {
            for(int i = 0; i <= numberOfQuestionsForExam; i++)
            {
                int question_id = GetRandomUniqueNumber();
                QuestionItem question = examItem.Questions[question_id];
                listOfQuestions.Add(new QuestionItemHistory(question.Question, question.Base64Img, question.Answers));
            }
        }

        private async void Next_button_Clicked(object sender, EventArgs e)
        {
            if (actualPosition < numberOfQuestionsForExam)
            {             
                actualPosition++;
                UpdateLayout();
                Debug.WriteLine(actualPosition);
                if (actualPosition == numberOfQuestionsForExam)
                    Next_button.Text = "Zakończ";
                else
                    Next_button.Text = "Dalej";
            }
            else
            {            
                await Navigation.PushAsync(new LearnModeResultsPage(listOfQuestions));
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

        private void UpdateLayout()
        {
            Question_label.Text = listOfQuestions[actualPosition].Question;
            //
            if ((listOfQuestions[actualPosition].Base64Img != null) && (listOfQuestions[actualPosition].Base64Img != ""))
                Question_image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(listOfQuestions[actualPosition].Base64Img)));
            else
                Question_image.Source = null;
            //
            Answers_listView.ItemsSource = listOfQuestions[actualPosition].Answers;
        }

        protected override void OnAppearing()
        {        
            base.OnAppearing();
        }
        
        List<int> listOfUsedId = new List<int>();
        private int GetRandomUniqueNumber()
        {
            Random r = new Random();
            int i = r.Next(examItem.Questions.Count());
            foreach (var item in listOfUsedId) { 
                if(item == i)
                    return GetRandomUniqueNumber();
            }
            listOfUsedId.Add(i);
            return i;
        }
    }



    //rozszerzenie obiektów o zapamiętywanie odpowiedzi użytkownika
    public class QuestionItemHistory : QuestionItem
    {
        
        [JsonIgnore] //ignorowanie atrybutu przy serializacji - jest zbędny
        public ImageSource QuestionImageSource
        {
            get { return Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Base64Img))); }
            set { }
        }
        //
        public new ObservableCollection<AnswerUserItem> Answers
        {
            get; set;
        } 
        public QuestionItemHistory(string question, string base64Img, ObservableCollection<AnswerItem> answers) : base(question, base64Img, answers)
        {
            this.Answers = new ObservableCollection<AnswerUserItem>();
            foreach(var answer in base.Answers)
            {
                this.Answers.Add(new AnswerUserItem(answer));
            }
        }
    }
    //
    public class AnswerUserItem : AnswerItem
    {
        [JsonIgnore]
        public Color AnswerColor { get; set; }
        //
        public bool userAnswer { get; set; }
        public AnswerUserItem(AnswerItem answerItem) : base (answerItem.Answer, answerItem.IsCorrect)
        {

        }
    }
}