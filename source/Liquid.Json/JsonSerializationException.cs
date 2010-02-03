using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    [Serializable]
    public sealed class JsonSerializationException : Exception {
        public JsonSerializationException(string message)
            : base(message) { }
    }
}
