﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HeirRL.Source
{
    public struct RectangleName
    {
        public Rectangle Rect;

        public long Hash
        {
            private set;
            get;
        }

        public string Name
        {
            set { Hash = value.GetHashCode(); }
        }
    }
}
