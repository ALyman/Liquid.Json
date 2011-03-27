using System;
using System.Collections.Generic;
using System.Linq;

namespace Liquid.Json.Tests
{
    internal class EmptyObject_Class { }

    internal class ObjectWithProperties_Class : IEquatable<ObjectWithProperties_Class>,
                                                IComparable<ObjectWithProperties_Class>
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        #region IComparable<ObjectWithProperties_Class> Members

        public int CompareTo(ObjectWithProperties_Class other)
        {
            if (A != other.A) {
                return A.CompareTo(other.A);
            } else if (B != other.B) {
                return B.CompareTo(other.B);
            } else {
                return C.CompareTo(other.C);
            }
        }

        #endregion

        #region IEquatable<ObjectWithProperties_Class> Members

        public bool Equals(ObjectWithProperties_Class other)
        {
            return A == other.A
                   && B == other.B
                   && C == other.C;
        }

        #endregion
    }

    internal class ObjectWithCaseInsensitiveProperties_Class : IEquatable<ObjectWithCaseInsensitiveProperties_Class>,
                                                               IComparable<ObjectWithCaseInsensitiveProperties_Class>
    {
        public int ABC { get; set; }
        public int abc { get; set; }

        #region IComparable<ObjectWithCaseInsensitiveProperties_Class> Members

        public int CompareTo(ObjectWithCaseInsensitiveProperties_Class other)
        {
            if (ABC != other.ABC) {
                return ABC.CompareTo(other.ABC);
            } else {
                return abc.CompareTo(other.abc);
            }
        }

        #endregion

        #region IEquatable<ObjectWithCaseInsensitiveProperties_Class> Members

        public bool Equals(ObjectWithCaseInsensitiveProperties_Class other)
        {
            return ABC == other.ABC
                   && abc == other.abc;
        }

        #endregion
    }

    internal class ObjectWithFields_Class
    {
        public int A;
        public int B;
        public int C;
    }

    internal class ObjectWithNullable_Class
    {
        public int? A = null;
    }

    internal class ObjectWithDate_Class
    {
        public DateTime A;
    }

    internal class ObjectWithNullReadOnlyChild_Class
    {
        public readonly ObjectWithFields_Class A = null;
    }

    internal class ObjectWithReadOnlyChild_Class
    {
        public readonly ObjectWithFields_Class A = new ObjectWithFields_Class();
    }

    internal class ObjectWithProperties_And_Fields_Class
    {
        public int C;
        public int A { get; set; }
        public int B { get; set; }

        public void X() { }
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

    internal class ObjectWithArrays_Class : IEquatable<ObjectWithArrays_Class>
    {
        public int[] A { get; set; }
        public int[] B { get; set; }

        #region IEquatable<ObjectWithArrays_Class> Members

        public bool Equals(ObjectWithArrays_Class other)
        {
            return A.SequenceEqual(other.A)
                   && B.SequenceEqual(other.B);
        }

        #endregion
    }

    internal class ObjectWithLists_Class : IEquatable<ObjectWithLists_Class>
    {
        public ObjectWithLists_Class()
        {
            this.A = new List<int>();
            this.B = new List<int>();
        }
        public List<int> A { get; private set; }
        public List<int> B { get; private set; }

        #region IEquatable<ObjectWithLists_Class> Members

        public bool Equals(ObjectWithLists_Class other)
        {
            return A.SequenceEqual(other.A)
                   && B.SequenceEqual(other.B);
        }

        #endregion
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