using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json.Tests {
    class EmptyObject_Class { }
    class ObjectWithProperties_Class {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
    }
    class ObjectWithFields_Class {
        public int A;
        public int B;
        public int C;
    }
    class ObjectWithProperties_And_Fields_Class {
        public int A { get; set; }
        public int B { get; set; }
        public int C;
    }
    class RespectsIgnoreAttributeOnProperties_Class {
        public int A { get; set; }
        public int B { get; set; }
        [JsonIgnore]
        public int C { get; set; }
    }
    class RespectsIgnoreAttributeOnFields_Class {
        public int A;
        public int B;
        [JsonIgnore]
        public int C;
    }
    class ObjectWithArrays_Class {
        public int[] A { get; set; }
        public int[] B { get; set; }
    }
    class CyclicalObject_Class {
        public CyclicalObject_Class A { get; set; }
        public CyclicalObject_Class2 B { get; set; }
    }
    class CyclicalObject_Class2 {
        public CyclicalObject_Class A { get; set; }
    }
}
