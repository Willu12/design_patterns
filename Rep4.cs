using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace BTM
{
    public class Rep4
    {
        public static Dictionary<int, string> Map = new Dictionary<int, string>();
        public class Line
        {
            public int commonName;
            public int numberHex;
            public int numberDec;
            public List<int> stops;
            public List<int> vehicles;

            public Line(int numberHex,int numberDec,int commonName,List<int> stops = null, List<int> vehicles = null)
            {
                this.numberHex = numberHex;
                this.numberDec = numberDec;
                this.commonName = commonName;
                this.stops = stops;
                this.vehicles = vehicles;
            }
        }

        public class Stop
        {
            public int name;
            public int id;
            public int type;
            public List<int> lines;

            public Stop(int id,int name, int type, List<int> lines = null)
            {
                this.id = id;
                this.name = name;
                this.type = type;
                this.lines = lines;
            }

            
        }

        public abstract class Vehicle
        {
            public int id;
            public List<int> drivers;
            public Vehicle(int id)
            {
                this.id = id;
                drivers = new List<int>();
            }
        }

        public class Bytebus : Vehicle
        {
            public int engineClass;
            public List<int> lines;

            public Bytebus(int id,List<int> lines, int engineClass) : base(id)
            {
                this.engineClass = engineClass;
                this.lines = lines;
            }

            /*
            public Bytebus(int id, List<Line> lines, string engineClass) : base(id)
            {
                this.engineClass = engineClass.GetHashCode();
                if (Map.ContainsKey(engineClass.GetHashCode()) == false)
                    Map.Add(engineClass.GetHashCode(), engineClass);
                this.linesIds = new List<int>();
                if (lines != null)
                    foreach (var l in lines)
                    {
                        linesIds.Add(l.NumberDec);
                    }
            }
            */

            

        }

        public class Tram : Vehicle
        {
            public int carsNumber;
            public int lineId;

            public Tram(int id, int carsNumber, int line) : base(id)
            {
                this.carsNumber = carsNumber;
                this.lineId = line;
            }
        }

        public class Driver
        {
            public List<int> vehicles;
            public int name;
            public int surname;
            public int seniority;
            public int id;
            static int c_id = 0;

            public Driver(int name, int surname, int seniority, List<int> vehicles)
            {
                this.name = name;
                this.surname = surname;
                this.seniority = seniority;
                this.vehicles = vehicles;
                this.id = c_id++;
            }

            /*
            public Driver(string name, string surname, int seniority, List<Vehicle> vehicles)
            {
                this.vehiclesIds = new List<int>();
                foreach (var v in vehicles)
                {
                    vehiclesIds.Add(v.Id);
                }
                this.name = name.GetHashCode();
                this.surname = surname.GetHashCode();
                this.seniority = seniority;
                if (Map.ContainsKey(name.GetHashCode()) == false)
                    Map.Add(name.GetHashCode(), name);
                Map.Add(surname.GetHashCode(), surname);
            }
            */
        }
    }

    public class Adapt_rep4_Line : ILine
    {
        public Rep4.Line line;

        public static Dictionary<int, Adapt_rep4_Line> linesMap = new Dictionary<int, Adapt_rep4_Line>();


        
        public Adapt_rep4_Line(string numberHex, int numberDec, string commonName, List<Adapt_rep4_Stop> stops = null, List<Adapt_rep4_Stop> vehicles = null)
        {
            this.line = new Rep4.Line(numberHex.GetHashCode(), numberDec.ToString().GetHashCode(), commonName.GetHashCode(), null, null);

            Rep4.Map[line.numberHex] = numberHex;
            Rep4.Map[line.numberDec] = numberDec.ToString();
            Rep4.Map[line.commonName] = commonName;
            line.stops = new List<int>();
            line.vehicles = new List<int>();

            if (stops != null)
            {
                foreach(var s in stops)
                {
                    line.stops.Add(s.Id);
                }
            }
            if (vehicles != null)
            {
                foreach (var v in vehicles)
                {
                    line.vehicles.Add(v.Id);
                }
            }
            linesMap[numberDec] = this;

        }

      

        

        public int NumberDec
        {
            get => int.Parse(Rep4.Map[line.numberDec]);
            set
            {
                line.numberDec = value.ToString().GetHashCode();
                Rep4.Map[line.numberDec] = value.ToString();

            }
        }
        public string NumberHex
        {
            get => Rep4.Map[line.numberHex];
            set
            {
                line.numberHex = value.GetHashCode();
                Rep4.Map[line.numberHex] = value;

            }
        }
        public string CommonName
        {
            get => Rep4.Map[line.commonName];
            set
            {
                line.commonName = value.GetHashCode();
                Rep4.Map[line.commonName] = value;

            }
        }
        public List<IStop> Stops
        {
            get
            {
                List<IStop> list = new List<IStop>();
                if (this.line.stops == null) return list;

                foreach(var s in line.stops)
                {
                    list.Add(Adapt_rep4_Stop.stopsMap[s]);
                }
                return list;
            }
        }
        public List<Adapt_rep4_Stop> SetStops
        {
            set
            {
                line.stops = new List<int>();
                foreach (var s in value)
                {
                    line.stops.Add(s.Id);
                }
            }
        }

        public List<IVehicle> Vehicles
        {
            get
            {
                List<IVehicle> list = new List<IVehicle>();
                if (this.line.vehicles == null) return list;

                foreach (var v in line.vehicles)
                {
                    list.Add(Adapt_rep4_Vehicle.vehiclesMap[v]);
                }
                return list;
            }
        }
        public List<Adapt_rep4_Vehicle> SetVehicles
        {
            set
            {
                line.vehicles = new List<int>();
                foreach (var v in value)
                {
                    line.vehicles.Add(v.Id);
                }
            }
        }

        public override string ToString()
        {
            string s = $"{this.NumberDec}, {this.CommonName}";
            return s;
        }
    }

    public class Adapt_rep4_Stop : IStop
    {
        public Rep4.Stop stop;
        public static Dictionary<int, Adapt_rep4_Stop> stopsMap = new Dictionary<int, Adapt_rep4_Stop>();


        public Adapt_rep4_Stop(int id, string name, string type, List<Adapt_rep4_Line> lines = null)
        {
            this.stop = new Rep4.Stop(id.ToString().GetHashCode(), name.GetHashCode(), type.GetHashCode(), null);
            Rep4.Map[stop.name] = name;
            Rep4.Map[stop.id] = id.ToString();
            Rep4.Map[stop.type] = type;
            stop.lines = new List<int>();

            if (lines != null)
            {
                foreach (var l in lines)
                {
                    stop.lines.Add(l.NumberDec);
                }
            }
            stopsMap[id] = this;
        }

        public int Id
        {
            get => int.Parse(Rep4.Map[stop.id]);

            set
            {
                stop.id = value.ToString().GetHashCode();
                Rep4.Map[stop.id] = value.ToString();
            }
        }

        public string Name
        {
            get => Rep4.Map[stop.name];

            set
            {
                stop.name = value.GetHashCode();
                Rep4.Map[stop.name] = value;
            }
        }

        public string Type
        {
            get => Rep4.Map[stop.type];

            set
            {
                stop.type = value.GetHashCode();
                Rep4.Map[stop.type] = value;
            }
        }


        public List<ILine> Lines
        {
            get
            {
                List<ILine> list = new List<ILine>();
                if (this.stop.lines == null) return list;

                foreach (var s in stop.lines)
                {
                    list.Add(Adapt_rep4_Line.linesMap[s]);
                }
                return list;
            }
        }
        public List<Adapt_rep4_Line> SetLines
        {
            set
            {
                stop.lines = new List<int>();
                foreach (var s in value)
                {
                    stop.lines.Add(s.NumberDec);
                }
            }
        }
    }
    public class Adapt_rep4_Vehicle : IVehicle
    {
        public static Dictionary<int, Adapt_rep4_Vehicle> vehiclesMap = new Dictionary<int, Adapt_rep4_Vehicle>();
        private int _id;
        private List<int> _drivers;

        public Adapt_rep4_Vehicle(int id)
        {
            _id = id.ToString().GetHashCode();
            Rep4.Map[_id] = id.ToString();
            _drivers = new List<int>();
        }
        public void AddDriver(Adapt_rep4_Driver d)
        {
            _drivers.Add(d.Id);
        }

        public List<IDriver> Drivers
        {
            get
            {
                List<IDriver> list = new List<IDriver>();
                if (this._drivers == null) return list;

                foreach (var s in _drivers)
                {
                    list.Add(Adapt_rep4_Driver.mapDrivers[s]);
                }
                return list;
            }
        }

        public List<Adapt_rep4_Driver> GetAdaptDrivers
        {
            get
            {
                List<Adapt_rep4_Driver> list = new List<Adapt_rep4_Driver>();
                if (this._drivers == null) return list;

                foreach (var s in _drivers)
                {
                    list.Add(Adapt_rep4_Driver.mapDrivers[s]);
                }
                return list;
            }
        }

        public int Id { get => int.Parse(Rep4.Map[_id]); set { _id = value.ToString().GetHashCode(); Rep4.Map[_id] = value.ToString(); } }
    }
    public class Adapt_rep4_Bytebus : Adapt_rep4_Vehicle, IBytebus
    {
        public Rep4.Bytebus bus;
        public Adapt_rep4_Bytebus(int id, List<Adapt_rep4_Line> lines, string engineClass) : base(id)
        { 
            this.bus = new Rep4.Bytebus(id.ToString().GetHashCode(), null,engineClass.GetHashCode());
            Rep4.Map[bus.engineClass] = engineClass;
            Rep4.Map[bus.id] = id.ToString();
            bus.lines = new List<int>();

            if (lines != null)
            {
                foreach (var l in lines)
                {
                    bus.lines.Add(l.NumberDec);
                }
            }
            vehiclesMap[id] = this;
        }

   

        public string EngineClass
        {
            get => Rep4.Map[bus.engineClass];

            set
            {
                bus.engineClass= value.GetHashCode();
                Rep4.Map[bus.engineClass] = value;
            }
        }

        public List<ILine> Lines
        {
            get
            {
                List<ILine> list = new List<ILine>();
                if (this.bus.lines == null) return list;

                foreach (var s in bus.lines)
                {
                    list.Add(Adapt_rep4_Line.linesMap[s]);
                }
                return list;
            }
        }
        public List<Adapt_rep4_Line> SetLines
        {
            set
            {
                bus.lines = new List<int>();
                foreach (var s in value)
                {
                    bus.lines.Add(s.NumberDec);
                }
            }
        }


    }
    public class Adapt_rep4_Tram : Adapt_rep4_Vehicle, ITram
    {
        public Rep4.Tram tram;
        public Adapt_rep4_Tram(int id, int carsNumber, Adapt_rep4_Line line) : base(id)
        {
            this.tram = new Rep4.Tram(id.ToString().GetHashCode(), carsNumber.ToString().GetHashCode(), line.NumberDec);
            Rep4.Map[tram.carsNumber] = carsNumber.ToString();
            Rep4.Map[tram.id] = id.ToString();
            
            vehiclesMap[id] = this;
        }

        

        public int CarsNumber
        {
            get => int.Parse(Rep4.Map[tram.carsNumber]);

            set
            {
                tram.carsNumber = value.ToString().GetHashCode();
                Rep4.Map[tram.carsNumber] = value.ToString();
            }
        }

       
        public ILine Line
        {
            get => Adapt_rep4_Line.linesMap[tram.lineId] as ILine;
            set => tram.lineId = value.NumberDec;
        }
    }
    public class Adapt_rep4_Driver : IDriver
    {
        public Rep4.Driver driver;
        public static Dictionary<int, Adapt_rep4_Driver> mapDrivers = new Dictionary<int, Adapt_rep4_Driver>();

        public Adapt_rep4_Driver(Rep4.Driver driver)
        {
            this.driver = driver;
        }

        public Adapt_rep4_Driver(string name, string surname, int seniority, List<Adapt_rep4_Vehicle> vehicles)
        {
            this.driver = new Rep4.Driver(name.GetHashCode(), surname.GetHashCode(),seniority.ToString().GetHashCode(), null);
            Rep4.Map[driver.name] = name;
            Rep4.Map[driver.surname] = surname;
            Rep4.Map[driver.seniority] = seniority.ToString();

            driver.vehicles = new List<int>();
            if(vehicles != null)
            {
                foreach (var v in vehicles)
                {
                    driver.vehicles.Add(v.Id);
                }
                
            }

            Rep4.Map[driver.id] = driver.id.ToString();
            mapDrivers[driver.id] = this;
        }

        public int Id
        {
            get => int.Parse(Rep4.Map[driver.id]);
            
        }

        public int Seniority
        {
            get => int.Parse(Rep4.Map[driver.seniority]);

            set
            {
                driver.seniority = value.ToString().GetHashCode();
                Rep4.Map[driver.seniority] = value.ToString();
            }
        }

        public string Name
        {
            get => Rep4.Map[driver.name];

            set
            {
                driver.name = value.GetHashCode();
                Rep4.Map[driver.name] = value;
            }
        }


        public string Surname
        {
            get => Rep4.Map[driver.surname];

            set
            {
                driver.surname = value.GetHashCode();
                Rep4.Map[driver.surname] = value;
            }
        }


        public List<IVehicle> Vehicles
        {
            get
            {
                List<IVehicle> list = new List<IVehicle>();
                if (this.driver.vehicles == null) return list;

                foreach (var v in driver.vehicles)
                {
                    list.Add(Adapt_rep4_Vehicle.vehiclesMap[v]);
                }
                return list;
            }
        }

        public List<Adapt_rep4_Vehicle> GetAdaptVehicles
        {
            get
            {
                List<Adapt_rep4_Vehicle> list = new List<Adapt_rep4_Vehicle>();
                if (this.driver.vehicles == null) return list;

                foreach (var v in driver.vehicles)
                {
                    list.Add(Adapt_rep4_Vehicle.vehiclesMap[v]);
                }
                return list;
            }
        }

        public List<Adapt_rep4_Vehicle> SetVehicles
        {
            set
            {
                driver.vehicles = new List<int>();
                foreach (var v in value)
                {
                    driver.vehicles.Add(v.Id);
                }
            }
        }
        
    }

}


