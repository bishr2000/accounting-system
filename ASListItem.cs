using System;
 

namespace AccountSystem
{

    internal class ASListItem
    {
        private string textField;
        private object valueField;
        internal ASListItem()
        { 
            
        }
        internal ASListItem(string text, object value)
        {
            textField = text;
            valueField = value;
        }
        public string TextField
        {
            set { textField = value; }
            get { return textField; }
        }
        public object ValueField
        {
            set { valueField = value; }
            get { return valueField; }
        }
    }

}
