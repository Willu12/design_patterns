using System;
namespace BTM
{
	public class Algorithms
	{
        public static bool f(ILine line)
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

        public static void PrintTask(List<ILine> Lines, List<IDriver> drivers)
        {
            List<IVehicle> vehs_L = new();
            List<IVehicle> vehs_D = new();

            foreach (var d in drivers)
            {
                if (d.Seniority > 10) vehs_D.AddRange(d.Vehicles);
            }
            foreach (var L in Lines)
            {
                List<IVehicle> vehs_tmp = new List<IVehicle>(vehs_D);
                vehs_tmp.Intersect(L.Vehicles);
                if (vehs_tmp.Count > 0)
                {
                    Console.WriteLine($"{L.CommonName}, {L.NumberDec}");
                }
            }
        }
        public static T? Find<T>(ICollection<T> collection, Func<T, bool> pred, bool reverse = false)
        {
            Iiterator<T> iterator;
            if (reverse) iterator = collection.CreateReverseIterator();
            else iterator = collection.CreateForwardIterator();

            iterator.First();

            while (iterator.isDone() == false)
            {
                if (pred(iterator.currentItem()) == true) return iterator.currentItem();
                iterator.Next();
            }
            return default(T);
        }



        public static T? Find<T>(Iiterator<T> iterator, IPredicate<T> pred) where T : class
        {
            while (iterator.isDone() == false)
            {
                if (pred.eval(iterator.currentItem()))
                {
                    return iterator.currentItem();
                }
                iterator.Next();
            }
            return null;
        }

        public static void ForEach<T>(Iiterator<T> iterator, IFunction<T> Fun)
        {
            while (iterator.isDone() == false)
            {
                Fun.f(iterator.currentItem());
                iterator.Next();
            }
        }

        public static string ForEachToString<T>(Iiterator<T> iterator, Func<T,string> func)
        {
            string s = "";
            while (iterator.isDone() == false)
            {
                s += func(iterator.currentItem());
                iterator.Next();
            }
            return s;
        }

        public static string ForEachIfToString<T>(Iiterator<T> iterator, Func<T, bool> func)
        {
            string s = "";
            while (iterator.isDone() == false)
            {
                if (func(iterator.currentItem())) s += iterator.currentItem().ToString() + '\n';
                iterator.Next();
            }
            return s;
        }

        public static int CountIf<T>(Iiterator<T> iterator, IPredicate<T> pred)
        {
            int sum = 0;
            while (iterator.isDone() == false)
            {
                if (pred.eval(iterator.currentItem())) sum++;
                iterator.Next();
            }
            return sum;
        }

        public static void Print<T>(ICollection<T> collection, Func<T, bool> pred)
        {
            Iiterator<T> iterator = collection.CreateForwardIterator();

            iterator.First();

            while (iterator.isDone() == false)
            {
                Console.WriteLine(iterator.currentItem().ToString());
                iterator.Next();
            }
        }
    }
}

