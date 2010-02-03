using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json {
    public interface IJsonTypeSerializer<T> {
        void Serialize(T @object, JsonSerializationContext context);
        T Deserialize(JsonDeserializationContext context);
    }

    public interface IJsonTypeInplaceSerializer<T>
        : IJsonTypeSerializer<T> {
        void DeserializeInto(ref T @object, JsonDeserializationContext context);
    }
}
