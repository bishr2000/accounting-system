using System;
using System.Collections.Generic;
using System.Text;

namespace AccountSystem
{
    class ASEnum
    {
        internal enum Status:short
        {
            All = 2,
            Unapproval = 0,
            Approved=1
        }
        internal enum Activate : short
        {
            All = 2,
            Continue = 0,
            Discontinued = 1
        }
        internal struct Item
        {
            string nameField;
            object valueField;
        }
    }
}
