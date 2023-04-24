using System;
namespace BTM
{
    public class Rep0
    {
        public class Line : ILine
        {
            private string _numberHex;
            private int numberDec;
            private List<Stop> stops;
            private List<Vehicle> vehicles;
            private string commonName;

            public Line(string numberHex, int numberDec, string commonName, List<Stop> stops = null, List<Vehicle> vehicles = null)
            {
                this._numberHex = numberHex;
                this.numberDec = numberDec;
                if (this.stops == null) stops = new List<Stop>();
                else
                {
                    this.stops = stops;
                }
                this.commonName = commonName;

                this.vehicles = vehicles;
            }

            public ILine Clone()
            {
                return new Line(this.NumberHex, this.numberDec, this.commonName, this.stops, this.vehicles);
            }
            public int NumberDec { get => numberDec; set => numberDec = value; }
            public List<IVehicle> Vehicles { get => new List<IVehicle>(vehicles); }// set => vehicles = value; }
            public List<Vehicle> setVehicles { set => vehicles = value; }
            public List<Stop> setStops { set => stops = value; }
            public List<IStop> Stops { get => new List<IStop>(stops); }
            public string NumberHex { get => _numberHex; set => _numberHex = value; }
            public string CommonName { get => commonName; set => commonName = value; }

            public override string ToString()
            {
                string s = $"{_numberHex}, {numberDec}, {commonName}, Stops: [";
                foreach (Stop stop in stops)
                {
                    s += $" {stop.Id}";
                }
                s += $" ],  Vehicles: [ ";
                foreach (Vehicle veh in vehicles)
                {
                    s += $" {veh.Id}";
                }
                s += " ]";
                return s;
            }

        }

        public class Stop : IStop
        {
            private string name;
            private int id;
            private string type;
            private List<Line> lines;

            public string Type { get => type; set => type = value; }
            public string Name { get => name; set => name = value; }
            public int Id { get => id; set => id = value; }
            public List<ILine> Lines { get => new List<ILine>(lines); }
            public List<Line> setLines { set => lines = value; }


            public Stop(int id, string name, string type, List<Line> lines = null)
            {
                this.name = name;
                this.id = id;
                this.type = type;
                this.lines = lines;
            }

            public override string ToString()
            {
                string s = $"{name},{id}, type = {type}, lines: [";
                foreach (Line line in lines)
                {
                    s += $" {line.NumberDec}";
                }
                s += $"]";
                return s;
            }


        }

        public abstract class Vehicle : IVehicle
        {
            private int id;
            private List<Driver> drivers;
            public int Id { get => id; set => id = value; }
            
            public Vehicle(int id)
            {
                this.id = id;
                drivers = new List<Driver>();
            }
            public override string ToString()
            {
                return $"id = {id}";
            }

            public void AddDriver(Driver d)
            {
                drivers.Add(d);
            }

            public List<IDriver> Drivers { get => new List<IDriver>(drivers); }
        }

        public class Bytebus : Vehicle, IBytebus
        {
            private string engineClass;
            private List<Line> lines;

            public Bytebus(int id, List<Line> lines, string engineClass) : base(id)
            {
                this.engineClass = engineClass;
                this.lines = lines;
            }
            public string EngineClass { get => engineClass; set => engineClass = value; }
            public List<ILine> Lines { get => new List<ILine>(lines); }
            public List<Line> setLines { set => lines = value; }

            public override string ToString()
            {
                string s = base.ToString();
                s += $"EngineClass: {engineClass}, lines: [";
                foreach (Line line in lines)
                {
                    s += $" {line.NumberDec}";
                }
                s += $"]";
                return s;
            }

        }

        public class Tram : Vehicle, ITram
        {
            private int carsNumber;
            private Line line;

            public int CarsNumber { get => carsNumber; set => carsNumber = value; }

            public ILine Line { get => line; }
            public Line setLine { set => line = value; }

            public Tram(int id, int carsNumber, Line line) : base(id)
            {
                this.carsNumber = carsNumber;
                this.line = line;
            }

            public override string ToString()
            {
                string s = base.ToString();
                s += $" carsNumber: {carsNumber}, Line: {line.NumberDec}";
                return s;
            }
        }

        public class Driver : IDriver
        {
            private List<Vehicle> vehicles;
            private string name;
            private string surname;
            private int seniority;

            public Driver(string name, string surname, int seniority, List<Vehicle> vehicles)
            {
                this.vehicles = vehicles;
                this.name = name;
                this.surname = surname;
                this.seniority = seniority;
            }

            public List<Vehicle> setVehicles { set => vehicles = value; }
            public string Name { get => name; set => name = value; }
            public string Surname { get => surname; set => surname = value; }
            public int Seniority { get => seniority; set => seniority = value; }

            public List<Vehicle> GetAdaptVehicles { get => vehicles; }

            public List<IVehicle> Vehicles { get => new List<IVehicle>(vehicles); }

            public override string ToString()
            {
                string s = $"{name} {surname}, {seniority}, Vehicles: [";
                foreach (Vehicle v in vehicles)
                {
                    s += $" {v.Id}";
                }
                s += " ]";
                return s;
            }
        }
    }
}

