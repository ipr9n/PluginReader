
using System;

namespace Person.Interfaces
{
    interface IInfoReadable
    {
        void GetBiography();

        int age { get; }

        string name { get; }

        DateTime BirthDate { get; }
    } 
}
