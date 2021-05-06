using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHoarder.Utils
{
    public static class Symbols
    {

        public enum SymbolType
        {
            Upper,
            Lower,
            Number,
            Special,
        }

        public static readonly string[] SymbolArray = {
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            "abcdefghijklmnopqrstuvwxyz",
            "0123456789",
            "!@#$%^&*()_+=-,./;:'\"|{}[]~`"
        };
    }
}
