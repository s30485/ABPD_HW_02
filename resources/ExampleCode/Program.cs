// // See https://aka.ms/new-console-template for more information
//
// using System.Runtime.CompilerServices;
// using System.Runtime.InteropServices;
// using System.Security.AccessControl;
//
// /*Console.WriteLine("Hello, World!");
//
// Foo instacneFoo = new();
// Foo instacneFoo2 = new Foo();
// var inctanceFoo3 = new Foo();
//
// instacneFoo.Name = "Foo";
//
// var car = new Car();
// car.Name = "Car";
// car.WheelCount = 4;
// car.EngineVolume = 1.6;
//
// Vechicle vechicle = car;
// Console.WriteLine(vechicle.ToString());
// car.GasVolume = 4;
// car.CheckGasVolume();
// IEnumerable<Vechicle> vechiclesNumerable;*/
//
// string path = "input.txt";
// string[] lines = [];
//
//
// if (File.Exists(path))
// {
//     lines = File.ReadAllLines(path);
//     foreach (var line in lines)
//     {
//         Console.WriteLine(line);
//     }
// }
// else
// {
//     throw new FileNotFoundException();
// }
//
//
// class Foo //comvemtion
// {
//     public string Name { get; set; } //public field names start with capital letters
//     
//     private string _privName; //private field names start with _
//
//     public int Age
//     {
//         get => Age; //idk what this does xd
//         set
//         {
//             if (value > 60)
//             {
//                 throw new ArgumentException("The age must be less than 60");
//             }
//
//             if (value < 18)
//             {
//                 throw new ArgumentException("The age must be greater than 18");
//             }
//         }
//         
//     }
//
//     public Foo()
//     {
//         Name = "Foo";
//         Age = 20;
//     }
//
//     public Foo(string name, int age)
//     {
//         Name = name;
//         Age = age;
//     }
//
//     public int AddAge(int value) => value + Age;
//
//     public override string ToString()
//     {
//         return string.Format("Name: {0}, Age: {1}", Name, Age);
//         //return $"My name is {Name}. My age is {Age}";
//         //return "My name is " + Name + " and I am " + Age;
//     }
// }
//
// public class Shape
// {
//     public double Width { get; set; }
//     public double Height { get; set; }
//     
//     public virtual double CalculateArea() => Width * Height;
// }
//
// public class Cube : Shape
// {
//     public double Depth { get; set; }
//
//     public override double CalculateArea() => Depth * Width * Height;
// }
//
// abstract class Vechicle
// {
//     public string Name { get; set; }
//
//     public int WheelCount { get; set; }
// }
//
// class Car : Vechicle, IGasNotification
// {
//     public double EngineVolume { get; set; }
//     public double GasVolume { get; set; }
//     public override string ToString() => $"My engine volume: {EngineVolume}";
//
//     public void Notify()
//     {
//         Console.WriteLine($"Low gas level");
//     }
//
//     public void CheckGasVolume()
//     {
//         if (GasVolume < 5)
//         {
//             Notify();
//         }
//     }
// }
//
// interface IGasNotification
// {
//     void Notify();
// }