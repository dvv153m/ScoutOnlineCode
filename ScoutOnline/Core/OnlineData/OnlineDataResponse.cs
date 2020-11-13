using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.OnlineData
{
    public class OnlineDataResponse
    {

        public UnitModel Unit { get; set; }

        public OnlineDataInfo OnlineData { get; set; }

        public override string ToString()
        {
            return Unit + " " + OnlineData;
        }

        public string GetStringToSearch()
        {
            //Поиск осуществлять только по: Название, Гос. номер, ID, Водитель
            return $"{Unit.Name} {Unit.StateNumber} {Unit.TerminalNumber} {OnlineData.DriverName}";
        }
    }

    public class UnitModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateNumber { get; set; }
        public string GarageNumber { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int? TypeId { get; set; }
        public string DeviceName { get; set; }
        public string Brand { get; set; }
        public string Vin { get; set; }
        public string Registration { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Owner { get; set; }
        public int CompanyId { get; set; }
        public string Olson { get; set; }
        public string OlsonDescription { get; set; }
        public string Power { get; set; }
        public string TerminalNumber { get; set; }
        public string TerminalType { get; set; }

        public override string ToString()
        {
            return $"{Name} {StateNumber} {GarageNumber} {Description} {Model} {DeviceName} {Brand} {Vin} {Registration} {Year} {Color} {Owner} {Olson} {OlsonDescription} {Power} {TerminalNumber} {Id}";
        }
    }
}
