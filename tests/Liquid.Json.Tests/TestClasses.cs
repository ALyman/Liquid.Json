using System;

namespace Liquid.Json.Tests
{
    internal class EmptyObject_Class {}

    internal class ObjectWithProperties_Class
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }

    internal class ObjectWithFields_Class
    {
        public int A;
        public int B;
        public int C;
    }

    internal class ObjectWithProperties_And_Fields_Class
    {
        public int C;
        public int A { get; set; }
        public int B { get; set; }
    }

    internal class RespectsIgnoreAttributeOnProperties_Class
    {
        public int A { get; set; }
        public int B { get; set; }

        [JsonIgnore]
        public int C { get; set; }
    }

    internal class RespectsIgnoreAttributeOnFields_Class
    {
        public int A;
        public int B;

        [JsonIgnore]
        public int C;
    }

    internal class ObjectWithArrays_Class
    {
        public int[] A { get; set; }
        public int[] B { get; set; }
    }

    internal class CyclicalObject_Class
    {
        public CyclicalObject_Class A { get; set; }
        public CyclicalObject_Class2 B { get; set; }
    }

    internal class CyclicalObject_Class2
    {
        public CyclicalObject_Class A { get; set; }
    }
}