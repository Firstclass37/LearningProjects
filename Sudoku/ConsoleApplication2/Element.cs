using System.Collections.Generic;

namespace ConsoleApplication2
{
    internal class Element
    {
        public int Value { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public int Square { get; set; }

        public List<int> CanBe { get; set; }

        public List<int> CantBe { get; set; }

        public bool IsEmpty => Value == 0;
    }
}