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
    public int UtilizationFactor { get; set; }
    public string ResponsiblePersonEmail { get; set; }

    public WashingMachineConfiguration(WashingMachineConfigurationDTO dto) 
    {
        _id = ObjectId.GenerateNewId().ToString();
        PositionX = dto.PositionX;
        PositionY = dto.PositionY;
        Manufacturer = dto.Manufacturer;
        StartDate = dto.StartDate;
        ExpirationDate = dto.ExpirationDate;
        UtilizationFactor = 0;
        ResponsiblePersonEmail = dto.ResponsiblePersonEmail;
    }

    public void UpdateUtilizationFactor(int spinRate, int temperature) {
        UtilizationFactor += spinRate * temperature / 1000;
    }
}