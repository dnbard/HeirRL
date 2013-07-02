﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirRL.Source
{
    public class Singleton<T> where T : class, new()
    {
        private Singleton() { }

        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance { get { return _instance.Value; } }
    }
}
