using System;
namespace BTM
{
    public class LineBaseCreator : ILineCreator
    {
        public LineBaseCreator()
        {
        }

        public ILine createLine(int numberDec, string numberHex, string commonName)
        {
            return new Rep0.Line(numberHex, numberDec, commonName, null, null);
        }
    }

    public class LineRep6Creator : ILineCreator
    {
        public LineRep6Creator()
        {
        }

        public ILine createLine(int numberDec, string numberHex, string commonName)
        {
            return new Adapt_rep6_Line(numberHex, numberDec, commonName, null, null);
        }
    }

    public class StopBaseCreator : IStopCreator
    {
        public StopBaseCreator()
        {
        }

        public IStop createStop(int id, string name, string type)
        {
            return new Rep0.Stop(id, name, type, null);
        }
    }

    public class StopRep6Creator : IStopCreator
    {
        public StopRep6Creator()
        {
        }

        public IStop createStop(int id, string name, string type)
        {
            return new Adapt_rep6_Stop(id, name, type, null);
        }
    }

    public class BytebusBaseCreator : IBytebusCreator
    {
        public BytebusBaseCreator() { }
        public IBytebus  createBytebus(int id, string engineClass)
        {
            return new Rep0.Bytebus(id, null, engineClass);
        }
    }

    public class BytebusRep6Creator : IBytebusCreator
    {
        public BytebusRep6Creator() { }
        public IBytebus createBytebus(int id, string engineClass)
        {
            return new Adapt_rep6_Bytebus(id, null, engineClass);
        }
    }

    public class TramBaseCreator : ITramCreator
    {
        public TramBaseCreator() { }

        public ITram createTram(int id, int carsNumber)
        {
            return new Rep0.Tram(id, carsNumber, null);
        }
    }
    public class TramRep6Creator : ITramCreator
    {
        public TramRep6Creator() { }

        public ITram createTram(int id, int carsNumber)
        {
            return new Adapt_rep6_Tram(id, carsNumber, null);
        }
    }

    public class DriverBaseCreator : IDriverCreator
    {
        public DriverBaseCreator() { }
        public IDriver createDriver(string name, string surname, int seniority)
        {
            return new Rep0.Driver(name, surname, seniority, null);
        }
    }
    public class DriverRep6Creator : IDriverCreator
    {
        public DriverRep6Creator() { }
        public IDriver createDriver(string name, string surname, int seniority)
        {
            return new Adapt_rep6_Driver(name, surname, seniority, null);
        }
    }
}

