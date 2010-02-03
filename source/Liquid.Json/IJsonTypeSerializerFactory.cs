using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    public interface IJsonTypeSerializerFactory {
        IJsonTypeSerializer<T> CreateSerializer<T>(JsonSerializer serializer);
    }
}
