using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM
{
    // predykat z zadania
    public class Predicate : IPredicate<ILine>
    {
        public bool eval(ILine line)
        {

            List<IVehicle> vehs_D = new();

            foreach (IVehicle v in line.Vehicles)
            {
                foreach (IDriver d in v.Drivers)
                {
                    if (d.Seniority > 10) return true;
                }
            }
            return false;
        }
    }

    public class Function : IFunction<ILine>
    {
        public void f(ILine line)
        {
            List<IVehicle> vehs_D = new();

            foreach (IVehicle v in line.Vehicles)
            {
                foreach (IDriver d in v.Drivers)
                {
                    if (d.Seniority > 10)
                    {
                        Console.WriteLine(line);
                        return;
                    }
                }
            }
            
        }
    }

}
