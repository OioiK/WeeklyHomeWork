using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeeklyHomeWork
{
    class MyCollection<T> : IEnumerable<T>
    {
        T[] items = new T[1];
        int count;

        public void Add(T item)
        {
            if(count == items.Length)            
                Array.Resize(ref items, items.Length + 1);
            items[count++] = item;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        public T[] GetFirstAndLast()
        {
            T[] firstAndLast = new T[2];

            firstAndLast[0] = items.First();
            firstAndLast[1] = items.Last();
            return firstAndLast;
        }

        public T[] CutMiddle()
        {
            int position;
            T[] tempArray = new T[items.Length];
            if ((items.Length % 2 == 0) && items.Length > 3)
            {
                position = items.Length / 2 - 1;

                List<T> temp = new List<T>(items);
                temp.RemoveRange(position, 2);
                tempArray = temp.ToArray();
            }
            else if ((items.Length % 2 != 0) && items.Length > 3)
            {
                position = items.Length / 2 - 1;

                List<T> temp = new List<T>(items);
                temp.RemoveAt(position);
                tempArray = temp.ToArray();
            }else if (items.Length == 3)
            {
                List<T> temp = new List<T>(items);
                temp.RemoveAt(1);
                tempArray = temp.ToArray();
            }

            return tempArray;
        }

        public T[] GetClosest(T value, bool isValid)
        {
            List<T> temp = new List<T>(items);
            T[] tempArray = new T[3];
            if (isValid && temp.Contains(value))
            {
                int[] indexes = new int[3];
                indexes[1] = temp.IndexOf(value);
                if(indexes[1] > 1 && indexes[1] < items.Length)
                {
                    indexes[0] = indexes[1] - 2;
                    indexes[2] = indexes[1] + 1;

                    tempArray[0] = temp[indexes[0]];
                    tempArray[1] = temp[indexes[1]];
                    tempArray[2] = temp[indexes[2]];
                }                
            }

            return tempArray;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MyCollection<string> collection = new MyCollection<string>() { "1", "2", "3", "4" };

            Console.WriteLine("Первый и последний элементы:");
            foreach (string item in collection.GetFirstAndLast())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nУдаление середины:");
            foreach (string item in collection.CutMiddle())
            {
                Console.WriteLine(item);
            }

            Predicate<MyCollection<string>> predicate = IsValid;
            Console.WriteLine("\nБлижайшие по условию");
            foreach (string item in collection.GetClosest("3", predicate.Invoke(collection)))
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        private static bool IsValid(MyCollection<string> items)
        {            
            return items.Count() > 3;
        }
    }
    
}
