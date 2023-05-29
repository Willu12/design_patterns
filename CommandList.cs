using System;
namespace BTM
{
    public class CommandList : ICommand
    {
        private DataStorer _dataStorer = DataStorer.GetDataStorer();
        private Dictionary<string, ICollectionPrinter> collectionPrintersMap;
        private List<string> words;
        public DataStorer DataStorer { get => _dataStorer; set => _dataStorer = value;}

        public CommandList()
        {
            words = new List<string>();
            createCollectionPrintersMap();
        }

        public void execute()
        {
            string[] collectionNames = words.ToArray();

            foreach (string collectionName in collectionNames)
            {
                if (collectionPrintersMap.ContainsKey(collectionName) == false)
                {
                    Console.WriteLine($"collection \"{collectionName}\" doesn't exist");
                    return;
                }
                ICollectionPrinter collectionPrinter = collectionPrintersMap[collectionName];
                collectionPrinter.printCollection();
            }
        }
        public void execute(string commandLine)
        {
            if (checkcommandLine(commandLine) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, commandLine) == false) return;
            execute();
        }

        private void createCollectionPrintersMap()
        {
            collectionPrintersMap = new Dictionary<string, ICollectionPrinter>();

            collectionPrintersMap["lines"] = new CollectionPrinter<ILine>(DataStorer.lines);
            collectionPrintersMap["stops"] = new CollectionPrinter<IStop>(DataStorer.stops);
            collectionPrintersMap["drivers"] = new CollectionPrinter<IDriver>(DataStorer.drivers);
            collectionPrintersMap["bytebuses"] = new CollectionPrinter<IBytebus>(DataStorer.bytebuses);
            collectionPrintersMap["trams"] = new CollectionPrinter<ITram>(DataStorer.trams);
        }

        private string[] GetValuesFromString(string commandLine)
        { 
            if (commandLine.StartsWith("list ")) commandLine = commandLine.Substring("list ".Length);
            return commandLine.Split(' ');
        }

        public bool checkcommandLine(string commandLine)
        {
            string[] words = GetValuesFromString(commandLine);

            foreach(string word in words)
            {
                if (collectionPrintersMap.ContainsKey(word) == false) return false;
            }
            this.words = new List<string>(words);
            return true;
        }

        public bool enqueue(string s = "")
        {
            if(checkcommandLine(s)) return true;
            return false;
        }

        public override string ToString()
        {
            string s = "Command List : ";
            foreach(string word in words)
            {
                s += $"{word} ";
            }
            return s;
        }

        public string saveCommand()
        {
            return "";
        }

       

        public void undo()
        {
            Console.WriteLine($"command {this} undoed");
        }
    }
    public class CollectionPrinter<T> : ICollectionPrinter
    {
        private ICollection<T> collection;

        public CollectionPrinter(ICollection<T> collection)
        {
            this.collection = collection;
        }

        public void printCollection()
        {
            Func<T, string> func = item => item.ToString() + '\n';
            string s = Algorithms.ForEachToString<T>(collection.CreateForwardIterator(), func);
            Console.WriteLine(s);
        }
    }

}

