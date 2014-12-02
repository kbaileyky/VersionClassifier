using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HistoryClassifier;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        List<HistoryEntry> Entries1 = new List<HistoryEntry>();
        List<HistoryEntry> Entries2 = new List<HistoryEntry>();

        public Form1()
        {
            InitializeComponent();
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

        private void button3_Click(object sender, EventArgs e)
        {
            List<List<HistoryEntry>> diffList;
            List<Splist_Entry> splitlist = new List<Splist_Entry>();
            string filename = String.Empty;

            Entries1.Clear();
            Entries2.Clear();
            Read_Comp_File(textBox1.Text, Entries1);
            Read_Comp_File(textBox2.Text, Entries2);
            try
            {
                diffList = RunComparison(Entries1, Entries2, splitlist);
                if (Get_Save_File(ref filename))
                {
                    Write_Difflist(diffList, filename, splitlist, Entries1, Entries2);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        private bool Get_Save_File(ref string filename)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
                return true;
            }
            else
            {
                return false;
            }

        }

        private string Calc_Stats(List<HistoryEntry> Entries1, List<HistoryEntry> Entries2, List<List<HistoryEntry>> difflist, List<Splist_Entry> splitlist)
        {
            int entrycount1 = Entries1.Count;
            int diffcount = difflist.Count;

            int splitCount = 0;
            foreach (Splist_Entry se in splitlist)
            {
                splitCount = splitCount + se.get_List1().Count;
            }

            entrycount1 = entrycount1 - splitCount;

            splitCount = splitlist.Count;

            int totalentries = entrycount1 + splitCount;

            diffcount = diffcount + splitCount;

            double percent = (((double)totalentries - (double)diffcount) / (double)totalentries) * 100.0;

            return "Percent Agreement:\t" + percent.ToString() + "\t Total Classified:\t" + totalentries.ToString() + "\t Number Different:\t" + diffcount.ToString() + "\tTotal Split:\t" + splitCount.ToString();

        }

        private void Write_Difflist(List<List<HistoryEntry>> difflist, string filename, List<Splist_Entry> splitlist, List<HistoryEntry> Entries1, List<HistoryEntry> Entries2){
            TextWriter txtWriter;
           
                try
                {

                    String VersionString = String.Empty;
                    String tempString = String.Empty;
                    txtWriter = new StreamWriter(filename);
                   
                    using (txtWriter)
                    {
                        txtWriter.WriteLine(Calc_Stats(Entries1, Entries2, difflist, splitlist));
                        foreach (List<HistoryEntry> v in difflist)
                        {
                            txtWriter.WriteLine(textBox1.Text);
                            txtWriter.WriteLine(JsonHelper.FormatJson(JsonConvert.SerializeObject(v[0])));
                            txtWriter.WriteLine("");

                            txtWriter.WriteLine(textBox2.Text);
                            txtWriter.WriteLine(JsonHelper.FormatJson(JsonConvert.SerializeObject(v[1])));
                        }

                        if (splitlist.Count > 0)
                        {
                            txtWriter.WriteLine("===========================Splits===========================");

                            foreach (Splist_Entry se in splitlist)
                            {
                                txtWriter.WriteLine(textBox1.Text);
                                foreach (HistoryEntry h in se.get_List1())
                                {
                                    txtWriter.WriteLine(JsonHelper.FormatJson(JsonConvert.SerializeObject(h)));
                                    txtWriter.WriteLine("");
                                }
                                txtWriter.WriteLine("");
                                txtWriter.WriteLine(textBox2.Text);
                                foreach (HistoryEntry h in se.get_List2())
                                {
                                    txtWriter.WriteLine(JsonHelper.FormatJson(JsonConvert.SerializeObject(h)));
                                    txtWriter.WriteLine("");
                                }

                            }
                        }

                    }
                    txtWriter.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
        }

        private List<List<HistoryEntry>> RunComparison(List<HistoryEntry> ls1, List<HistoryEntry> ls2, List<Splist_Entry> splitlist){
            int i = 0;
            int j = 0;

            List<List<HistoryEntry>> ret = new List<List<HistoryEntry>>();


            while (i < ls1.Count && j < ls2.Count)
            {
                if (!(ls1[i].Equals(ls2[j]))){

                    if(ls1[i].OriginallyMerged(ls2[j])){

                        splitlist.Add(Create_Split_List(ls1, ls2, ref i, ref j));

                    } else {
                        throw new FormatException("There is a missing entry! Cannot compare " + textBox1.Text + " " + ls1[i].ApplicationName + " " +  ls1[i].VersionNumber + "at Line " + i + " and" +  textBox2.Text + " " + ls2[j].ApplicationName + " " + ls2[j].VersionNumber + "at Line " + j);
                    }

                } else {

                    if (ls1[i].Get_Classification() != ls2[j].Get_Classification())
                    {
                        List<HistoryEntry> diffEntries = new List<HistoryEntry>();

                        diffEntries.Add(ls1[i]);
                        diffEntries.Add(ls2[j]);

                        ret.Add(diffEntries);
                    }
                    i = i + 1;
                    j = j + 1;
                }


            }

            return ret;
        }

        private Splist_Entry Create_Split_List(List<HistoryEntry> ls1, List<HistoryEntry> ls2, ref int i, ref int j)
        {
            
            Splist_Entry ret = new Splist_Entry(textBox1.Text, textBox2.Text);

            while (i < ls1.Count && ls1[i].OriginallyMerged(ls2[j]))
            {
                ret.Add_To_List1(ls1[i]);
                i = i + 1;
            }

            while (j < ls2.Count && ls1[i-1].OriginallyMerged(ls2[j]))
            {
                ret.Add_To_List2(ls2[j]);
                j = j + 1;
            }

            ret.Purge_Lists();
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = String.Empty;
            Select_File(ref filename);
            textBox1.Text = filename;
        }

        private bool Select_File(ref string fn)
        {
            OpenFileDialog nextDialog = new OpenFileDialog();

            nextDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            nextDialog.FilterIndex = 2;
            nextDialog.RestoreDirectory = true;

            if (nextDialog.ShowDialog() == DialogResult.OK)
            {
                fn = nextDialog.FileName;
                return true;
            }
            else
            {
                fn = string.Empty;
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = String.Empty;
            Select_File(ref filename);
            textBox2.Text = filename;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            List<List<HistoryEntry>> diffList;
            List<Splist_Entry> splitlist = new List<Splist_Entry>();
            string filename = String.Empty;
            string MobilePath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Mobile";
            string DesktopPath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Desktop";
            string SiblingPath = "C:\\Users\\Kitsune\\Documents\\GitHub\\VersionClassifier\\Data\\json\\VersionJson\\Both";

            Entries1.Clear();
          //  Entries2.Clear();
            Read_Comp_File(textBox1.Text, Entries1);
          //  Read_Comp_File(textBox2.Text, Entries2);
            try
            {
                foreach (HistoryEntry h in Entries1)
                {
                    Console.Out.WriteLine(h.ApplicationName + " " + h.VersionNumber);
                }
                Console.Out.WriteLine("\n\nSorted\n\n");
                Entries1.Sort();
                foreach (HistoryEntry h in Entries1)
                {
                    Console.Out.WriteLine(h.ApplicationName+ " " + h.VersionNumber);
                }

                PutBack(Entries1, MobilePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        private void InsertNewlyClassified(List<HistoryEntry> entrylist, List<ReleaseContainer> Releases)
        {
              for(int i = 0; i < entrylist.Count; i++)
                    {
                        HistoryEntry e = entrylist[i];
                        if(e.ApplicationName.Equals(Releases[0].ApplicationName)){

                            foreach(ReleaseContainer rc in Releases){

                                if(e.VersionNumber.Equals(rc.VersionNumber)){

                                    for(int j = 0; j < rc.EntryList.Count; j ++){
                                        HistoryEntry he = rc.EntryList[j];
                                        if(he.Equals(e)){
                                            he.Set_Classification(e.classification);
                                        } else if(he.OriginallyMerged(e)){

                                           if(he.entry.Equals(he.original_text)){
                                               he.Set_Entry(e.entry);
                                               he.Set_Classification(e.classification);
                                               he.Set_Split(true);
                                           } else {
                                               rc.EntryList.Insert(rc.EntryList.IndexOf(he)+1, e);
                                           }

                                        }
                                    }

                                }

                            }

                        }
                    }
        }

        private void PutBack(List<HistoryEntry> entrylist, string Path)
        {
           

            string[] filePaths = Directory.GetFiles(Path);


            List<ReleaseContainer> Releases = new List<ReleaseContainer>();
            foreach (string s in filePaths)
            {
                if (!(s.Contains(".txt")))
                {
                    Console.WriteLine(s);
                    Releases = Read_Our_File(s);
                    InsertNewlyClassified(entrylist, Releases);


                   Save_By_Version(Releases, s);
                }

            }

        }

        private List<ReleaseContainer> Read_Our_File(string filename)
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


        private void Save_By_Version(List<ReleaseContainer> ReleaseList, String Filename)
        {

            StreamWriter txtWriter;
       
                try
                {

                    String VersionString = String.Empty;
                    String tempString = String.Empty;
                    txtWriter = new StreamWriter(Filename);

                    using (txtWriter)
                    {

                        foreach (ReleaseContainer v in ReleaseList)
                        {
                            txtWriter.WriteLine(JsonConvert.SerializeObject(v));
                        }
                    }
                    txtWriter.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }



            
        }

    }

}
