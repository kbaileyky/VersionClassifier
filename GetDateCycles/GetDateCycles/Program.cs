using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HistoryClassifier;
using Newtonsoft.Json;
using System.Globalization;

namespace GetDateCycles
{
    class Program
    {

        static void Calc_Cycles(string Path, string outfile){
            List<List<int>> Cycles = new List<List<int>>();
               string[] filePaths = Directory.GetFiles(Path);
             List<List<ReleaseContainer>> CurrentVersions = new List<List<ReleaseContainer>>();

            List<ReleaseContainer> Releases = new List<ReleaseContainer>();
            foreach (string s in filePaths)
            {
                if (!(s.Contains(".txt")))
                {
                    Console.WriteLine(s);
                    Releases = Read_Our_File(s);
                    Cycles.Add(Calculate_Cycle_Lengths(Releases));
                    CurrentVersions.Add(Releases);
                }

            }

            Save_Cycle(Path + outfile, CurrentVersions, Cycles);

        }


        static void Main(string[] args)
        {
            List<List<int>> MobileCycles = new List<List<int>>();
            List<List<int>> DesktopCycles = new List<List<int>>();
            List<List<int>> SibilingDesktopCycles = new List<List<int>>();
            List<List<int>> MobileDesktopCycles = new List<List<int>>();

            List<List<ReleaseContainer>> CurrentVersions = new List<List<ReleaseContainer>>();

            string MobilePath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Mobile";
            string DesktopPath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Desktop";
            string SiblingPath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Both";

            //string[] filePaths = Directory.GetFiles(MobilePath);

            //List<ReleaseContainer> Releases = new List<ReleaseContainer>();
            //foreach (string s in filePaths)
            //{
            //    if (!(s.Contains(".txt")))
            //    {
            //        Console.WriteLine(s);
            //        Releases = Read_Our_File(s);
            //        MobileCycles.Add(Calculate_Cycle_Lengths(Releases));
            //        CurrentVersions.Add(Releases);
            //    }

            //}

            //Save_Cycle(MobilePath + "\\MobileCycles.txt", CurrentVersions, MobileCycles);

            //CurrentVersions.Clear();

            Calc_Cycles(MobilePath, "\\MobileCycles.txt");
            Calc_Cycles(DesktopPath, "\\DesktopCycles.txt");
            Calc_Cycles(SiblingPath, "\\SiblingCycles.txt");

            Console.WriteLine("Done");

        }


        private static void Save_Cycle(string filename, List<List<ReleaseContainer>> VersionList, List<List<int>> cycleList)
        {
            StreamWriter txtWriter;
               try
                {

                    String VersionString = String.Empty;
                    String tempString = String.Empty;
                    txtWriter = new StreamWriter(filename);

                    using (txtWriter)
                    {

                        for(int i = 0; i < VersionList.Count -1; i++)
                        {
                            //Console.Out.WriteLine("i: " + i);
                            txtWriter.Write(VersionList[i][0].ApplicationName + ":");
                            for (int j = 0; j < cycleList[i].Count-1; j ++ )
                            {
                              //  Console.Out.WriteLine("j: " + j);
                                txtWriter.Write(cycleList[i][j] + ",");
                            }
                            txtWriter.WriteLine();
                        }
                    }
                    txtWriter.Close();
                }
                catch (Exception ex)
                {

                    throw ex;

                }
            
        }

        static public List<int> Calculate_Cycle_Lengths(List<ReleaseContainer> ls)
        {
            DateTime NewDate;
            DateTime OldDate;
            CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");

            List<int> cycleLengths = new List<int>();
            ls.Reverse();
            for (int i = 0; i < ls.Count - 2; i ++ )
            {
                DateTime.TryParseExact(ls[i].ReleaseDate, "dd/MM/yyyy", enUS, DateTimeStyles.None, out NewDate);
                DateTime.TryParseExact(ls[i + 1].ReleaseDate, "dd/MM/yyyy", enUS, DateTimeStyles.None, out OldDate);

                TimeSpan ts = OldDate - NewDate;

                if (ts.Days < 0 || ts.Days > 1000)
                {
                    Console.Out.WriteLine(ts.Days);
                }

                cycleLengths.Add(ts.Days);
            }

            return cycleLengths;
        }

        static public List<ReleaseContainer> Read_Our_File(string filename)
        {
            StreamReader txtReader;
            List<ReleaseContainer> newRelease = new List<ReleaseContainer>();
            try
            {

                String VersionString = String.Empty;
                String tempString = String.Empty;
                txtReader = new StreamReader(filename);


                using (txtReader)
                {

                    while ((tempString = txtReader.ReadLine()) != null)
                    {
                        newRelease.Add(JsonConvert.DeserializeObject<ReleaseContainer>(tempString));
                    } //end reading file

                } //end using stream

                txtReader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return newRelease;

        }

    }
}
