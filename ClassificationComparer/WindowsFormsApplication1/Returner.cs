using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HistoryClassifier;
using System.IO;
using Newtonsoft.Json;

namespace ClassificationComparer
{
    class Returner
    {

        List<HistoryEntry> compEntries;

        private bool CompAppName(HistoryEntry entry, ReleaseContainer release)
        {
            return entry.ApplicationName.Equals(release.ApplicationName);
        }

        private bool CompAppVersion(HistoryEntry entry, ReleaseContainer release)
        {
            return entry.VersionNumber.Equals(release.VersionNumber);
        }

        private List<List<HistoryEntry>> SortByApplication(List<HistoryEntry> scrambled)
        {
            List<List<HistoryEntry>> ret = new List<List<HistoryEntry>>();


            return ret;
        }

        public void putBack(List<HistoryEntry> ls)
        {


        }



        private void Read_Comp_File(string filename, List<HistoryEntry> ls)
        {
            StreamReader txtReader;
            try
            {

                String VersionString = String.Empty;
                String tempString = String.Empty;
                txtReader = new StreamReader(filename);
                HistoryEntry newEntry = new HistoryEntry();
                using (txtReader)
                {

                    while ((tempString = txtReader.ReadLine()) != null)
                    {

                        newEntry = JsonConvert.DeserializeObject<HistoryEntry>(tempString);
                        ls.Add(newEntry);

                    } //end reading file
                } //end using stream

                txtReader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
