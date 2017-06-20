using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurosport.Kodakademi.Pointer
{
    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public List<Person> Children { get; private set; }

        public Person()
        {
            this.Children = new List<Person>();
        }

        public Person(Person other) : this()
        {
            this.Name = other.Name;
            this.Age = other.Age;
            this.Children.AddRange(other.Children.Select(otherChild => new Person(otherChild)));
        }
    }

    internal static class Program
    {
        private static void Main()
        {
            //DiffCreateTwoPersons();
            //ChangeAddress();
            //CopyList();
            //AddChild();
            //AddChild();
            //AddChildInPlace();
            //ChangeName();
            //Bonus();

            Console.ReadLine();
        }

        #region AddChild

        private static void AddChild()
        {
            Person parent = new Person() { Name = "Parent" };
            Console.WriteLine("parent adress is :");
            GetAddress(parent);
            parent = AddChild(parent);
            Console.WriteLine("parent adress is :");
            GetAddress(parent);
        }

        
        private static void AddChildInPlace()
        {
            Person parent = new Person() { Name = "Parent" };
            Console.WriteLine("parent adress is :");
            GetAddress(parent);
            Console.WriteLine("parent children are :" + GetListContent(parent.Children));
            AddChildInPlace(parent);
            Console.WriteLine("parent adress is :");
            GetAddress(parent);
            Console.WriteLine("parent children are :" + GetListContent(parent.Children));
        }
        
        private static Person AddChild(Person person)
        {
            person.Children.Add(new Person() { Name = "Little Kid" });
            return person;
        }

        private static void AddChildInPlace(Person person)
        {
            person.Children.Add(new Person() { Name = "Little Kid" });
        }

        #endregion

        #region ChangeName

        private static void ChangeName()
        {
            Person john = new Person() { Name = "John" };
            Console.WriteLine("parent adress is :");
            GetAddress(john);
            Console.WriteLine("john name is :" + john.Name);
            ChangeName(john);
            Console.WriteLine("john adress is :");
            GetAddress(john);
            Console.WriteLine("john name is :" + john.Name);
            ReallyChangeName(ref john);
            Console.WriteLine("john adress is :");
            GetAddress(john);
            Console.WriteLine("john name is :" + john.Name);

        }


        private static void ChangeName(Person person)
        {
            GetAddress(person);
            person = new Person(person);
            person.Name = "Mike";
        }

        private static void ReallyChangeName(ref Person person)
        {
            GetAddress(person);
            person = new Person(person);
            person.Name = "Mike";
        }

        #endregion


        #region List

        private static List<Person> subscriberList = new List<Person>()
        {
            new Person() { Name = "John"},
            new Person() { Name = "Cassidy"},
            new Person() { Name = "Mike"}
        };

        private static void CopyList()
        {
            List<Person> l = new List<Person>(subscriberList);

            Console.WriteLine("Adress of local list :");
            GetAddress(l);
            Console.WriteLine("Adress of static list :");
            GetAddress(subscriberList);

            l[0].Name = "John changed !";
            Console.WriteLine("Content of local list :");
            Console.WriteLine(GetListContent(l));
            Console.WriteLine("Content of static list :");
            Console.WriteLine(GetListContent(subscriberList));
        }


        #endregion

        #region CreateTwoPersons

        private static void DiffCreateTwoPersons()
        {
            CreateTwoPersons();
            Console.WriteLine("=========");
            CreateTwoPersons2();
            Console.WriteLine("=========");
        }
                
        private static void CreateTwoPersons()
        {
            Person OnePerson = new Person() { Name = "Cab", Age = 64 };
            GetAddress(OnePerson);
            OnePerson.Name = "Aretha";
            OnePerson.Age = 48;
            GetAddress(OnePerson);
        }

        private static void CreateTwoPersons2()
        {
            Person OnePerson = new Person() { Name = "Cab", Age = 64 };
            GetAddress(OnePerson);
            OnePerson = new Person() { Name = "Aretha", Age = 48 };
            GetAddress(OnePerson);
        }

        #endregion

        #region Bonus

        private static void Bonus()
        {
            Person OnePerson = new Person() { Name = "Cab", Age = 64 };
            GetAddress(OnePerson);
            for (int i = 0; i < 100000; i++)
            {
                int[] ha = new int[i];
                for (int j = 0; j < i; j++)
                {
                    ha[j] = i;
                }
            }
            GC.Collect(2,GCCollectionMode.Forced, true);
            GetAddress(OnePerson);
            Console.WriteLine("=========");
        }

        #endregion

        #region Helper

        private static unsafe void GetAddress(Object o)
        {
            TypedReference tr = __makeref(o);
            IntPtr ptr = **(IntPtr**)(&tr);
            Console.WriteLine(ptr.ToInt64());
        }

        private static string GetListContent(List<Person> list)
        {
            return String.Join(";", list.Select(p => p.Name));
        }

        #endregion

    }

}
