using System;
using System.Collections.Generic;
using System.Text;

namespace AccountSystem
{
    
    internal class ASCommon
    {

        //  Utility class (common) is a class that contains commonly used methods or properties
        //  that can be accessed by other classes or objects in the program. 
        internal string GetPreviousMonth(string currentMonth)
        {
            int month = GetMonth(currentMonth);
            int year = Convert.ToInt32(currentMonth.Substring(currentMonth.IndexOf("/")+1));
            if(month ==1) 
            {
                month = 12;
                year--; 
            }
            else
            {
                month--;
            }
            return GetMonthName(month) + "/" + year.ToString();
        }
        internal string GetMonthName(int month)
        {
            string[] monthName = new string[12] {"Jan", "Feb", "Mar", "Apr", "May", " Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
            return monthName[month-1];
        }
        internal int GetMonth(string monthName)
        {            
            return (DateTime.Parse("1/" + monthName).Month);
        }
        
    }
}
