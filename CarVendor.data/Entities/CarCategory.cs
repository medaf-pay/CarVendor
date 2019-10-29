using System.Collections.Generic;

namespace CarVendor.data.Entities
{
    public class CarCategory : TEntity<long>
    {
        public long CarId { get; set; }
        public virtual Car Car { get; set; }
        public long CategoryId { get; set; }
        public int EngineCapacity { get; set; }
        public int TransfersNo { get; set; }
        public int CylindersNo { get; set; }
        public int CastingsNo { get; set; }
        public bool ElectronicFuelInjection { get; set; }
        public string MaximumTorque { get; set; }
        public string EnginePower { get; set; }
        public int Acceleration { get; set; }
        public string TractionType { get; set; }
        public int SeatsNo { get; set; }
        public int DoorsNo { get; set; }
        public int AvgFuelConsumption { get; set; }
        public int FuelTankCapacity { get; set; }
        public int GroundClearance { get; set; }
        public int MaxSpeed { get; set; }
        public string FuelRecommended { get; set; }
        public bool DriverAirbags { get; set; }
        public bool FrontPassengerAirbags { get; set; }
        public bool ElectricChairs { get; set; }
        public bool BrakeSystemABS { get; set; }
        public bool ElectronicBrakeDistribution { get; set; }
        public bool ElectronicBalanceProgram { get; set; }
        public bool AntitheftAlarmSystem { get; set; }
        public bool ImmobilizerSystemAgainstTheft { get; set; }
        public bool SportRims { get; set; }
        public int RimSize { get; set; }
        public bool FrontFogLanterns { get; set; }
        public bool BackFogLanterns { get; set; }
        public bool BackCleaners { get; set; }
        public bool ElectricSideMirrors { get; set; }
        public bool ElectricallyFoldingSideMirrors { get; set; }
        public bool SideMirrorsSignals { get; set; }
        public bool XenonBulbsLighting { get; set; }
        public bool HeadlampWipers { get; set; }
        public bool SensitiveHeadlamps { get; set; }
        public bool HeadlampControl { get; set; }
        public bool HeadlampLightingLED { get; set; }
        public bool TaillightsLightingLED { get; set; }
        public bool BackSpoiler { get; set; }
        public bool IntelligentParkingSystem { get; set; }
        public bool SoundSystem { get; set; }
        public bool CDDriver { get; set; }
        public bool AUXPort { get; set; }
        public bool USBPort { get; set; }
        public bool Bluetooth { get; set; }
        public bool FrontHeadrests { get; set; }
        public bool RearHeadrests { get; set; }
        public bool ElectricWindshield { get; set; }
        public bool ElectricRearGlass { get; set; }
        public bool OneTouchGlassControl { get; set; }
        public bool RemoteControlToLockAndOpenDoors { get; set; }
        public bool DriverHeightControl { get; set; }
        public bool LeatherBrushes { get; set; }
        public bool EngineStartStopButtonSystem { get; set; }
        public bool Sunroof { get; set; }
        public bool ElectricSunroof { get; set; }
        public bool BackCamera { get; set; }
        public bool ComputerTrips { get; set; }
        public bool SteeringWheelType { get; set; }
        public bool ControllableSteeringWheel { get; set; }
        public bool ControlTheSoundSystemOfTheSteeringWheel { get; set; }
        public bool CruiseControl { get; set; }
        public bool LeatherSteeringWheel { get; set; }
        public bool LeatherTransmission { get; set; }
        public bool FrontDoorStorage { get; set; }
        public bool BackDoorStorageAreas { get; set; }
        public bool PossibilityToFoldBackSeats { get; set; }
        public bool Lighter { get; set; }
        public bool MobileAshtray { get; set; }
        public bool CentralDoorLock { get; set; }
        public bool AlarmSoundWhenTheCarIsNotClosed { get; set; }
        public bool FrontCupHolder { get; set; }
        public bool BackCupHolder { get; set; }
        public bool FrontArmrest { get; set; }
        public bool AirConditionedFrontArmrest { get; set; }
        public bool BackArmrest { get; set; }
        public bool BackTrunkCover { get; set; }
        public bool FrontStorageDrawer { get; set; }
        public bool PowerOutlet { get; set; }
        public bool BackOutletPowerOutlet { get; set; }
        public bool BackWipers { get; set; }
        public bool RainSensitiveWindshieldWipers { get; set; }
        public bool BackLight { get; set; }
        public bool SensitiveHeadlampsForLighting { get; set; }
        public bool BackTrunkSpace { get; set; }
        public bool BackSeatBelt { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CarColor> CarColors { get; set; }
    }
}
