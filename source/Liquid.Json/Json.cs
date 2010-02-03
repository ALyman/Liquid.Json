using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    public static class Json {
        public static string ToJson<T>(this T value) {
            return new JsonSerializer().Serialize(value);
        }
    }
}
