using System;
using System.Xml.Linq;

namespace BTM
{
    public class Rep6
    {
        public class Line
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();

            public int id;

            public Line(Dictionary<string, string> map, int id)
            {
                Map = map;
                this.id = id;
            }
        }

        public class Stop
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();

            public int id;

            public Stop(Dictionary<string, string> map, int id)
            {
                Map = map;
                this.id = id;
            }
        }

        public abstract class Vehicle
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();
            public int id;
            public int Id { get => id; set => id = value; }
            public Vehicle(int id, Dictionary<string, string> map)
            {
                this.id = id;
                Map = map;
            }
        }

        public class Bytebus : Vehicle
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();

            public Bytebus(int id, Dictionary<string, string> map) : base(id,null)
            {
                Map = map;
            }
        }

        public class Tram : Vehicle
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();

            public Tram(int id, Dictionary<string, string> map) : base(id,null)
            {
                Map = map;
            }
        }

        public class Driver
        {
            public Dictionary<string, string> Map = new Dictionary<string, string>();
            public int id;
            static int c_id = 0;
            public Driver(Dictionary<string, string> map)
            {
                Map = map;
                this.id = c_id++;
            }
        }
    }
    public class Adapt_rep6_Line : ILine
    {
        public Rep6.Line line;
        public static Dictionary<int, Adapt_rep6_Line> linesMap = new Dictionary<int, Adapt_rep6_Line>();

        public Adapt_rep6_Line(string numberHex, int numberDec, string commonName, List<Adapt_rep6_Stop> stops = null, List<IVehicle> vehicles = null)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("numberHex", numberHex);
            map.Add("commonName", commonName);
            if (stops != null)
            {
                int len = stops.Count;
                map.Add("stops.Size()", len.ToString());
                for (int i = 0; i < len; i++)
                {
                    map.Add($"stops[{i}]", stops[i].Id.ToString());
                }
            }
            if (vehicles != null)
            {
                int len = vehicles.Count;
                map.Add("vehicles.Size()", len.ToString());
                for (int i = 0; i < len; i++)
                {
                    map.Add($"vehicles[{i}]", vehicles[i].Id.ToString());
                }
            }
            this.line = new Rep6.Line(map, numberDec);
            linesMap[numberDec] = this;
        }
        public int NumberDec
        {
            get => line.id;
            set => line.id = value;
        }


        public string NumberHex
        {
            get => line.Map["numberHex"];
            set => line.Map.Add("numberHex", value);
        }

        public string CommonName
        {
            get => line.Map["commonName"];
            set => line.Map.Add("commonName", value);
        }
        public List<IStop> Stops
        {
            get
            {
                int len = int.Parse(line.Map["stops.Size()"]);
                List<IStop> stops = new List<IStop>();
                for (int i = 0; i < len; i++)
                {
                    stops.Add((IStop)Adapt_rep6_Stop.stopsMap[int.Parse(line.Map[$"stops[{i}]"])]);
                }
                return stops;

            }
        }

        public List<Adapt_rep6_Stop> SetStops
        {
            set
            {
                line.Map.Add("stops.Size()", value.Count.ToString());
                for (int i = 0; i < value.Count; i++)
                {
                    line.Map.Add($"stops[{i}]", value[i].Id.ToString());
                }
            }
        }

        public List<IVehicle> Vehicles
        {
            get
            {
                int len = int.Parse(line.Map["vehicles.Size()"]);
                List<IVehicle> vehicles = new List<IVehicle>();
                for (int i = 0; i < len; i++)
                {
                    vehicles.Add((IVehicle)Adapt_rep6_Vehicle.vehiclesMap[int.Parse(line.Map[$"vehicles[{i}]"])]);
                }
                return vehicles;
            }
        }
        public List<Adapt_rep6_Vehicle> setVehicles
        {
            set
            {
                line.Map.Add("vehicles.Size()", value.Count.ToString());
                for (int i = 0; i < value.Count; i++)
                {
                    line.Map.Add($"vehicles[{i}]", value[i].Id.ToString());
                }
            }
        }
        public override string ToString()
        {
            string s = $"{this.NumberHex}, {this.NumberDec}, {this.CommonName}";
            return s;
        }
    }

    public class Adapt_rep6_Stop : IStop
    {
        public Rep6.Stop stop;
        public static Dictionary<int, Adapt_rep6_Stop> stopsMap = new Dictionary<int, Adapt_rep6_Stop>();

        public Adapt_rep6_Stop(int id, string name, string type, List<Adapt_rep6_Line> lines = null)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("name", name);
            map.Add("type", type);
            if (lines != null)
            {
                int n = lines.Count;
                map.Add("lines.Size()", n.ToString());

                for (int i = 0; i < n; i++)
                {
                    map.Add($"lines[{i}]", lines[i].NumberDec.ToString());
                }
            }
            this.stop = new Rep6.Stop(map, id);
            stopsMap[id] = this;
        }

        public int Id
        {
            get => stop.id;
            set => stop.id = value;
        }

        public string Name
        {
            get => stop.Map["name"];
            set => stop.Map.Add("name", value);
        }

        public string Type
        {
            get => stop.Map["type"];
            set => stop.Map["type"] = value;
        }
        public List<ILine> Lines
        {
            get
            {
                int len = int.Parse(stop.Map["lines.Size()"]);
                List<ILine> lines = new List<ILine>();
                for (int i = 0; i < len; i++)
                {
                    lines.Add(Adapt_rep6_Line.linesMap[int.Parse(stop.Map[$"lines[{i}]"])] as ILine);
                }
                return lines;
            }
            set
            {
                stop.Map.Add("lines.Size()", value.Count.ToString());
                for (int i = 0; i < value.Count; i++)
                {
                    stop.Map.Add($"stops[{i}]", value[i].NumberDec.ToString());
                }
            }
        }

        public override string ToString()
        {
            string s = $"{Name}, {Id}, type = {Type}";
            return s;
        }
    }

    public abstract class Adapt_rep6_Vehicle : IVehicle
    {
        private int _id;
        private Dictionary<string, string> map;
        public static Dictionary<int, Adapt_rep6_Vehicle> vehiclesMap = new Dictionary<int, Adapt_rep6_Vehicle>();


        public Adapt_rep6_Vehicle(int id)
        {
            map = new Dictionary<string, string>();
            map["drivers.Size()"] = "0";

            _id = id;
        }
        public int Id { get => _id; set => _id = value; }

        public void AddDriver(Adapt_rep6_Driver d)
        {

            int len = int.Parse(map["drivers.Size()"]) + 1;

            map[$"drivers[{len++}]"] = d.Id.ToString();

            map["drivers.Size()"] = len.ToString();
        }

        public List<IDriver> Drivers
        {
            get
            {
                int len = int.Parse(map["drivers.Size()"]);
                List<IDriver> drivers = new List<IDriver>();
                for (int i = 0; i < len; i++)
                {
                    drivers.Add(Adapt_rep6_Driver.mapDrivers[int.Parse(this.map[$"drivers[{i}]"])] as IDriver);
                }
                return drivers;
            }
        }

        public override string ToString()
        {
            return $"id = {Id}";
        }
    }

    public class Adapt_rep6_Bytebus : Adapt_rep6_Vehicle, IBytebus
    {
        public Rep6.Bytebus bus;

        public Adapt_rep6_Bytebus(Rep6.Bytebus bus) : base(bus.Id)
        {
            this.bus = bus;
        }
        public Adapt_rep6_Bytebus(int id, List<Adapt_rep6_Line> lines, string engineClass) : base(id)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map["engineClass"] = engineClass;
            map["lines.Size()"] = "0";

            if (lines != null)
            {
                map["lines.Size()"] = lines.Count.ToString();
                for (int i = 0; i < lines.Count; i++)
                {
                    map[$"lines[{i}]"] = lines[i].NumberDec.ToString();
                }
            }
            this.bus = new Rep6.Bytebus(id, map);
            vehiclesMap[id] = this;

        }
        public int Id
        {
            get => bus.id;
            set => bus.id = value;
        }
        public string EngineClass
        {
            get => bus.Map["id"];
            set => bus.Map.Add("id", value);
        }

        public List<ILine> Lines
        {
            get
            {
                int len = int.Parse(bus.Map["lines.Size()"]);
                List<ILine> lines = new List<ILine>();
                for (int i = 0; i < len; i++)
                {
                    lines.Add(Adapt_rep6_Line.linesMap[int.Parse(bus.Map[$"lines[{i}]"])] as ILine);
                }
                return lines;
            }
            set
            {
                bus.Map.Add("lines.Size()", value.Count.ToString());
                for (int i = 0; i < value.Count; i++)
                {
                    bus.Map.Add($"lines[{i}]", value[i].NumberDec.ToString());
                }
            }
        }

        public override string ToString()
        {
            string s = base.ToString();
            s += $" EngineClass: {EngineClass}";
            return s;
        }
    }
    public class Adapt_rep6_Tram : Adapt_rep6_Vehicle, ITram
    {
        public Rep6.Tram tram;

        public Adapt_rep6_Tram(Rep6.Tram tram) : base(tram.Id)
        {
            this.tram = tram;
            vehiclesMap[tram.id] = this;
        }

        public Adapt_rep6_Tram(int id, int carsNumber, Adapt_rep6_Line line) : base(id)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            map.Add("CarsNumber", carsNumber.ToString());
            if(line != null) map.Add("line", line.NumberDec.ToString());
            this.tram = new Rep6.Tram(id, map);
            vehiclesMap[id] = this;
        }

        public int Id
        {
            get => tram.id;
            set => tram.id = value;
        }

        public int CarsNumber { get => int.Parse(tram.Map["CarsNumber"]); set => tram.Map["CarsNumber"] = value.ToString(); }

        public ILine Line
        {
            get
            {
                return Adapt_rep6_Line.linesMap[int.Parse(tram.Map["line"])] as ILine;
            }
            set
            {
                tram.Map["line"] = value.NumberDec.ToString();
            }
        }
        public override string ToString()
        {
            string s = base.ToString();
            s += $" carsNumber: {CarsNumber}";
            return s;
        }
    }

    public class Adapt_rep6_Driver : IDriver
    {
        public Rep6.Driver driver;
        public static Dictionary<int, Adapt_rep6_Driver> mapDrivers = new Dictionary<int, Adapt_rep6_Driver>();

        public Adapt_rep6_Driver(Rep6.Driver driver)
        {
            this.driver = driver;
        }

        public Adapt_rep6_Driver(string name, string surname, int seniority, List<Adapt_rep6_Vehicle> vehicles)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();

            map.Add("name", name);
            map.Add("surname", surname);
            map.Add("seniority", seniority.ToString());
            map.Add("vehicles.Size()", "0");

            if (vehicles != null)
            {
                map["vehicles.Size()"] = vehicles.Count.ToString();
                for (int i = 0; i < vehicles.Count; i++)
                {
                    map.Add($"vehicles[{i}]", vehicles[i].Id.ToString());
                }
            }
            this.driver = new Rep6.Driver(map);
            mapDrivers[this.Id] = this;
        }

        public int Id
        {
            get => driver.id;
            set => driver.id = value;
        }

        public List<IVehicle> Vehicles
        {
            get
            {
                int len = int.Parse(driver.Map["vehicles.Size()"]);
                List<IVehicle> vehicles = new List<IVehicle>();
                for (int i = 0; i < len; i++)
                {
                    vehicles.Add((Adapt_rep6_Vehicle.vehiclesMap[int.Parse(driver.Map[$"vehicles[{i}]"])]));
                }
                return vehicles;
            }

        }

        public List<Adapt_rep6_Vehicle> GetAdaptVehicles
        {
            get
            {
                int len = int.Parse(driver.Map["vehicles.Size()"]);
                List<Adapt_rep6_Vehicle> vehicles = new List<Adapt_rep6_Vehicle>();
                for (int i = 0; i < len; i++)
                {
                    vehicles.Add((Adapt_rep6_Vehicle.vehiclesMap[int.Parse(driver.Map[$"vehicles[{i}]"])]));
                }
                return vehicles;
            }

        }
        public string Name { get => driver.Map["name"]; set => driver.Map["name"] = value; }
        public string Surname { get => driver.Map["surname"]; set => driver.Map["surname"] = value; }
        public int Seniority { get => int.Parse(driver.Map["seniority"]); set => driver.Map["seniority"] = value.ToString(); }

        public override string ToString()
        {
            string s = $"{Name} {Surname}, {Seniority}";
            return s;
        }
    }
}

