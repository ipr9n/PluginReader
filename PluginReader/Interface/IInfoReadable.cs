using System;

namespace PluginReader.Interface
{
    interface IInfoReadable
    {
        void GetBiography();

        int agee { get; }

        string name { get; }

        DateTime BirthDate { get; }
    }
}
