
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Documentation.API.Entities
{
    public class DocumentationList
    {

        [BsonId]
        public string studentID;
        public Document? ApplicationForm { get; set; }
        public Document? AvgGradeCertificate { get; set; }

        public Document? IncomeCertificate { get; set; }


        public Document? UnemploymentCertificate { get;set; }

        public Document? FacultyDataForm { get; set; }

        public Document? FirstTimeStudentCertificate { get; set; }


        public Document? HighSchoolFirstYearCertificate { get; set ; }


        public Document? HighSchoolSecondYearCertificate { get; set; }


        public Document? HighSchoolThirdYearCertificate { get; set; }


        public Document? HighSchoolFourthYearCertificate { get; set; }

        public DocumentationList(string studentID)
        {
            this.studentID = studentID;
            this.ApplicationForm = null;
            this.IncomeCertificate = null;
            this.UnemploymentCertificate = null;
            this.FacultyDataForm = null;
            this.FirstTimeStudentCertificate = null;
            this.HighSchoolFirstYearCertificate = null;
            this.HighSchoolSecondYearCertificate = null;
            this.HighSchoolThirdYearCertificate = null;
            this.HighSchoolFourthYearCertificate = null;

        }


        public bool checkStudentId(string studentID)
        {
            return studentID == this.studentID;
        }

        internal object checkStudentID(string studentID)
        {
            return studentID == this.studentID;
        }
    }
}
