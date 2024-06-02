using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Payment.API.Entities
{
    public class StudentDebts
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string studentID { get; set; }

        Dictionary<string, float> allDebts = new Dictionary<string, float>();



        public StudentDebts(string studentID, Dictionary<string, float> allDebts)
        {
            this.studentID = studentID ?? throw new ArgumentNullException(nameof(studentID));
            this.allDebts = allDebts ?? throw new ArgumentNullException(nameof(allDebts));
        }

        public StudentDebts(string studentID)
        {
            this.studentID = studentID;
            foreach (string type in DebtType.types)
            {
                allDebts[type] = 0;
            }
        }


        public Dictionary<string, float> getAllDebts(string studentID)
        {
            return allDebts;
        }

        public Dictionary<string, float> getBasicDebts(string studentID)
        {
            Dictionary<string, float> basicDebts = allDebts;
            basicDebts.Remove(DebtType.CREDIT);

            return basicDebts;
        }

        public float getCreditStatus(string studentID)
        {
            return allDebts[DebtType.CREDIT];
        }

        public bool reduceCredit(float amount)
        {
            if (allDebts[DebtType.CREDIT] >= amount)
            {
                allDebts[DebtType.CREDIT] = allDebts[DebtType.CREDIT] - amount;
                return true;
            }
            return false;
        }
    }
}
