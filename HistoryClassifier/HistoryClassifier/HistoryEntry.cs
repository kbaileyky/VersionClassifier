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
        public desc classification; 

       
        public HistoryEntry()
        {
            entry = String.Empty;
            classification = desc.NotClassified;
        }

        public HistoryEntry(string ent)
        {
            entry = ent;
            classification = desc.NotClassified;
        }

        public HistoryEntry(string ent, desc clss)
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
            switch (classification)
            {
                case desc.Bug:
                    return "Bug";
                case desc.Enhancement:
                    return "Enhancement";
                case desc.Feature:
                    return "Feature";
                case desc.Junk:
                    return "Non-functional";
                case desc.Ads:
                    return "Advertisement";
                case desc.RevChangeRequest:
                    return "RatingReponse";
                case desc.NotClassified:
                    return "NotClassified";
                default:
                    throw new Exception("Invalid Classification (enum) value in class, something has gone horribly wrong");
            }
        }

        public int Get_Classification()
        {
            switch (classification)
            {
                case desc.Bug:
                    return 0;
                case desc.Enhancement:
                    return 2;
                case desc.Feature:
                    return 1;
                case desc.Junk:
                    return 3;
                case desc.Ads:
                    return 4;
                case desc.RevChangeRequest:
                    return 5;
                case desc.NotClassified:
                    return 6;
                default:
                    throw new Exception("Invalid Classification (enum) value in class, something has gone horribly wrong");
            }
        }

        public void Set_Classification(desc newclass)
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
