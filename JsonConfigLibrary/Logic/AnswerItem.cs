using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonConfigLibrary.Logic
{
    public class AnswerItem
    {
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerItem(string answer, bool isCorrect)
        {
            Answer = answer;
            IsCorrect = isCorrect;
        }
    }

    public class ExamModeAnswerItem
    {
        public int Id;
        [JsonIgnore]
        public string Answer { get; set; }
        public bool UserAnswer { get; set; }

        public ExamModeAnswerItem(int id, string answer, bool userAnswer)
        {
            Id = id;
            Answer = answer;
            UserAnswer = userAnswer;
        }
    }
}
