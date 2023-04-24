using System;
namespace BTM
{
    public class basicCommandFind : ICommandFind
    {
        public void execute()
        {
            Console.WriteLine(this.filteredCollections());
        }

        public string filteredCollections()
        {
            return "";
        }
    }

    public abstract class CommandFindDecorator : ICommandFind
    {
        protected ICommandFind commandFind;

        public CommandFindDecorator(ICommandFind commandFind)
        {
            this.commandFind = commandFind;
        }

        public int checkSign(string sign)
        {
            if (sign == ">") return 1;
            if (sign == "<") return -1;
            return 0;
        }


        public virtual void execute()
        {
            commandFind.execute();
        }

        public virtual string filteredCollections()
        {
            return commandFind.filteredCollections();
        }
    }

    public class CommandFindLines : CommandFindDecorator
    {
        private ICollection<ILine> collection;
        private int sign;
        private string field;
        private string value;
        private Func<ILine, string> f;

        public CommandFindLines(ICommandFind commandFind, ICollection<ILine> collection, string sign, string field, string value) : base(commandFind)
        {
            this.sign = checkSign(sign);
            this.field = field;
            this.value = value;
            this.collection = collection;
            f = this.setfunction();
        }

        private Func<ILine, string> setfunction()
        {
            switch (field)
            {
                case "numberdec":
                    return x => x.NumberDec.ToString();
                case "numberhex":
                    return x => x.NumberHex.ToString();
                case "commonName":
                    return x => x.CommonName;
                default:
                    return x => x.CommonName;
            }
        }

        public override string filteredCollections()
        {
            string s = commandFind.filteredCollections();
            Func<ILine, bool> func = item => f(item).CompareTo(value) == sign;   
            s += Algorithms.ForEachIfToString<ILine>(collection.CreateForwardIterator(), func);
            if (s == "") return "Nothing has been found\n";
            return s;
        }

        public override void execute()
        {
            Console.WriteLine(this.filteredCollections());
        }
    }

    public class CommandFindStops : CommandFindDecorator
    {
        private ICollection<IStop> collection;
        private int sign;
        private string field;
        private string value;
        private Func<IStop, string> f;

        public CommandFindStops(ICommandFind commandFind, ICollection<IStop> collection, string sign, string field, string value) : base(commandFind)
        {
            this.sign = checkSign(sign);
            this.field = field;
            this.value = value;
            this.collection = collection;
            f = this.setfunction();
        }

        private Func<IStop, string> setfunction()
        {
            switch (field)
            {
                case "id":
                    return x => x.Id.ToString();
                case "name":
                    return x => x.Name;
                case "type":
                    return x => x.Type;
                default:
                    return x => x.Id.ToString();
            }
        }

        public override string filteredCollections()
        {
            string s = commandFind.filteredCollections();
            Func<IStop, bool> func = item => f(item).CompareTo(value) == sign;
            s += Algorithms.ForEachIfToString<IStop>(collection.CreateForwardIterator(), func);
            if (s == "") return "Nothing has been found\n";
            return s;
        }

        public override void execute()
        {
            Console.WriteLine(this.filteredCollections());
        }
    }

    public class CommandFindDrivers : CommandFindDecorator
    {
        private ICollection<IDriver> collection;
        private int sign;
        private string field;
        private string value;
        private Func<IDriver, string> f;

        public CommandFindDrivers(ICommandFind commandFind, ICollection<IDriver> collection, string sign, string field, string value) : base(commandFind)
        {
            this.sign = checkSign(sign);
            this.field = field;
            this.value = value;
            this.collection = collection;
            f = this.setfunction();
        }

        private Func<IDriver, string> setfunction()
        {
            switch (field)
            {
                case "name":
                    return x => x.Name;
                case "surname":
                    return x => x.Surname;
                case "seniority":
                    return x => x.Seniority.ToString();
                default:
                    return x => x.Name;
            }
        }

        public override string filteredCollections()
        {
            string s = commandFind.filteredCollections();
            Func<IDriver, bool> func = item => f(item).CompareTo(value) == sign;
            s += Algorithms.ForEachIfToString<IDriver>(collection.CreateForwardIterator(), func);
            if (s == "") return "Nothing has been found\n";
            return s;
        }

        public override void execute()
        {
            Console.WriteLine(this.filteredCollections());
        }
    }

    public class CommandFindVehicles : CommandFindDecorator
    {
        private ICollection<IVehicle> collection;
        private int sign;
        private string field;
        private string value;
        private Func<IVehicle, string> f;

        public CommandFindVehicles(ICommandFind commandFind, ICollection<IVehicle> collection, string sign, string field, string value) : base(commandFind)
        {
            this.sign = checkSign(sign);
            this.field = field;
            this.value = value;
            this.collection = collection;
            f = this.setfunction();
        }

        private Func<IVehicle, string> setfunction()
        {
            switch (field)
            {
                case "id":
                    return x => x.Id.ToString();
                default:
                    return x => x.Id.ToString();
            }
        }

        public override string filteredCollections()
        {
            string s = commandFind.filteredCollections();
            Func<IVehicle, bool> func = item => f(item).CompareTo(value) == sign;
            s += Algorithms.ForEachIfToString<IVehicle>(collection.CreateForwardIterator(), func);
            if (s == "") return "Nothing has been found\n";
            return s;
        }

        public override void execute()
        {
            Console.WriteLine(this.filteredCollections());
        }
    }

}

