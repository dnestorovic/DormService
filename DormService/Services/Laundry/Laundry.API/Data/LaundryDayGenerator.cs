using Laundry.API.Entities;

namespace Laundry.API.Data;

public class LaundryDayGenerator
{

    public static IEnumerable<WashingMachine> CreateNewDefaultDay(string date) 
    {
        // TODO: Should be replaced with fetching from other source 
        IEnumerable<WashingMachine> machines = [
        new WashingMachine(date, TimeSlot.Morning.ToString()),
        new WashingMachine(date, TimeSlot.Morning.ToString()),
        new WashingMachine(date, TimeSlot.Morning.ToString()),
        new WashingMachine(date, TimeSlot.Noon.ToString()),
        new WashingMachine(date, TimeSlot.Noon.ToString()),
        new WashingMachine(date, TimeSlot.Noon.ToString()),
        new WashingMachine(date, TimeSlot.Afternoon.ToString()),
        new WashingMachine(date, TimeSlot.Afternoon.ToString()),
        new WashingMachine(date, TimeSlot.Afternoon.ToString())
    ];
        return machines;
    }


}