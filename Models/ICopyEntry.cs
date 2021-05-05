using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHoarder.Models
{
    interface ICopyEntry
    {
        void Copy(IPasswordEntry other);
    }
}
