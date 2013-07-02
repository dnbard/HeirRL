using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirRL.Graphics
{
    public delegate void VisualEventHandler(VisualComponent sender, VisualEventArgs e);

    public class VisualEventArgs : EventArgs
    {
        
    }
}
