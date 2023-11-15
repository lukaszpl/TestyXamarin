using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace JsonConfigLibrary.Logic
{
    public class JSONConfig
    {
        public JSONConfig(string organisationName, string organisationInformations, string logo_Base64, string examScriptAddres, ObservableCollection<ExamItem> examItems)
        {
            OrganisationName = organisationName;
            OrganisationInformations = organisationInformations;
            Logo_Base64 = logo_Base64;
            ExamScriptAddres = examScriptAddres;
            this.examItems = examItems;
        }

        public string OrganisationName { get; set; }
        public string OrganisationInformations { get; set; }
        public string Logo_Base64 { get; set; }
        public string ExamScriptAddres { get; set; }
        public ObservableCollection<ExamItem> examItems { get; set; }
    }
}
