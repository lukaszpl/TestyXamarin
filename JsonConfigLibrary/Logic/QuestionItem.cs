using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JsonConfigLibrary.Logic
{
    public class QuestionItem
    {
        public QuestionItem(string question, string base64Img, ObservableCollection<AnswerItem> answers)
        {
            Question = question;
            Answers = answers;
            Base64Img = base64Img;
        }

        public string Question { get; set; }
        public string Base64Img { get; set; }
        public ObservableCollection<AnswerItem> Answers { get; set; }
    }

    public class ExamModeQuestionItem
    {
        public ExamModeQuestionItem(int id, string question, string base64Img, ObservableCollection<ExamModeAnswerItem> answers)
        {
            Id = id;
            Question = question;
            Answers = answers;
            Base64Img = base64Img;
        }
        public int Id;
        [JsonIgnore]
        public string Question { get; set; }
        [JsonIgnore]
        public string Base64Img { get; set; }
        public ObservableCollection<ExamModeAnswerItem> Answers { get; set; }
    }
}
