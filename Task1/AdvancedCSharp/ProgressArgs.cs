using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCSharp
{
    public class ProgressArgs : EventArgs
    {
        public bool IsSearchActive { get; set; } = true;
        public string Message { get; set; }
    }
}
