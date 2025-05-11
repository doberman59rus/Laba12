using WatchLibrary;
using System;
using LAB12;
class Program
{
    static void Main(string[] args)
    {
        //создаем список из 5 случайных часов
        Point watchList = Point.MakeList(5);
        
        Thread.Sleep(500);

        Console.WriteLine("\nСписок:");
        Point.ShowList(watchList);

        //добавляем умные часы на 3 позицию
        var smartWatch = new SmartWatch();
        smartWatch.RandomInit();
        watchList = Point.AddPoint(watchList, 3, smartWatch);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после добавления умных часов на 3 позицию:");
        Point.ShowList(watchList);

        Thread.Sleep(500);

        //удаляем часы по бренду
        Console.WriteLine("\nВведите бренд для удаления часов:");
        string brandToRemove = Console.ReadLine();
        watchList = Point.RemoveFirstByBrand(watchList, brandToRemove);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после удаления:");
        Point.ShowList(watchList);

        //добавляем 2 случайных часов в начало
        watchList = Point.AddElementsToBeginning(watchList, 2);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после добавления двух случайных часов в начало:");
        Point.ShowList(watchList);

        //клонируем список
        Point clonedList = Point.DeepCloneList(watchList);

        //очищаем оригинальный список
        Point.ClearList(ref watchList);

        Thread.Sleep(500);

        Console.WriteLine("\nОригинальный список после очищения:");
        Point.ShowList(watchList);

        Thread.Sleep(500);

        Console.WriteLine("\nКлонированный список:");
        Point.ShowList(clonedList);

        MyHashSet hashSet = new MyHashSet();

        //добавляем часы разных брендов
        hashSet.Add(new Watch("Rolex", 2020));
        hashSet.Add(new AnalogWatch("Casio", 2021, "Sport"));
        hashSet.Add(new ElectronicWatch("Samsung", 2022, "AMOLED"));
        
        //печать после добавления
        hashSet.PrintHS();
        
        //проверяем наличие бренда
        bool hasCasio = hashSet.Contains("Casio");
        Console.WriteLine($"Содержит Casio: {hasCasio}");

        //удаляем бренд
        bool removed = hashSet.Remove("Casio");
        Console.WriteLine($"Удален Casio: {removed}");

        //печать после удаления
        hashSet.PrintHS();

        //проверяем снова
        hasCasio = hashSet.Contains("Casio");
        Console.WriteLine($"Содержит Casio после удаления: {hasCasio}");
    }
}