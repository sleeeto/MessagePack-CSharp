using System;
using MessagePack;

namespace SourceGeneratorTest
{
    public class Program
    {
        private static void Main(string[] args)
        {

        }
    }

    [MessagePackObject(true)]
    public partial struct Foo
    {
        public int MyProperty2 { get; set; }

    }
}
