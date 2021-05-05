using System.Collections.Generic;
using PasswordHoarder.Models.UI;

namespace PasswordHoarder.Models
{
    public class PasswordFileModel
    {
        public List<PasswordEntryModel> Entries { get; set; }

        public PasswordFileModel()
        {
            Entries = new List<PasswordEntryModel>();
        }
    }
}
