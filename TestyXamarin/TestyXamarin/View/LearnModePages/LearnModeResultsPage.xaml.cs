using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View.LearnModePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnModeResultsPage : ContentPage
    {
        private ObservableCollection<QuestionItemHistory> listOfQuestions;
        public LearnModeResultsPage(ObservableCollection<QuestionItemHistory> listOfQuestions)
        {
            InitializeComponent();
            this.listOfQuestions = listOfQuestions;
            //
            int allpoints = getAllPossiblePoints();
            int userpoints = getUserPoints();
            double percentResult = ((double)userpoints / (double)allpoints) * 100;

            Result_label.Text = "Twój wynik to: " + userpoints + "/" + allpoints + " " + percentResult + "%";
            Results_listView.ItemsSource = this.listOfQuestions;
        }

        //calculate result
        private int getUserPoints()
        {
            int points = 0;
            foreach(var question in listOfQuestions)
            {
                int questionPoints = 0;
                foreach(var answer in question.Answers)
                {
                    if ((answer.IsCorrect) && (answer.userAnswer))
                    {
                        questionPoints++;
                    }
                    else if ((!answer.IsCorrect) && (answer.userAnswer))
                    {
                        questionPoints = 0;
                        break;
                    }
                }
                points += questionPoints;
            }
            return points;
        }

        private int getAllPossiblePoints()
        {
            int points = 0;
            foreach (var question in listOfQuestions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.IsCorrect)
                    {
                        points++;
                        answer.AnswerColor = Color.Green;
                    }
                }
            }
            return points;
        }
    }

}