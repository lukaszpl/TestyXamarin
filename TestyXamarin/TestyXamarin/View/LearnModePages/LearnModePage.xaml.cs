using JsonConfigLibrary.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestyXamarin.View.LearnModePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestyXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnModePage : ContentPage
    {
        public LearnModePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            ExamList_listView.ItemsSource = JSONConfigGlobal.jsonConfig.examItems;
            base.OnAppearing();
        }

        private void Start_button_Clicked(object sender, EventArgs e)
        {
            if (ExamList_listView.SelectedItem != null)
            {
                ExamItem selectedExam =(ExamItem)ExamList_listView.SelectedItem;
                Navigation.PushAsync(new LearnModeQuestionsPage(selectedExam));
            }
        }
    }
}