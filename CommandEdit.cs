using System.Collections;
using System;

namespace BTM
{
	public class CommandEdit : ICommand
	{
        private Dictionary<string, ICollectionEditor> collectionEditorsMap;
        private string? type;

        private string collectionName;
        private string field;
        int sign;
        private string signSymbol;
        string value;


        public CommandEdit(DataStorer dataStorer)
        {
            DataStorer = dataStorer;
            createCollectionFiltersMap();
        }
        public DataStorer DataStorer { get; set; }

        public void execute(string s)
        {
            if(getValuesFromString(s) == false) return;
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

        public bool checkcommandLine(string commandLine)
        {
            if(getValuesFromString(commandLine) == false)
            {
                return false;
            }
            if (collectionEditorsMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"Command Edit: editing item from {collectionName} that satisfies {field} {signSymbol} {value}";
        }
    }

    public abstract class CollectionEditor<T> : ICollectionEditor
    {
        protected CollectionFilter<T>? collectionFilter;
        protected Dictionary<string, string> fieldMap;
        private ICollection<T>? collection;
        protected T? item;

        public CollectionEditor()
        {
            fieldMap = new Dictionary<string, string>();
        }

        public bool findItem(string field, int sign, string value)
        {
            if (collectionFilter == null) return false;
            collectionFilter.filterCollection(field, sign, value);

            collection = collectionFilter.FilteredCollection;

            if(collection == null || collection.Count() != 1)
            {
                Console.WriteLine("couldnt find one specific item to edit");
                return false;
            }
            item = collection.CreateForwardIterator().currentItem();
            return true;
        }

        public abstract void editItem(string field, int sign, string value);
        
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
            fieldMap["commonname"] = item.CommonName;
            fieldMap["numberdec"] = item.NumberDec.ToString();
            fieldMap["numberhex"] = item.NumberHex;

            getFields();

            item.CommonName = fieldMap["commonname"];
            item.NumberDec = int.Parse(fieldMap["numberdec"]);
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
            fieldMap.Clear();
            fieldMap["name"] = item.Name;
            fieldMap["type"] = item.Type;
            fieldMap["id"] = item.Id.ToString();

            getFields();

            item.Name = fieldMap["name"];
            item.Id = int.Parse(fieldMap["id"]);
            item.Type = item.Type;

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
            fieldMap.Clear();
            fieldMap["name"] = item.Name;
            fieldMap["surname"] = item.Surname;
            fieldMap["seniority"] = item.Seniority.ToString();

            getFields();

            item.Name = fieldMap["name"];
            item.Seniority = int.Parse(fieldMap["seniority"]);
            item.Surname = fieldMap["surname"];
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
            fieldMap.Clear();
            fieldMap["carsnumber"] = item.CarsNumber.ToString();
            fieldMap["id"] = item.Id.ToString();

            getFields();

            item.CarsNumber = int.Parse(fieldMap["carsnumber"]);
            item.Id = int.Parse(fieldMap["id"]);
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
            fieldMap.Clear();
            fieldMap["engineclass"] = item.EngineClass;
            fieldMap["id"] = item.Id.ToString();

            getFields();

            item.EngineClass = fieldMap["engineclass"];
            item.Id = int.Parse(fieldMap["id"]);
        }
    }
}

