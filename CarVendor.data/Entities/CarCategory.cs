using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string MaximumTorque { get; set;}
        public string EnginePower { get; set;}
        public int Acceleration { get; set;}
        public string TractionType { get; set;}
        public int SeatsNo { get; set;}
        public int DoorsNo { get; set;}
        public int AvgFuelConsumption { get; set;}
        public int FuelTankCapacity { get; set;}
        public int GroundClearance { get; set;}
        public int MaxSpeed{ get; set;}
        public string FuelRecommended { get; set;}
        public bool DriverAirbags { get; set;}
        public bool FrontPassengerAirbags { get; set;}
        public bool ElectricChairs { get; set;}
        public bool BrakeSystemABS { get; set;}

        public virtual Category Category { get; set; }
        public virtual ICollection<CarColor> CarColors { get; set; }
    }
}
