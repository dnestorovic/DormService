namespace Laundry.API.Entities;

public class WashingMachineReservationDTO
{
    public string Id { get; set; }
    public string ConfigurationId { get; set;}
    public string StudentId {get; set;}
    public string EmailAddress {get; set;}
    public int SpinRate { get; set; }
    public int WashingTemperature { get; set; }  
    public int Price {get; set;}

}