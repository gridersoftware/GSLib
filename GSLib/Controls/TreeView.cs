using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSLib.Controls
{
    /// <summary>
    /// Represents a TreeView control. This control fixes the "Double-Click Bug" in the existing TreeView control."
    /// </summary>
    public class TreeView : System.Windows.Forms.TreeView
    {
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x203)
            {
                m.Result = new IntPtr(0);
                OnDoubleClick(new EventArgs());
            }
            else base.WndProc(ref m);
        }
        
    }
}
