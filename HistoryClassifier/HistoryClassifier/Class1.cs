using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryClassifier
{
    class ReleaseContainer
    {
        public string VersionNumber;
        public string ReleaseDate;
        public string ReleaseContents;

        public string ApplicationName;

        public AppType ApplicationType;

        public string datePattern;

        public bool flag = false;

        public List<HistoryEntry> EntryList;

        public ReleaseContainer()
        {
            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            ReleaseContents = String.Empty;
            datePattern = String.Empty;
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;

            EntryList = new List<HistoryEntry>();
            return;
        }

        public ReleaseContainer(string pat)
        {
            VersionNumber = String.Empty;
            ReleaseDate = String.Empty; 
            ReleaseContents = String.Empty;
            EntryList = new List<HistoryEntry>();
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;
            

            datePattern = pat;
            return;
        }


        public ReleaseContainer(string pat, string number)
        {
           ReleaseDate = String.Empty;
           ReleaseContents = String.Empty;
           EntryList = new List<HistoryEntry>();
           ApplicationType = new AppNotClassified();
           ApplicationName = String.Empty;

            VersionNumber = number;
            datePattern = pat;
            return;
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

        public void Set_Date(string newDate){
            ReleaseDate = newDate;
        }

        public void Set_Version(string newVersion){
            VersionNumber = newVersion;
        }

        public string Get_ID()
        {
            return VersionNumber;
        }

        public string Set_Contents()
        {
            return ReleaseContents;
        }

        public void Set_Contents(string newContents)
        {
            ReleaseContents = newContents;
            Process_Contents();

        }

        public void Set_Flag(bool flg)
        {
            flag = flg;
        }

        public bool Get_Flag(){
            return flag;
        }

        public string Get_Contents()
        {
           return ReleaseContents;
        }

        private void Process_Contents()
        {
            string[] brokenUp = ReleaseContents.Split('\n');
            foreach (string str in brokenUp)
            {
                if (!str.Equals(String.Empty))
                {
                    EntryList.Add(new HistoryEntry(str));
                }
            }
            return;
        }

        public List<HistoryEntry> Get_Entry_List()
        {
            return EntryList;
        }

        public void Split_Entry(int index, int SubstringPosition){
            string entry = EntryList[index].Get_Entry();
            EntryList[index].Set_Entry(entry.Substring(0, SubstringPosition));
            EntryList.Insert(index + 1, new HistoryEntry(entry.Substring(SubstringPosition)));
            return;
        }

        public void classify_Entry(int index, Classification type)
        {
            if (index >= 0)
            {
                EntryList[index].Set_Classification(type);
            }
        }

    }
}
