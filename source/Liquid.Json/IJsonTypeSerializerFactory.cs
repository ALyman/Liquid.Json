using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    /// <summary>
    /// Creates JSON type serializers
    /// </summary>
    public interface IJsonTypeSerializerFactory {
        /// <summary>
        /// Creates a serializer for the specified type
        /// </summary>
        /// <typeparam name="T">The type to be serialized</typeparam>
        /// <param name="serializer">The root serializer</param>
        /// <returns>The type serializer</returns>
        IJsonTypeSerializer<T> CreateSerializer<T>(JsonSerializer serializer);
    }
}
