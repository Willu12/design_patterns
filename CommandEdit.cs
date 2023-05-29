using System.Collections;
using System;

namespace BTM
{
	public class CommandEdit : ICommand
    {
        private Dictionary<string, ICollectionEditor> collectionEditorsMap;
        private string? type;
        private DataStorer _dataStorer = DataStorer.GetDataStorer();

        private string collectionName;
        private string field;
        int sign;
        private string signSymbol;
        string value;

        public CommandEdit()
        {
            createCollectionFiltersMap();
        }
        public DataStorer DataStorer { get => _dataStorer; set => _dataStorer = value; }

        public bool checkcommandLine(string commandLine)
        {
            if (getValuesFromString(commandLine) == false)
            {
                return false;
            }
            if (collectionEditorsMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return false;
            }
            ICollectionEditor collectionEditor = collectionEditorsMap[collectionName];
            collectionEditor.prepareEditItem(field, sign, value);
            return true;
        }

        public bool enqueue(string s)
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        public void execute(string s)
        {
            if (checkcommandLine(s) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, s) == false) return;
            ICollectionEditor collectionEditor = collectionEditorsMap[collectionName];
            collectionEditor.editItem(field,sign,value);
        }

        public void execute()
        {
            if (collectionEditorsMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return;
            }
            ICollectionEditor collectionEditor = collectionEditorsMap[collectionName];
            collectionEditor.editItem(field, sign, value);
        }

        private void createCollectionFiltersMap()
        {
            collectionEditorsMap = new Dictionary<string, ICollectionEditor>();

            collectionEditorsMap["lines"] = new LineEditor(DataStorer.lines);
            collectionEditorsMap["stops"] = new StopEditor(DataStorer.stops);
            collectionEditorsMap["bytebuses"] = new BytebusEditor(DataStorer.bytebuses);
            collectionEditorsMap["drivers"] = new DriverEditor(DataStorer.drivers);
            collectionEditorsMap["trams"] = new TramEditor(DataStorer.trams);
        }

        private bool getValuesFromString(string commandLine)
        {
            if (commandLine.StartsWith("edit ")) commandLine = commandLine.Substring("find ".Length);
            string[] words = System.Text.RegularExpressions.Regex.Split(commandLine, "\"([^\"]*)\"|(\\s+)");
            List<string> wordsList = new List<string>(words);
            wordsList.RemoveAll(item => item == " " || item == "");
            words = wordsList.ToArray();
            if (words.Length != 4)
            {
                Console.WriteLine("Incorrect syntax for edit command (edit <name_of_the_class> <name_of_field>=|<|><value>)");
                return false;
            }
            collectionName = words[0];
            field = words[1];
            sign = checkSign(words[2]);
            signSymbol = words[2];
            value = words[3];
            return true;
        }

        private int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public override string ToString()
        {
            return $"Command Edit: editing item from {collectionName} that satisfies {field} {signSymbol} {value}";
        }

        public string saveCommand()
        {
            string s = "";
            foreach (var item in collectionEditorsMap[collectionName].getFieldsMap)
            {
                s += $"{item.Key}={item.Value}\n";
            }
            s += "done";
            return s;
        }

        public void undo()
        {
            Console.WriteLine($"{this} undoed");
            ICollectionEditor collectionEditor = collectionEditorsMap[collectionName];
            collectionEditor.undoEditItem();
        }
    }

    public abstract class CollectionEditor<T> : ICollectionEditor
    {
        protected CollectionFilter<T>? collectionFilter;
        protected Dictionary<string, string> fieldMap;
        private ICollection<T>? collection;
        protected T? item;

        public Dictionary<string, string> getFieldsMap => fieldMap;

        public CollectionEditor()
        {
            fieldMap = new Dictionary<string, string>();
        }

        public bool findItem(string field, int sign, string value)
        {
            if (collectionFilter == null) return false;
            collectionFilter.filterCollection(field, sign, value);

            collection = collectionFilter.FilteredCollection;

            if (collection == null)
            {
                Console.WriteLine($"There is no item that satisfies given criteria");
                return false;
            }

            if (collection.Count() != 1)
            {
                Console.WriteLine("there is more than one item that satisfies given criteria");
                return false;
            }
            item = collection.CreateForwardIterator().currentItem();
            return true;
        }

        public abstract void editItem(string field, int sign, string value);
        public abstract void prepareEditItem(string field, int sign, string value);
        
        public void getFields()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null) continue;
                commandLine = commandLine.Trim().ToLower();
                if (commandLine == "exit")
                {
                    Console.WriteLine("Editing Aborted");
                    break;
                }
                if (commandLine == "done") return;

                int index = commandLine.IndexOf('=');
                if (index == -1 || fieldMap.ContainsKey(commandLine.Substring(0, index)) == false)
                {
                    Console.WriteLine("invalid command");
                    continue;
                }
                fieldMap[commandLine.Substring(0, index)] = commandLine.Substring(index + 1);
            }
        }

        public abstract void undoEditItem();
    }

    public class LineEditor : CollectionEditor<ILine>
    {
        public LineEditor(ICollection<ILine> lines) : base()
        {
            collectionFilter = new LineFilter(lines);
        }
        public override void editItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            item.CommonName = fieldMap["commonname"];
            item.NumberDec = int.Parse(fieldMap["numberdec"]);
            item.NumberHex = item.NumberDec.ToString("X");
        }

        public override void prepareEditItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            fieldMap["commonname"] = item.CommonName;
            fieldMap["numberdec"] = item.NumberDec.ToString();
            fieldMap["numberhex"] = item.NumberHex;
            fieldMap["oldcommonname"] = item.CommonName;
            fieldMap["oldnumberdec"] = item.NumberDec.ToString();
            fieldMap["oldnumberhex"] = item.NumberHex;


            getFields();
        }

        public override void undoEditItem()
        {
            if (item == null) return;
            item.CommonName = fieldMap["oldcommonname"];
            item.NumberDec = int.Parse(fieldMap["oldnumberdec"]);
            item.NumberHex = item.NumberDec.ToString("X");
        }
    }

    public class StopEditor : CollectionEditor<IStop>
    {
        public StopEditor(ICollection<IStop> stops) : base()
        {
            collectionFilter = new StopFilter(stops);
        }
        public override void editItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            item.Name = fieldMap["name"];
            item.Id = int.Parse(fieldMap["id"]);
            item.Type = fieldMap["type"];
        }

        public override void prepareEditItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            fieldMap.Clear();
            fieldMap["name"] = item.Name;
            fieldMap["type"] = item.Type;
            fieldMap["id"] = item.Id.ToString();
            fieldMap["oldname"] = item.Name;
            fieldMap["oldtype"] = item.Type;
            fieldMap["oldid"] = item.Id.ToString();

            getFields();
        }

        public override void undoEditItem()
        {
            if (item == null) return;
            item.Name = fieldMap["oldname"];
            item.Id = int.Parse(fieldMap["oldid"]);
            item.Type = fieldMap["oldtype"];
        }
    }

    public class DriverEditor : CollectionEditor<IDriver>
    {
        public DriverEditor(ICollection<IDriver> drivers) : base()
        {
            collectionFilter = new DriverFilter(drivers);
        }
        public override void editItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            item.Name = fieldMap["name"];
            item.Seniority = int.Parse(fieldMap["seniority"]);
            item.Surname = fieldMap["surname"];
        }

        public override void prepareEditItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            fieldMap.Clear();
            fieldMap["name"] = item.Name;
            fieldMap["surname"] = item.Surname;
            fieldMap["seniority"] = item.Seniority.ToString();
            fieldMap["oldname"] = item.Name;
            fieldMap["oldsurname"] = item.Surname;
            fieldMap["oldseniority"] = item.Seniority.ToString();
            getFields();
        }

        public override void undoEditItem()
        {
            if (item == null) return;
            item.Name = fieldMap["oldname"];
            item.Seniority = int.Parse(fieldMap["oldseniority"]);
            item.Surname = fieldMap["oldsurname"];
        }
    }

    public class TramEditor : CollectionEditor<ITram>
    {
        public TramEditor(ICollection<ITram> trams) : base()
        {
            collectionFilter = new TramFilter(trams);
        }
        public override void editItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            item.CarsNumber = int.Parse(fieldMap["carsnumber"]);
            item.Id = int.Parse(fieldMap["id"]);
        }
        public override void prepareEditItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            fieldMap.Clear();
            fieldMap["carsnumber"] = item.CarsNumber.ToString();
            fieldMap["id"] = item.Id.ToString();
            fieldMap["oldcarsnumber"] = item.CarsNumber.ToString();
            fieldMap["oldid"] = item.Id.ToString();
            getFields();
        }

        public override void undoEditItem()
        {
            if (item == null) return;
            item.CarsNumber = int.Parse(fieldMap["oldcarsnumber"]);
            item.Id = int.Parse(fieldMap["oldid"]);
        }
    }

    public class BytebusEditor : CollectionEditor<IBytebus>
    {
        public BytebusEditor(ICollection<IBytebus> bytebuses) : base()
        {
            collectionFilter = new BytebusFilter(bytebuses);
        }
        public override void editItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            item.EngineClass = fieldMap["engineclass"];
            item.Id = int.Parse(fieldMap["id"]);
        }

        public override void prepareEditItem(string field, int sign, string value)
        {
            if (findItem(field, sign, value) == false) return;
            if (item == null) return;
            fieldMap.Clear();
            fieldMap["engineclass"] = item.EngineClass;
            fieldMap["id"] = item.Id.ToString();
            fieldMap["oldengineclass"] = item.EngineClass;
            fieldMap["oldid"] = item.Id.ToString();
            getFields();
        }

        public override void undoEditItem()
        {
            if (item == null) return;
            item.EngineClass = fieldMap["oldengineclass"];
            item.Id = int.Parse(fieldMap["oldid"]);
        }
    }
}

