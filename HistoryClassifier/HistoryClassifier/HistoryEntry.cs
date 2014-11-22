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

        public string VersionNumber;
        public string ReleaseDate;

        public string ApplicationName;

        public AppType ApplicationType;

        public string[] datePattern;

        public bool flag = false;

        public bool split = false;
        public bool merged = false;
        public string original_text = String.Empty;
       
        public HistoryEntry()
        {
            entry = String.Empty;
            classification = new NotClassified();

            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            datePattern = new string[] {String.Empty};
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;

        }

        public HistoryEntry(string ent)
        {
            entry = ent;
            original_text = ent;
            classification = new NotClassified();

            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            datePattern = new string[] { String.Empty };
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;
        }

        public HistoryEntry(string ent, Classification clss)
        {
            entry = ent;
            classification = clss;
            original_text = ent;

            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            datePattern = new string[] { String.Empty };
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;
        }

        public HistoryEntry(string ent, Classification clss, string name)
        {
            entry = ent;
            classification = clss;
            ApplicationName = name;
            original_text = ent;

            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            datePattern = new string[] {String.Empty};
            ApplicationType = new AppNotClassified();
            ApplicationName = name;
        }

        public HistoryEntry(string ent, Classification clss, string name,string number)
        {
            entry = ent;
            classification = clss;
            ApplicationName = name;
            VersionNumber = number;
            original_text = ent; 

            ReleaseDate = String.Empty;
            datePattern = new string[] { String.Empty };
            ApplicationType = new AppNotClassified();
            ApplicationName = name;
        }

        public HistoryEntry(string ent, Classification clss, string name, string number, string date, string[] pat)
        {
            entry = ent;
            classification = clss;
            ApplicationName = name;
            VersionNumber = number;
            ReleaseDate = date;
            original_text = ent;
            datePattern = pat;

            ApplicationType = new AppNotClassified();
            ApplicationName = name;
        }

        public HistoryEntry(string ent, Classification clss, string name, string number, string date, string[] pat, AppType tpe)
        {
            entry = ent;
            classification = clss;
            ApplicationName = name;
            VersionNumber = number;
            ReleaseDate = date;
            ApplicationType = tpe;
            original_text = ent;
            datePattern = pat;

            ApplicationName = name;

        }




        public void Set_App_Type(AppType newclass)
        {
            ApplicationType = newclass;
        }

        public string Get_App_Type_Str()
        {
            return ApplicationType.Get_String();
        }

        public int Get_App_Type_Int()
        {
            return ApplicationType.Get_Int();
        }

        public void Set_Date(string newDate)
        {
            ReleaseDate = newDate;
        }


        public void Set_Date_Pattern(string[] newPattern)
        {
            datePattern = newPattern;
        }

        public void Set_Flag(bool flg)
        {
            flag = flg;
        }

        public bool Get_Flag()
        {
            return flag;
        }

        public void Set_Name(string newname)
        {
            ApplicationName = newname;
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

        public void Set_Merged(bool TF)
        {
            merged = TF;
            CheckOriginal();
        }

        public void Set_Split(bool Tf)
        {
            split = Tf;
            CheckOriginal();
        }

        public void Set_Original_Text(string text)
        {
            original_text = text;
        }

        private void CheckOriginal()
        {
            if(original_text.Equals(entry)){
                split = false;
                merged = false;
            }
        }
    }
}
