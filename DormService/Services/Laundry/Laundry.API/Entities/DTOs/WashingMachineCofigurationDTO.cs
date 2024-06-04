namespace Laundry.API.Entities;

public class WashingMachineConfigurationDTO
{

    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Manufacturer { get; set; }
    public string StartDate { get; set; }
    public string ExpirationDate { get; set; }
    public string ResponsiblePersonEmail { get; set; }
}