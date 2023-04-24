using System;
namespace BTM
{
    public class basicCommandList : ICommandList
    {
        public void execute()
        {
            Console.WriteLine(this.listCollections());
        }

        public string listCollections()
        {
            return "";
        }
    }

    public abstract class commandListDecorator : ICommandList
    {
        protected ICommandList commandList;

        public commandListDecorator(ICommandList commandList)
        {
            this.commandList = commandList;
        }

        public virtual void execute()
        {
            commandList.execute();
        }

        public virtual string listCollections()
        {
            return commandList.listCollections();
        }
    }

    public class CollectionAdder<T> : commandListDecorator
    {
        ICollection<T> collection;
        public CollectionAdder(ICollection<T> collection,ICommandList commandList) : base(commandList)
        {
            this.collection = collection;
        }

        public  override string listCollections()
        {
            string s = commandList.listCollections();
            Func<T, string> func = item => item.ToString() + '\n';
            s += Algorithms.ForEachToString<T>(collection.CreateForwardIterator(), func);

            return s;
        }

        public override void execute()
        {
            Console.WriteLine(this.listCollections());
        }
    }







}

