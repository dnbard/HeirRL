using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeirRL.Graphics;

namespace HeirRL.Events
{
    public class ActionBase
    {
        public VisualComponent Object { get; set; }

        public ActionDelegate Action { get; set; }

        public long RaiseTime { get; set; }

        /// <summary>
        /// Raised when level.time >= RaiseTime. 
        /// </summary>
        /// <returns>Return true when Action must be disposed</returns>
        public virtual bool OnTimeExcess()
        {
            return true;
        }
    }
}
