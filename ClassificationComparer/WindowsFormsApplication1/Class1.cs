using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryClassifier;

namespace WindowsFormsApplication1
{
    class Splist_Entry
    {
        List<HistoryEntry> ls1;
        List<HistoryEntry> ls2;
        string source1 = String.Empty;
        string source2 = String.Empty;

        public Splist_Entry(string s1, string s2)
        {
            source1 = s1;
            source2 = s2;

            ls1 = new List<HistoryEntry>();
            ls2 = new List<HistoryEntry>();
        }

        public Splist_Entry(string s1, string s2, List<HistoryEntry> l1, List<HistoryEntry> l2)
        {
            source1 = s1;
            source2 = s2;

            ls1 = l1;
            ls2 = l2;
        }

        public List<HistoryEntry> get_List1()
        {
            return ls1;
        }

        public List<HistoryEntry> get_List2()
        {
            return ls2;
        }

        public void Add_To_List1(HistoryEntry entry){
            ls1.Add(entry);
        }

        public void Add_To_List2(HistoryEntry entry)
        {
            ls2.Add(entry);
        }


        public string get_Source1()
        {
            return source1;
        }

        public string get_Source2()
        {
            return source2;
        }

        public void Purge_Lists()
        {
            List<HistoryEntry> removeList1 = new List<HistoryEntry>();
            List<HistoryEntry> removeList2 = new List<HistoryEntry>();

            foreach( HistoryEntry h in ls1){
                foreach(HistoryEntry e in ls2){
                    if(h.Equals(e) && h.Get_Classification() == e.Get_Classification()){
                        removeList1.Add(h);
                        removeList2.Add(e);
                    }
                }
             }

            foreach (HistoryEntry e in removeList1)
            {
                if (ls1.Contains(e))
                {
                    ls1.Remove(e);
                }
            }
            foreach (HistoryEntry e in removeList1)
            {
                if (ls2.Contains(e))
                {
                    ls2.Remove(e);
                }
            }

        }


    }
}
