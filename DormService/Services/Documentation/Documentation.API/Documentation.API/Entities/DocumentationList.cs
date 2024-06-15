
namespace Documentation.API.Entities
{
    public class DocumentationList
    {
      
        public string StudentID;

        public DocumentationList(string studentID)
        {
            StudentID = studentID;
        }

        public Document? ApplicationForm { get; set; }   
        public Document? AvgGradeCertificate { get; set;}

        public Document? IncomeCertificate { get; set; }

        public Document? UnemploymentCertificate { get;set; }

        public Document? FacultyDataForm { get; set; }

        public Document? FirstTimeStudentCertificate { get; set; }

        public Document? HighSchoolFirstYearCertificate { get; set ; }

        public Document? HighSchoolSecondYearCertificate { get; set; }

        public Document? HighSchoolThirdYearCertificate { get; set; }

        public Document? HighSchoolFourthYearCertificate { get; set; }

        public bool checkStudentId(string studentID)
        {
            return studentID == StudentID;
        }

        internal object checkStudentID(string studentID)
        {
            return studentID == StudentID;
        }
    }
}
