using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liquid.Json {
    public enum JsonTokenType {
        Integer,
        ObjectStart,
        ObjectEnd,
        ArrayStart,
        ArrayEnd,
        Comma,
        Real,
        True,
        False,
        New,
        Identifier,
        String,
        Colon
    }
}
