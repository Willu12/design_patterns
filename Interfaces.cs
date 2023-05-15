using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BTM
{
    public interface ILine
    {
        int NumberDec { get; set; }
        string NumberHex { get; set; }
        string CommonName { get; set; }

        List<IStop> Stops { get; }
        List<IVehicle> Vehicles { get; }
    }

    public interface IVehicle
    {
        int Id { get; set; }
        List<IDriver> Drivers { get; }
    }

    public interface IStop
    {
        int Id { get; set; }
        List<ILine> Lines { get; }
        string Name { get; set; }
        string Type { get; set; }
    }

    public interface IBytebus : IVehicle
    {
        List<ILine> Lines { get; }
        string EngineClass { get; set; }
    }

    public interface ITram : IVehicle
    {
        ILine Line { get; }
        int CarsNumber { get; set; }
    }
    public interface IDriver
    {
        List<IVehicle> Vehicles { get; }
        string Name { get; set; }
        string Surname { get; set; }
        int Seniority { get; set; }
    }

    public interface ICollection<T> : IEnumerable<T>
    {
        void Add(T obj);
        bool Delete(T obj);
        Iiterator<T> CreateForwardIterator();
        Iiterator<T> CreateReverseIterator();

    }

    public interface IPredicate<T>
    {
        public bool eval(T item);
    }

    public interface IFunction<T>
    {
        public void f(T item);
    }

    public interface Iiterator<T> : IEnumerator<T>
    {
        void First();
        void Next();
        bool isDone();
        T currentItem();
    }

    public interface IComparer<T>
    {
        int checkSign(string sign);
        bool compare(T? x, T? y);
    }
    public interface ICommand
    {
        void execute(string s = "");
       // DataStorer DataStorer { get; set; }
        bool checkcommandLine(string commandLine);
    }

    public interface ICollectionPrinter
    {
        void printCollection();
    }

    public interface IEditor<T>
    {
        void editItem(T item);
    }

    public interface ICollectionEditor
    {
        void editItem(string field, int sign, string value);
    }
    public interface ICollectionFilter
    {
        void printFilteredCollection(string field, int sign, string value);
    }

    public interface IItemAdder
    {
        void addItem(string representation);
    }

    public interface ILineCreator
    {
        ILine createLine(int numberDec, string numberHex, string commonName);
    }

    public interface IStopCreator
    {
        IStop createStop(int id, string name, string type);
    }

    public interface ITramCreator
    {
        ITram createTram(int id, int carsNumber);
    }
    public interface IBytebusCreator
    {
        IBytebus createBytebus(int id, string engineClass);
    }
    public interface IDriverCreator
    {
      IDriver createDriver(string name, string surname, int seniority);
    }
}
