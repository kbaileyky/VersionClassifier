using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HistoryClassifier
{
    class ReleaseContainer
    {
        public string VersionNumber;
        public string ReleaseDate;
        public string ReleaseContents;

        public string ApplicationName;

        public AppType ApplicationType;

        private string[] datePattern;

        public bool flag = false;

        private CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
            
        public List<HistoryEntry> EntryList;

        public ReleaseContainer()
        {
            VersionNumber = String.Empty;
            ReleaseDate = String.Empty;
            ReleaseContents = String.Empty;
            datePattern = new string[] {String.Empty};
            ApplicationType = new AppNotClassified();
            ApplicationName = String.Empty;

            EntryList = new List<HistoryEntry>();
            return;
        }

        public ReleaseContainer(string[] pat)
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


        public ReleaseContainer(string[] pat, string number)
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
            foreach (HistoryEntry el in EntryList)
            {
                el.Set_App_Type(newclass);
            }
        }

        public void Set_App_Name(string newName)
        {
            ApplicationName = newName;
            foreach (HistoryEntry el in EntryList)
            {
                el.Set_Name(newName);
            }
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
            ReleaseDate = Convert_Date(newDate);
            foreach (HistoryEntry h in EntryList)
            {
                h.Set_Date(ReleaseDate);
            }
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

        public void Set_Flag(bool flg, int index)
        {
            flag = flg;
            EntryList[index].Set_Flag(flg);
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
                    EntryList.Add(new HistoryEntry(str, new NotClassified(), ApplicationName, VersionNumber, ReleaseDate, datePattern, ApplicationType));
 
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
            EntryList[index].Set_Split(true);
            EntryList[index + 1].Set_Split(true);
            return;
        }

        public void Merge_Entry(int index, int index2) //Appends Second to the first entry
        {
            string newEntry = EntryList[index].Get_Entry();
            newEntry += EntryList[index2].Get_Entry();
            EntryList[index].Set_Entry(newEntry);

            EntryList.RemoveAt(index2);
            EntryList[index].Set_Merged(true);
            return;
        }

        public void classify_Entry(int index, Classification type)
        {
            if (index >= 0)
            {
                EntryList[index].Set_Classification(type);
            }
        }

        public string Convert_Date(string oldDate)
        {



              DateTime date;
              if (DateTime.TryParseExact(oldDate, datePattern, enUS, DateTimeStyles.None, out date))
              {
                  ReleaseDate = date.ToString("dd/MM/yyyy");
                  return ReleaseDate;
              }
              else
              {

                  return oldDate;
              }
           

        }

    }
}
