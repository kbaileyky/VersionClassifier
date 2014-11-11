using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryClassifier
{
    class HistoryEntry
    {
        public string entry;
        public Classification classification;
       
        public HistoryEntry()
        {
            entry = String.Empty;
            classification = new NotClassified();
        }

        public HistoryEntry(string ent)
        {
            entry = ent;
            classification = new NotClassified();
        }

        public HistoryEntry(string ent, Classification clss)
        {
            entry = ent;
            classification = clss;
        }

        public string Get_Entry()
        {
            return entry;
        }

        public string Get_Classification_String()
        {
          return classification.Get_String();
        }

        public int Get_Classification()
        {
            return classification.Get_Int();
        }

        public void Set_Classification(Classification newclass)
        {
            classification = newclass;
        }

        public void Set_Entry(string newEntry)
        {
            entry = newEntry;
        }

        public string Get_ID() //returns first word of the entry for the list box
        {
            if (entry.Length > 10)
            {
                return entry.Substring(0, 10);
            }
            else
            {
                return entry;
            }
        }
    }
}
