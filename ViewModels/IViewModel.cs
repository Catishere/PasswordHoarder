﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHoarder.Stores;

namespace PasswordHoarder.ViewModels
{
    public interface IViewModel
    {
        IViewModel CurrentViewModel { get; }
    }
}
