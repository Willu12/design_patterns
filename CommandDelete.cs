using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BTM
{
    public class CommandDelete : ICommand
    {
        private Dictionary<string, ICollectionDeletor> collectionDeletorMap;
        private string? type;
        private DataStorer _dataStorer = DataStorer.GetDataStorer();


        private string collectionName;
        private string field;
        int sign;
        private string signSymbol;
        string value;

        public CommandDelete()
        {
            createCollectionFiltersMap();
        }
        public DataStorer DataStorer { get => _dataStorer; set => _dataStorer = value; }

        public void execute()
        {
            if (collectionDeletorMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return;
            }
            ICollectionDeletor collectionDeletor = collectionDeletorMap[collectionName];
            collectionDeletor.deleteItem(field, sign, value);
        }

        public void execute(string s)
        {
            if (checkcommandLine(s) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, s) == false) return;
            execute();
        }

        private void createCollectionFiltersMap()
        {
            collectionDeletorMap = new Dictionary<string, ICollectionDeletor>();

            collectionDeletorMap["lines"] = new LineDeletor(DataStorer.lines);
            collectionDeletorMap["stops"] = new StopDeletor(DataStorer.stops);
            collectionDeletorMap["bytebuses"] = new BytebusDeletor(DataStorer.bytebuses);
            collectionDeletorMap["drivers"] = new DriverDeletor(DataStorer.drivers);
            collectionDeletorMap["trams"] = new TramDeletor(DataStorer.trams);
        }

        private bool getValuesFromString(string commandLine)
        {
            if (commandLine.StartsWith("delete ")) commandLine = commandLine.Substring("delete ".Length);
            string[] words = System.Text.RegularExpressions.Regex.Split(commandLine, "\"([^\"]*)\"|(\\s+)");
            List<string> wordsList = new List<string>(words);
            wordsList.RemoveAll(item => item == " " || item == "");
            words = wordsList.ToArray();
            if (words.Length != 4)
            {
                Console.WriteLine("Incorrect syntax for delete command (delete <name_of_the_class> <name_of_field>=|<|><value>)");
                return false;
            }
            collectionName = words[0];
            field = words[1];
            sign = checkSign(words[2]);
            signSymbol = words[2];
            value = words[3];
            return true;
        }
        public bool enqueue(string s = "")
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        private int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }

        public bool checkcommandLine(string commandLine)
        {
            if (getValuesFromString(commandLine) == false)
            {
                return false;
            }
            if (collectionDeletorMap.ContainsKey(collectionName) == false)
            {
                Console.WriteLine("this collection doesnt exists");
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"Command Delete: deleting item from {collectionName} that satisfies {field} {signSymbol} {value}";
        }
        public string saveCommand()
        {
            return "";
        }

 
        public void undo()
        {
            Console.WriteLine($"command {this} undoed");
            ICollectionDeletor collectionDeletor = collectionDeletorMap[collectionName];
            collectionDeletor.restoreItem();
        }

        public abstract class CollectionDeletor<T> : ICollectionDeletor
        {
            protected CollectionFilter<T>? collectionFilter;
            protected T? item;
            protected ICollection<T>? collection;
            public bool findItem(string field, int sign, string value)
            {
                if (collectionFilter == null) return false;
                collectionFilter.filterCollection(field, sign, value);

                ICollection<T> collection = collectionFilter.FilteredCollection;

                if (collection == null)
                {
                    Console.WriteLine($"There is no item that satisfies given criteria");
                    return false;
                }

                if (collection == null || collection.Count() != 1)
                {
                    Console.WriteLine("there is more than one item that satisfies given criteria");
                    return false;
                }
                item = collection.CreateForwardIterator().currentItem();
                return true;
            }

            public void deleteItem(string field, int sign, string value)
            {
                if (collection == null) return;
                if (findItem(field, sign, value) == false) return;
                if (item == null) return;
                collection.Delete(item);
            }

            public void restoreItem()
            {
                if (collection == null) return;
                if(item == null) return;    
                collection.Add(item);
            }


        }

        public class LineDeletor : CollectionDeletor<ILine>
        {
            public LineDeletor(ICollection<ILine> lines) : base()
            {
                collectionFilter = new LineFilter(lines);
                collection = lines;
            }
        }

        public class StopDeletor : CollectionDeletor<IStop>
        {
            public StopDeletor(ICollection<IStop> stops) : base()
            {
                collectionFilter = new StopFilter(stops);
                collection = stops;
            }
        }

        public class DriverDeletor : CollectionDeletor<IDriver>
        {
            public DriverDeletor(ICollection<IDriver> drivers) : base()
            {
                collectionFilter = new DriverFilter(drivers);
                collection = drivers;
            }
        }

        public class TramDeletor : CollectionDeletor<ITram>
        {
            public TramDeletor(ICollection<ITram> trams) : base()
            {
                collectionFilter = new TramFilter(trams);
                collection = trams;
            }
        }

        public class BytebusDeletor : CollectionDeletor<IBytebus>
        {
            public BytebusDeletor(ICollection<IBytebus> bytebuses) : base()
            {
                collectionFilter = new BytebusFilter(bytebuses);
                collection = bytebuses;
            }
        }
    }
}

