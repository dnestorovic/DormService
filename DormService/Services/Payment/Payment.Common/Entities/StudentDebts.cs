using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Payment.Common.Entities
{
    public class StudentDebts
    {
        [BsonId]
        public string studentID { get; set; }

        public decimal credit {  get; set; }

        // All debt types
        public decimal rent {  get; set; }
        public decimal internet {  get; set; }
        public decimal airConditioning {  get; set; }
        public decimal phone {  get; set; }
        public decimal cleaning { get; set; }

        public StudentDebts(string studentID, decimal credit, decimal rent, decimal internet, decimal airConditioning, decimal phone, decimal cleaning)
        {
            this.studentID = studentID ?? throw new ArgumentNullException(nameof(studentID));
            this.credit = credit;
            this.rent = rent;
            this.internet = internet;
            this.airConditioning = airConditioning;
            this.phone = phone;
            this.cleaning = cleaning;
        }

        public StudentDebts(string studentID)
        {
            this.studentID = studentID ?? throw new ArgumentNullException(nameof(studentID));
            this.credit = 0;
            this.rent = 0;
            this.internet = 0;
            this.airConditioning = 0;
            this.phone = 0;
            this.cleaning = 0;
        }

        public StudentDebts(string studentID, decimal credit)
        {
            this.studentID = studentID ?? throw new ArgumentNullException(nameof(studentID));
            this.credit = credit;
        }

        public StudentDebts() { }
    }
}
