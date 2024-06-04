using MongoDB.Bson;
namespace Laundry.API.Entities;

public class WashingMachineConfiguration
{

    public string _id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Manufacturer { get; set; }
    public string StartDate { get; set; }
    public string ExpirationDate { get; set; }
    public int NumberOfCycles { get; set; }
    public int UtilizationFactor { get; }
    public string ResponsiblePersonEmail { get; set; }

    public WashingMachineConfiguration(WashingMachineConfigurationDTO dto) 
    {
        _id = ObjectId.GenerateNewId().ToString();
        PositionX = dto.PositionX;
        PositionY = dto.PositionY;
        Manufacturer = dto.Manufacturer;
        StartDate = dto.StartDate;
        ExpirationDate = dto.ExpirationDate;
        ExpirationDate = dto.ExpirationDate;
        NumberOfCycles = 0;
        UtilizationFactor = 0;
        ResponsiblePersonEmail = dto.ResponsiblePersonEmail;
    }

    private int CalculateUtilizationFactor() {
        // TODO: should be implemented once spinRate and washingTemperature were added
        return 0;
    }
}