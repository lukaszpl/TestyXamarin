using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JsonConfigLibrary.Logic
{
    public class ExamItem
    {
        public string ExamName { get; set; }
        public string Information;
        public int Version;
        public ObservableCollection<QuestionItem> Questions;

        public ExamItem(string examName, string information, int version, ObservableCollection<QuestionItem> questions)
        {
            ExamName = examName;
            Information = information;
            Version = version;
            Questions = questions;
        }
    }

    public class ExamModeExamItem
    {
        public int Id;
        [JsonIgnore]
        public string ExamName { get; set; }
        [JsonIgnore]
        public string Information;
        public ObservableCollection<ExamModeQuestionItem> Questions;

        public ExamModeExamItem(int id, string examName, string information, ObservableCollection<ExamModeQuestionItem> questions)
        {
            Id = id;
            ExamName = examName;
            Information = information;
            Questions = questions;
        }
    }
}
