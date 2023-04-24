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
        List<IDriver> Drivers {get;}
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

    public interface ICollection<T>
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

    public interface Iiterator<T>
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
        void execute();
    }

    public interface ICommandList : ICommand
    {
        String listCollections();
    }

    public interface ICommandFind : ICommand
    {
        string filteredCollections();
    }
}
