using System;
namespace BTM
{
    public class CommandFind : ICommand
    {
        private DataStorer _dataStorer = DataStorer.GetDataStorer();
        private Dictionary<string, ICollectionFilter> collectionFiltersMap;
        private string collectionName;
        private string field;
        private int sign;
        private string signSymbol;
        private string value;

        public CommandFind()
        {
            createCollectionFiltersMap();
        }

        public DataStorer DataStorer { get => _dataStorer; set => _dataStorer = value; }

        public void execute()
        {
            if(collectionFiltersMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return;
            }
            ICollectionFilter collectionFilter = collectionFiltersMap[collectionName];
            collectionFilter.printFilteredCollection(field, sign, value);
        }

        public void execute(string commandLine)
        {
            if (checkcommandLine(commandLine) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, commandLine) == false) return;
            execute();
        }
        private bool getValuesFromString(string commandLine)
        {
            if (commandLine.StartsWith("find ")) commandLine = commandLine.Substring("find ".Length);
            string[] words = System.Text.RegularExpressions.Regex.Split(commandLine, "\"([^\"]*)\"|(\\s+)");
            List<string> wordsList = new List<string>(words);
            wordsList.RemoveAll(item => item == " " || item == "");
            words = wordsList.ToArray();
            if (words.Length != 4)
            {
                Console.WriteLine("Incorrect syntax for find command (find <name_of_the_class> <name_of_field>=|<|><value>)");
                return false;
            }
            collectionName = words[0];
            field = words[1];
            sign = checkSign(words[2]);
            value = words[3];
            signSymbol = words[2];
            return true;
        }

        private void createCollectionFiltersMap()
        {
            collectionFiltersMap = new Dictionary<string, ICollectionFilter>();

            collectionFiltersMap["lines"] = new LineFilter(DataStorer.lines);
            collectionFiltersMap["stops"] = new StopFilter(DataStorer.stops);
            collectionFiltersMap["bytebuses"] = new BytebusFilter(DataStorer.bytebuses);
            collectionFiltersMap["drivers"] = new DriverFilter(DataStorer.drivers);
            collectionFiltersMap["trams"] = new TramFilter(DataStorer.trams);
        }

        private int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool checkcommandLine(string commandLine)
        {
            if (getValuesFromString(commandLine) == false) return false;

            if (collectionFiltersMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return false;
            }
            return true;
        }

        public bool enqueue(string s = "")
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        public override string ToString()
        {
            return $"command Find, {collectionName} that {field} {signSymbol} {value}";
        }

        public string saveCommand()
        {
            return "";
        }

        public void undo()
        {
            ; Console.WriteLine($"command {this} undoed");
        }
    }

    public abstract class CollectionFilter<T> : ICollectionFilter
    {
        ICollection<T> collection;
        ICollection<T>? filteredCollection;
        public Dictionary<string, Func<T, string>> getFieldGettersMap;

        public ICollection<T>? FilteredCollection { get => filteredCollection; set => filteredCollection = value; }

        protected CollectionFilter(ICollection<T> collection)
        {
            this.collection = collection;
            createFieldGetters();
        }


        public void filterCollection(string field, int sign, string value)
        {
            if (getFieldGettersMap.ContainsKey(field) == false)
            {
                Console.WriteLine("invalid field");
                return;
            }

            Func<T, string> getField = getFieldGettersMap[field];
            Func<T, bool> comparer = item =>
            {
                string fieldValue = getField(item).ToLower();
                int fieldvalueint = -1;
                if (int.TryParse(fieldValue, out fieldvalueint))
                {
                    return fieldvalueint.CompareTo(int.Parse(value)) == sign;
                }
                return fieldValue.CompareTo(value) == sign;
            };

            ICollection<T> filteredCollection = Algorithms.ForEachFilter<T>(collection, comparer);
            this.FilteredCollection = filteredCollection;

        }
        public void printFilteredCollection(string field, int sign, string value)
        {
            filterCollection(field, sign, value);
            if(FilteredCollection == null || FilteredCollection.Count() == 0)
            {
                Console.WriteLine("no items matching criterias");
                return;
            }

            CollectionPrinter<T> collectionPrinter = new CollectionPrinter<T>(FilteredCollection);
            collectionPrinter.printCollection();
        }

        public abstract void createFieldGetters();
    }

    public class LineFilter : CollectionFilter<ILine>
    {
        public LineFilter(ICollection<ILine> collection) : base(collection)
        {}
        public override void createFieldGetters()
        {
            getFieldGettersMap = new Dictionary<string, Func<ILine, string>>();

            getFieldGettersMap["commonname"] = new Func<ILine, string>(x => x.CommonName);
            getFieldGettersMap["numberdec"] = new Func<ILine, string>(x => x.NumberDec.ToString());
            getFieldGettersMap["numberhex"] = new Func<ILine, string>(x => x.NumberHex);
        }
    }

    public class StopFilter : CollectionFilter<IStop>
    {
        public StopFilter(ICollection<IStop> collection) : base(collection)
        { }

        public override void createFieldGetters()
        {
            getFieldGettersMap = new Dictionary<string, Func<IStop, string>>();

            getFieldGettersMap["id"] = new Func<IStop, string>(x => x.Id.ToString());
            getFieldGettersMap["name"] = new Func<IStop, string>(x => x.Name);
            getFieldGettersMap["type"] = new Func<IStop, string>(x => x.Type);
        }
    }

    public class DriverFilter : CollectionFilter<IDriver>
    {
        public DriverFilter(ICollection<IDriver> collection) : base(collection)
        { }

        public override void createFieldGetters()
        {
            getFieldGettersMap = new Dictionary<string, Func<IDriver, string>>();

            getFieldGettersMap["seniority"] = new Func<IDriver, string>(x => x.Seniority.ToString());
            getFieldGettersMap["name"] = new Func<IDriver, string>(x => x.Name);
            getFieldGettersMap["surname"] = new Func<IDriver, string>(x => x.Surname);
        }
    }

    public class BytebusFilter : CollectionFilter<IBytebus>
    {
        public BytebusFilter(ICollection<IBytebus> collection) : base(collection)
        { }
        public override void createFieldGetters()
        {
            getFieldGettersMap = new Dictionary<string, Func<IBytebus, string>>();

            getFieldGettersMap["id"] = new Func<IBytebus, string>(x => x.Id.ToString());
            getFieldGettersMap["engineclass"] = new Func<IBytebus, string>(x => x.EngineClass);
        }
    }

    public class TramFilter : CollectionFilter<ITram>
    {
        public TramFilter(ICollection<ITram> collection) : base(collection)
        { }

        public override void createFieldGetters()
        {
            getFieldGettersMap = new Dictionary<string, Func<ITram, string>>();

            getFieldGettersMap["id"] = new Func<ITram, string>(x => x.Id.ToString());
            getFieldGettersMap["engineclass"] = new Func<ITram, string>(x => x.CarsNumber.ToString());
        }
    }
}