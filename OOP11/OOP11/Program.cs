using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP11
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Exit = "exit";
            const string AddCommand = "add";
            const string DeleteCommand = "delete";
            const string ShowCommand = "show";

            Aquarium aquarium = new Aquarium();
            bool isWorking = true;

            while (isWorking)
            {
                aquarium.CheakFishDeath();

                Console.WriteLine($"Введите команду; команды:\nДобавить рыбу - {AddCommand}\nУбрать рыбу - {DeleteCommand}\nПоказать все рыбы - {ShowCommand}\nВыход из программы - {Exit}\nДля пропуска - ENTER");
                string input = Console.ReadLine();

                switch (input)
                {
                    case AddCommand:
                        aquarium.AddFish();
                        break;
                    case DeleteCommand:
                        aquarium.DeleteFish();
                        break;
                    case ShowCommand:
                        aquarium.ShowAllFish();
                        break;
                    case Exit:
                        Console.WriteLine("Программа завершена");

                        isWorking = false;
                        break;
                }

                aquarium.AgingFish();

                if (input != "")
                    ClearConsole();
                else
                    Console.Clear();
            }
        }

        private static void ClearConsole()
        {
            Console.WriteLine("Для продолжения нажмите ENTER");
            Console.ReadKey();
            Console.Clear();
        }
    }

    class Aquarium
    {
        const int MaxCountFish = 4;
        const int MaxAgeFish = 24;

        private List<Fish> _fishs = new List<Fish>();

        public void AddFish()
        {
            if (_fishs.Count == MaxCountFish)
                Console.WriteLine("Бассейн полон");
            else
            {
                Console.WriteLine("Введите имя рыбы");
                string name = Console.ReadLine();
                Console.WriteLine("Введите цвет рыбы");
                string color = Console.ReadLine();
                Console.WriteLine("Введите возраст рыбы (в месяцах)");
                int age = Convert.ToInt32(Console.ReadLine());

                if (age > MaxAgeFish)
                {
                    Console.WriteLine("Рыба слишком старая (не более 24 месяцев)");
                }
                else if(age < 0)
                {
                    Console.WriteLine("Неправильный возраст");
                }
                else
                {
                    _fishs.Add(new Fish(age, name, color));

                    Console.WriteLine($"Рыба {name} добавлена");
                }
            }
        }

        public void DeleteFish()
        {
            bool isFishReal = false;
            int index = 0;

            Console.WriteLine("Введите имя рыбы для удаления");
            string name = Console.ReadLine().ToLower();

            for (int i = 0; i < _fishs.Count; i++)
            {
                if (_fishs[i].Name.ToLower() == name)
                {
                    index = i;
                    isFishReal = true;
                }
            }

            if (isFishReal == false)
            {
                Console.WriteLine("Такой рыбы нет");
            }
            else
            {
                Console.WriteLine("Рыба - " + _fishs[index].Name + " удалена");
                _fishs.RemoveAt(index);
            }
        }

        public void ShowAllFish()
        {
            if (_fishs.Count == 0)
            {
                Console.WriteLine("Рыб нет");
            }
            else
            {
                foreach(var fish in _fishs) 
                    fish.ShowInfo(MaxAgeFish);
            }
        }

        public void CheakFishDeath()
        {
            for (int i = 0; i < _fishs.Count; i++)
            {
                if (_fishs[i].Age > MaxAgeFish && _fishs[i].FishAlive)
                {
                    Console.WriteLine("-------------Рыба " + _fishs[i].Name + " умерла-------------\n\n");
                    _fishs[i].FishDeath();
                }
            }
        }

        public void AgingFish()
        {
            foreach (var fish in _fishs)
                fish.Aging();
        }
    }

    class Fish
    {
        public int Age { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public bool FishAlive { get; private set; }

        public Fish(int age, string name, string color)
        {
            Age = age;
            Name = name;
            Color = color;
            FishAlive = true;
        }

        public void Aging()
        {
            Age += 1;
        }

        public void ShowInfo(int maxAgeFish)
        {
            if(Age > maxAgeFish)
            {
                Console.WriteLine($"Рыба {Name} мертва");
                FishAlive = false;
            }
            else
            {
                Console.WriteLine($"{Name} - {Color} - {Age}м.");
            }
        }

        public void FishDeath()
        {
            FishAlive = false;
        }
    }
}
