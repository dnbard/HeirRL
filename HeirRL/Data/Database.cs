using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeirRL.Data;
using HeirRL.Source;

namespace HeirRL
{
    public class Database
    {
        public static Service Service
        {
            get { return Singleton<Service>.Instance; }
        }

        public static Textures Textures
        {
            get { return Singleton<Textures>.Instance; }
        }
    }
}
