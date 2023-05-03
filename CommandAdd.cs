using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace BTM
{
    public class CommandAdd :ICommand
    {
        private string representation;
        private string type;
        private DataStorer dataStorer;
        private Dictionary<string, ItemAdder> itemAddersMap;

        public DataStorer? DataStorer { get => dataStorer; set => dataStorer = value; }

        public CommandAdd(DataStorer dataStorer)
        {
            this.dataStorer = dataStorer;
            createItemAddersMap();
        }

        public void execute(string commandLine)
        {
            if (getValueFromCommandLine(commandLine) == false) return;
            if (itemAddersMap.ContainsKey(type) == false)
            {
                Console.WriteLine("invalid type");
                return;
            }
            ItemAdder itemAdder = itemAddersMap[type];
            itemAdder.getFields();
            itemAdder.addItem(representation);

        }
        private bool getValueFromCommandLine(string commandLine)
        {
            if (commandLine.StartsWith("add ")) commandLine = commandLine.Substring("add ".Length);
            string[] words = System.Text.RegularExpressions.Regex.Split(commandLine, "\"([^\"]*)\"|(\\s+)");
            List<string> wordsList = new List<string>(words);
            wordsList.RemoveAll(item => item == " " || item == "");
            words = wordsList.ToArray();

            if (words.Length != 2)
            {
                Console.WriteLine("Incorrect syntax for add command (add <type> <representation>)");
                return false;
            }
            type = words[0];
            representation = words[1];
            if(representation != "base" && representation != "secondary")
            {
                Console.WriteLine("invallid representation");
                return false;
            }
            return true;
        }

        private void createItemAddersMap()
        {
            itemAddersMap = new Dictionary<string, ItemAdder>();
            itemAddersMap["line"] = new LineAdder(dataStorer);
            itemAddersMap["stop"] = new StopAdder(dataStorer);
            itemAddersMap["bytebus"] = new BytebusAdder(dataStorer);
            itemAddersMap["tram"] = new TramAdder(dataStorer);
            itemAddersMap["driver"] = new DriverAdder(dataStorer);
        }
    }

    public abstract class ItemAdder: IItemAdder
    {
        protected Dictionary<string, string> fieldMap;
        protected DataStorer dataStorer;

        public abstract void addItem(string representation);

        protected ItemAdder(DataStorer dataStorer)
        {
            this.dataStorer = dataStorer;
            createFieldMap();
        }

        public void getFields()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null) continue;
                commandLine = commandLine.Trim().ToLower();
                if (commandLine == "exit")
                {
                    Console.WriteLine("Creating Aborted");
                    break;
                }
                if (commandLine == "done")
                {
                    if (checkFields() == false)
                    {
                        Console.WriteLine("Not all fields are set");
                        continue;
                    }
                    return;
                }

                int index = commandLine.IndexOf('=');
                if (index == -1 || fieldMap.ContainsKey(commandLine.Substring(0, index)) == false)
                {
                    Console.WriteLine("invalid command");
                    continue;
                }
                fieldMap[commandLine.Substring(0, index)] = commandLine.Substring(index + 1);
            }
        }
        protected abstract bool checkFields();
        protected abstract void createFieldMap();
    }

    public class LineAdder : ItemAdder
    {
        protected Dictionary<string, ILineCreator> lineCreatorsMap;
        protected string commonName;
        protected int numberDec;
        protected string numberHex;

        public LineAdder(DataStorer dataStorer) : base(dataStorer)
        {
            createLineAddersMap();
        }

        public override void addItem(string representation)
        {
            if(lineCreatorsMap.ContainsKey(representation) == false)
            {
                Console.WriteLine("invalid represenation");
                return;
            }
            ILineCreator lineCreator = lineCreatorsMap[representation];
            dataStorer.lines.Add(lineCreator.createLine(numberDec, numberHex, commonName));
        }

        protected override bool checkFields()
        {
            foreach(var p in fieldMap)
            {
                if (p.Value == "-1") return false;
            }
            this.commonName = fieldMap["commonname"];
            this.numberDec = int.Parse(fieldMap["numberdec"]);
            this.numberHex = numberDec.ToString("X");
            return true;
        }

        protected override void createFieldMap()
        {
            fieldMap = new Dictionary<string, string>();
            fieldMap.Add("commonname", "-1");
            fieldMap.Add("numberdec", "-1");
        }

        private void createLineAddersMap()
        {
            lineCreatorsMap = new Dictionary<string, ILineCreator>();
            lineCreatorsMap["base"] = new LineBaseCreator();
            lineCreatorsMap["secondary"] = new LineRep6Creator();
        }
    }
    
    public class StopAdder : ItemAdder
    {
        protected Dictionary<string, IStopCreator> stopCreatorsMap;
        protected string name;
        protected int id;
        protected string type;

        public StopAdder(DataStorer dataStorer) : base(dataStorer)
        {
            createStopAddersMap();
        }

        public override void addItem(string representation)
        {
            if (stopCreatorsMap.ContainsKey(representation) == false)
            {
                Console.WriteLine("invalid represenation");
                return;
            }
            IStopCreator stopCreator = stopCreatorsMap[representation];
            dataStorer.stops.Add(stopCreator.createStop(id, name, type));
        }

        protected override bool checkFields()
        {
            foreach (var p in fieldMap)
            {
                if (p.Value == "-1") return false;
            }
            this.name = fieldMap["name"];
            this.id = int.Parse(fieldMap["id"]);
            this.type = fieldMap["type"];
            return true;
        }

        protected override void createFieldMap()
        {
            fieldMap = new Dictionary<string, string>();
            fieldMap.Add("name", "-1");
            fieldMap.Add("id", "-1");
            fieldMap.Add("type", "-1");
        }

        private void createStopAddersMap()
        {
            stopCreatorsMap = new Dictionary<string, IStopCreator>();
            stopCreatorsMap["base"] = new StopBaseCreator();
            stopCreatorsMap["secondary"] = new StopRep6Creator();
        }
    }

    public class BytebusAdder : ItemAdder
    {
        protected Dictionary<string, IBytebusCreator> bytebusCreatorsMap;
        protected int id;
        protected string engineClass;

        public BytebusAdder(DataStorer dataStorer) : base(dataStorer)
        {
            createBytebusAddersMap();
        }

        public override void addItem(string representation)
        {
            if (bytebusCreatorsMap.ContainsKey(representation) == false)
            {
                Console.WriteLine("invalid represenation");
                return;
            }
            IBytebusCreator bytebusCreator = bytebusCreatorsMap[representation];
            dataStorer.bytebuses.Add(bytebusCreator.createBytebus(id,engineClass));
        }

        protected override bool checkFields()
        {
            foreach (var p in fieldMap)
            {
                if (p.Value == "-1") return false;
            }
            this.engineClass = fieldMap["engineclass"];
            this.id = int.Parse(fieldMap["id"]);
            return true;
        }

        protected override void createFieldMap()
        {
            fieldMap = new Dictionary<string, string>();
            fieldMap.Add("engineclass", "-1");
            fieldMap.Add("id", "-1");
        }

        private void createBytebusAddersMap()
        {
            bytebusCreatorsMap = new Dictionary<string, IBytebusCreator>();
            bytebusCreatorsMap["base"] = new BytebusBaseCreator();
            bytebusCreatorsMap["secondary"] = new BytebusRep6Creator();
        }
    }

    public class TramAdder : ItemAdder
    {
        protected Dictionary<string, ITramCreator> tramCreatorsMap;
        protected int id;
        protected int carsNumber;

        public TramAdder(DataStorer dataStorer) : base(dataStorer)
        {
            createTramAddersMap();
        }

        public override void addItem(string representation)
        {
            if (tramCreatorsMap.ContainsKey(representation) == false)
            {
                Console.WriteLine("invalid represenation");
                return;
            }
            ITramCreator tramCreator = tramCreatorsMap[representation];
            dataStorer.trams.Add(tramCreator.createTram(id,carsNumber));
        }

        protected override bool checkFields()
        {
            foreach (var p in fieldMap)
            {
                if (p.Value == "-1") return false;
            }
            this.carsNumber = int.Parse(fieldMap["carsnumber"]);
            this.id = int.Parse(fieldMap["id"]);
            return true;
        }

        protected override void createFieldMap()
        {
            fieldMap = new Dictionary<string, string>();
            fieldMap.Add("carsnumber", "-1");
            fieldMap.Add("id", "-1");
        }

        private void createTramAddersMap()
        {
            tramCreatorsMap = new Dictionary<string, ITramCreator>();
            tramCreatorsMap["base"] = new TramBaseCreator();
            tramCreatorsMap["secondary"] = new TramRep6Creator();
        }
    }

    public class DriverAdder : ItemAdder
    {
        protected Dictionary<string, IDriverCreator> driverCreatorsMap;
        protected int seniority;
        protected string name;
        protected string surname;

        public DriverAdder(DataStorer dataStorer) : base(dataStorer)
        {
            createDriverAddersMap();
        }

        public override void addItem(string representation)
        {
            if (driverCreatorsMap.ContainsKey(representation) == false)
            {
                Console.WriteLine("invalid represenation");
                return;
            }
            IDriverCreator driverCreator = driverCreatorsMap[representation];
            dataStorer.drivers.Add(driverCreator.createDriver(name,surname,seniority));
        }

        protected override bool checkFields()
        {
            foreach (var p in fieldMap)
            {
                if (p.Value == "-1") return false;
            }
            this.name = fieldMap["name"];
            this.surname = fieldMap["surname"];
            this.seniority = int.Parse(fieldMap["seniority"]);
            return true;
        }

        protected override void createFieldMap()
        {
            fieldMap = new Dictionary<string, string>();
            fieldMap.Add("name", "-1");
            fieldMap.Add("seniority", "-1");
            fieldMap.Add("surname","-1");
        }

        private void createDriverAddersMap()
        {
            driverCreatorsMap = new Dictionary<string, IDriverCreator>();
            driverCreatorsMap["base"] = new DriverBaseCreator();
            driverCreatorsMap["secondary"] = new DriverRep6Creator();
        }
    }
}