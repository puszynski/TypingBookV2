﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TypingBook.Enums
{
    [Flags]
    public enum BookGenre
    {        
        None = 0,
        SciFi = 1,
        Horror = 2,
        Action = 4,
        Travel = 8,
        History = 16,
        Biography = 32,
        Drama = 64,
        Documentary = 128,
        Comedy = 256,
        War = 512,
    }
}
