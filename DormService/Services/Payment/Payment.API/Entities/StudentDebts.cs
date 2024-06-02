using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Payment.API.Entities
{
    public class StudentDebts
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string studentID { get; set; }

        // All debt types
        public decimal credit {  get; set; }
        public decimal rent {  get; set; }
        public decimal internet {  get; set; }
        public decimal airConditioning {  get; set; }
        public decimal phone {  get; set; }
        public decimal cleaning { get; set; }

        public StudentDebts(string studentID, decimal credit, decimal rent, decimal internet, decimal airConditioning, decimal phone, decimal cleaning)
        {
            _id = ObjectId.GenerateNewId().ToString();
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
            _id = ObjectId.GenerateNewId().ToString();
            this.studentID = studentID ?? throw new ArgumentNullException(nameof(studentID));
            this.credit = 0;
            this.rent = 0;
            this.internet = 0;
            this.airConditioning = 0;
            this.phone = 0;
            this.cleaning = 0;
        }

    }
}
