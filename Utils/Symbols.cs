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
            Alphabet,
            Number,
            Special,
        }

        public static readonly string[] SymbolArray = {
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
            "0123456789",
            "!@#$%^&*()_+=-,./;:'\"|{}[]~`"
        };
    }
}
