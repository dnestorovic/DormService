using Laundry.API.Entities;

namespace Laundry.API.Data;

public class LaundryDayGenerator
{

    public static IEnumerable<WashingMachine> CreateNewDefaultDay(string date, IEnumerable<WashingMachineConfiguration> availableMachines) 
    {
        List<WashingMachine> machines = new List<WashingMachine>();
        foreach (TimeSlot timeSlot in Enum.GetValues(typeof(TimeSlot)))
        {
            foreach (WashingMachineConfiguration wm in availableMachines)
            {
                machines.Add(new WashingMachine(wm._id, date, timeSlot.ToString()));
            }   
        }

        return machines;
    }


}