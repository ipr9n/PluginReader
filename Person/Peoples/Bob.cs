using System;
using Person.Interfaces;

namespace Person.Peoples
{
    class Bob : IInfoReadable
    {
        public void GetBiography()
        {
            Console.WriteLine($"Name: {name}\n" +
                              $"Age: {age}\n" +
                              $"BirthDate: {BirthDate}");

        }

        public int age { get; } = new Random().Next(15, 30);
        public string name { get; } = "Bob";
        public DateTime BirthDate { get; } = new DateTime(2015, 7, 20);
    }
}