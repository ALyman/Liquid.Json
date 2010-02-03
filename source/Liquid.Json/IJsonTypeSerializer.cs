using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Liquid.Json {
    public interface IJsonTypeSerializer<T> {
        void Serialize(T value, TextWriter writer, JsonSerializer serializer);
    }
}
