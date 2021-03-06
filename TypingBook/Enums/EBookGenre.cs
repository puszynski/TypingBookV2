﻿using System;

namespace TypingBook.Enums
{
    [Flags]
    public enum EBookGenre
    { 
        All = 0,
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
        Article = 1024,
        FairyTales = 2048,
    }        
}
