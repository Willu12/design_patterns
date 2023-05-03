using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM
{
    public class DataStorer
    {
        public ICollection<ILine> lines;
        public ICollection<IBytebus> bytebuses;
        public ICollection<ITram> trams;
        public ICollection<IDriver> drivers;
        public ICollection<IStop> stops;

        private static DataStorer instance;

        protected DataStorer(ICollection<ILine> lines, ICollection<IBytebus> bytebuses, ICollection<ITram> trams, ICollection<IDriver> drivers, ICollection<IStop> stops)
        {
            this.lines = lines;
            this.bytebuses = bytebuses;
            this.trams = trams;
            this.drivers = drivers;
            this.stops = stops;
        }

        public static DataStorer createDataStorer(ICollection<ILine> lines, ICollection<IBytebus> bytebuses, ICollection<ITram> trams, ICollection<IDriver> drivers, ICollection<IStop> stops)
        {
            if(instance == null)
            {
                instance = new DataStorer(lines, bytebuses, trams, drivers, stops);
            }
            return instance;
        }
        public static DataStorer? GetDataStorer()
        {
            if(instance == null)
            {
                Console.WriteLine("Data hasnt been created yet");
                return null;
            }
            return instance;

        }
    }
}
