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
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace HistoryClassifier
{
    public partial class Form1 : Form
    {

        List<ReleaseContainer> ReleaseList = new List<ReleaseContainer>();

        private string firstVersionPattern = "\\d+\\.\\d+";
        private string SecondVersionPattern = "\\d+\\.\\d+";
        private string datePattern = "\\d{1,2}-\\d{1,2}-(\\d{4}|\\d{2})";
        private string[] dateFormat;

        //so there are pretty colors
        private List<HistoryEntry> activeHistory = new List<HistoryEntry>();
        private SolidBrush[] colors = { new SolidBrush(Color.DarkRed), new SolidBrush(Color.Blue), new SolidBrush(Color.Purple), new SolidBrush(Color.Gray), new SolidBrush(Color.Green), new SolidBrush(Color.Gold), new SolidBrush(Color.Black) };
        private int lsboxindex = 0;

        public Form1()
        {
            InitializeComponent();

            //tell windows we are interested in drawing items in ListBox on our own
            this.lsbxHistory.DrawItem += new DrawItemEventHandler(this.DrawItemHandler);

            //tell windows we are interested in providing  item size
            this.lsbxHistory.MeasureItem +=  new System.Windows.Forms.MeasureItemEventHandler(this.MeasureItemHandler);
            this.KeyPreview = true;
          // this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
        }

        private void DrawItemHandler(object sender, DrawItemEventArgs e)
        {
            if (lsbxVersions.Items.Count > 0)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                if (e.Index < 0)
                {
                    e.Graphics.DrawString(activeHistory[lsboxindex].Get_ID(),
                                           Control.DefaultFont,
                                        colors[activeHistory[lsboxindex].Get_Classification()],
                                         e.Bounds);
                }
                else
                {
                    e.Graphics.DrawString(activeHistory[e.Index].Get_ID(),
                                            Control.DefaultFont,
                                         colors[activeHistory[e.Index].Get_Classification()],
                                          e.Bounds);
                }
            }
        }

        private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 22;
        }

        private bool Select_File(ref string fn)
        {
            OpenFileDialog nextDialog = new OpenFileDialog();

             nextDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*" ;
             nextDialog.FilterIndex = 2 ;
             nextDialog.RestoreDirectory = true ;

             if (nextDialog.ShowDialog() == DialogResult.OK)
             {
                 fn = nextDialog.FileName;
                 return true;
             } else {
                 fn = string.Empty;
                 return false;
             }
        }

       
        private void Read_Release_History_File(string filename){
          StreamReader txtReader;
            
            try{

                 String VersionString = String.Empty;
                     String tempString = String.Empty;
                     txtReader = new StreamReader(filename);
                     ReleaseContainer newRelease = new ReleaseContainer();

                     using(txtReader){

                        while((tempString = txtReader.ReadLine()) != null){
                            bool skip = false;

                            if(Regex.IsMatch(tempString, "\\S")){ //Not null or space


                                if(Regex.IsMatch(tempString, firstVersionPattern)){ //If this is the start of a new version's release notes
                                        
                                        newRelease.Set_Contents(VersionString);
                                        ReleaseList.Add(newRelease);
                                       VersionString = String.Empty;
                                       newRelease = new ReleaseContainer(dateFormat, Regex.Match(tempString, SecondVersionPattern).Value);
                                    skip = true;
                                }

                                if(Regex.IsMatch(tempString, datePattern)){ //If it contains the date (about 50-50 chance on the same line as version number)
                                    newRelease.Set_Date(Regex.Match(tempString, datePattern).Value);
                                    skip = true;
                                }

                                if(skip == false){ //dont double up on the date and version
                                    VersionString = VersionString + tempString + "\n"; //append anything
                                }
                                skip = false;
                            }

                        } //end reading file
                          newRelease.Set_Contents(VersionString);
                          ReleaseList.Add(newRelease);
                     } //end using stream

                     txtReader.Close();

                     if (ReleaseList[0].Get_ID().Equals(String.Empty) && ReleaseList[0].Get_Contents().Equals(String.Empty)) //if there is no text before the first version number, remove it
                     {
                         ReleaseList.RemoveAt(0);
                     }

                 } catch (Exception ex){

                    throw ex;
                 }

        }

        private void Repopulate_Versions_List()
        {
            lsbxVersions.Items.Clear();
            foreach (ReleaseContainer v in ReleaseList) {
                lsbxVersions.Items.Add(v.Get_ID());
            }
           
        }

        private void Repopulate_Entry_List(ReleaseContainer selection)
        {
            lsbxHistory.Items.Clear();
            activeHistory = selection.Get_Entry_List();
            if (activeHistory.Count > 0)
            {
                foreach (HistoryEntry h in activeHistory)
                {
                    lsbxHistory.Items.Add(h.Get_ID());
                }
            }
        }

        private void lsbxVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Repopulate_Entry_List(ReleaseList[lsbxVersions.SelectedIndex]);
            lsbxHistory.SelectedIndex = 0;
            if (lsbxVersions.SelectedIndex >= 0)
            {

                switch (ReleaseList[0].Get_App_Type_Int())
                {
                    case 0:
                        rbDesktop.Checked = true;
                        break;
                    case 1:
                        rbMobile.Checked = true;
                        break;
                    case 2:
                        rbSiblingD.Checked = true;
                        break;
                    default:
                        rbDesktop.Checked = false;
                        rbMobile.Checked = false;
                        rbSiblingD.Checked = false;
                        break;
                }
            }
        }



        private void lsbxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = activeHistory[lsbxHistory.SelectedIndex].Get_Entry();
            label2.Text = activeHistory[lsbxHistory.SelectedIndex].Get_Classification_String();
            chkbFlag.Checked = activeHistory[lsbxHistory.SelectedIndex].flag;
            lsboxindex = lsbxHistory.SelectedIndex;
            lblReleaseDate.Text = activeHistory[lsbxHistory.SelectedIndex].ReleaseDate;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (!textBox1.SelectedText.Equals(String.Empty))
            {
                ReleaseList[lsbxVersions.SelectedIndex].Split_Entry(lsbxHistory.SelectedIndex, textBox1.SelectionStart);
                textBox1.SelectedText = String.Empty;
                Repopulate_Entry_List(ReleaseList[lsbxVersions.SelectedIndex]);
            }


        }

        private void btnBug_Click(object sender, EventArgs e)
        {

            Classify_Statement(new Bug());
           
        }

        private void btnFeature_Click(object sender, EventArgs e)
        {
            Classify_Statement(new Feature());

            
        }

        private void btnEnhancement_Click(object sender, EventArgs e)
        {
            Classify_Statement(new Enhancement());
                       
        }

        private void btnIrrelevant_Click(object sender, EventArgs e)
        {

            Classify_Statement(new Junk());

        }

        private void btnAd_Click(object sender, EventArgs e)
        {
            Classify_Statement(new Ad());
        }

        private void btnChangeRating_Click(object sender, EventArgs e)
        {
            Classify_Statement(new RevChangeRequest());
        }

        private void Classify_Statement(Classification type)
        {
            ReleaseList[lsbxVersions.SelectedIndex].classify_Entry(lsbxHistory.SelectedIndex, type);
            Repopulate_Entry_List(ReleaseList[lsbxVersions.SelectedIndex]);
            MoveToNextIndex();
        }

        private void MoveToNextIndex(){
            //TODO roll over version
            try
            {
                if (lsboxindex == lsbxHistory.Items.Count - 1)
                {
                    if (lsbxVersions.SelectedIndex == lsbxVersions.Items.Count - 1)
                    {
                        return;
                    }

                    lsbxVersions.SelectedIndex = lsbxVersions.SelectedIndex + 1;
                    lsboxindex = 0;
                    return;
                }
                lsbxHistory.SelectedIndex = lsboxindex + 1;
            }
            catch (Exception ex)
            {
                Console.Out.Write(ex.Message);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!(textBox1.Text.Equals(String.Empty)) && !(txtName.ContainsFocus))
            {
                switch (e.KeyChar)
                {
                    case 'q':
                        Classify_Statement(new Bug());
                        break;
                    case 'w':
                        Classify_Statement(new Feature());
                        break;
                    case 'e':
                        Classify_Statement(new Enhancement());
                        break;
                    case 'r':
                        Classify_Statement(new Junk());
                        break;
                    case 'u':
                        Classify_Statement(new Ad());
                        break;
                    case 'i':
                        Classify_Statement(new RevChangeRequest());
                        break;
                    case '\r':
                        MoveToNextIndex();
                        break;
                    default: //do nothing
                        break;
                }
            }

        }

        private void chkbFlag_CheckedChanged(object sender, EventArgs e)
        {
            ReleaseList[lsbxVersions.SelectedIndex].Set_Flag(chkbFlag.Checked, lsbxHistory.SelectedIndex);
        }

        private void rawFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseList.Clear();
            groupBox1.Enabled = true;
            btnSet.Enabled = true;
            string filename = string.Empty;
            try
            {

                if (Select_File(ref filename) == true)
                {
                    Read_Release_History_File(filename);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            Repopulate_Versions_List();

        }

        private void savedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseList.Clear();
            groupBox1.Enabled = true;
            btnSet.Enabled = true;
            string filename = string.Empty;
            try
            {

                if (Select_File(ref filename) == true)
                {
                    Read_Our_File(filename);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            Repopulate_Versions_List();


            System.Console.WriteLine(filename);
        }

        public void Read_Our_File(string filename)
        {
            StreamReader txtReader;
            try
            {

                String VersionString = String.Empty;
                String tempString = String.Empty;
                txtReader = new StreamReader(filename);
                ReleaseContainer newRelease = new ReleaseContainer();

                using (txtReader)
                {

                    while ((tempString = txtReader.ReadLine()) != null)
                    {
                        newRelease = JsonConvert.DeserializeObject<ReleaseContainer>(tempString);
                        ReleaseList.Add(newRelease);
                    } //end reading file

                } //end using stream

                txtReader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Read_Comp_File(string filename)
        {
            StreamReader txtReader;
            try
            {

                String VersionString = String.Empty;
                String tempString = String.Empty;
                txtReader = new StreamReader(filename);
                ReleaseContainer newRelease = new ReleaseContainer();
                HistoryEntry newEntry = new HistoryEntry();
                using (txtReader)
                {

                    while ((tempString = txtReader.ReadLine()) != null)
                    {
                    
                       newEntry = JsonConvert.DeserializeObject<HistoryEntry>(tempString);
                       newRelease.EntryList.Add(newEntry);
                        
                    } //end reading file
                    newRelease.Set_Version("All for Comp");
                    ReleaseList.Add(newRelease);
                } //end using stream

                txtReader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {


        
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void patternToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = string.Empty;
            try
            {

                if (Select_File(ref filename) == true)
                {
                    Read_Pattern_File(filename);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            System.Console.WriteLine(filename);
        }

        private void Read_Pattern_File(string filename)
        {

            StreamReader txtReader;
            try
            {

                txtReader = new StreamReader(filename);

                using (txtReader)
                {
                    firstVersionPattern = txtReader.ReadLine();
                    SecondVersionPattern = txtReader.ReadLine();
                    datePattern = txtReader.ReadLine();
                    string line;
                    List<string> tempList = new List<string>();
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = txtReader.ReadLine()) != null)
                    {
                        tempList.Add(line);
                    }
                    dateFormat = new string[tempList.Count];
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        dateFormat[i] = tempList[i];
                    }
                } //end using stream

                txtReader.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }

          //  lblDateFormat.Text = dateFormat;
            lblDatePattern.Text = datePattern;
            lblVersionPat.Text = firstVersionPattern;
            lblSecVersionPattern.Text = SecondVersionPattern;

            }

        private void Set_App_Type(AppType newApp)
        {
            foreach (ReleaseContainer r in ReleaseList)
            {
                r.Set_App_Type(newApp);
            }
        }

        private void rbDesktop_CheckedChanged(object sender, EventArgs e)
        {
            Set_App_Type(new Desktop());
            
        }

        private void rbMobile_CheckedChanged(object sender, EventArgs e)
        {
            Set_App_Type(new Mobile());
        }

        private void rbSibling_CheckedChanged(object sender, EventArgs e)
        {
            Set_App_Type(new SiblingDesktop());
        }

        private void rbSiblingM_CheckedChanged(object sender, EventArgs e)
        {
            Set_App_Type(new SiblingMobile());
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            foreach (ReleaseContainer r in ReleaseList)
            {
                r.ApplicationName = txtName.Text;
                foreach (HistoryEntry h in r.EntryList)
                {
                    h.Set_Name(txtName.Text);
                }
            }
        }

        private void btnMergeDown_Click(object sender, EventArgs e)
        {
            if (lsbxHistory.SelectedIndex >= 0)
            {
                ReleaseList[lsbxVersions.SelectedIndex].Merge_Entry(lsbxHistory.SelectedIndex, lsbxHistory.SelectedIndex + 1);
                textBox1.SelectedText = String.Empty;
                Repopulate_Entry_List(ReleaseList[lsbxVersions.SelectedIndex]);
            }
        }

        private void btnMergeUp_Click(object sender, EventArgs e)
        {
            if (lsbxHistory.SelectedIndex >= 1)
            {
                ReleaseList[lsbxVersions.SelectedIndex].Merge_Entry(lsbxHistory.SelectedIndex - 1, lsbxHistory.SelectedIndex);
                textBox1.SelectedText = String.Empty;
                Repopulate_Entry_List(ReleaseList[lsbxVersions.SelectedIndex]);
            }
        }

        private void saveByVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            StreamWriter txtWriter;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    String VersionString = String.Empty;
                    String tempString = String.Empty;
                    txtWriter = new StreamWriter(saveFileDialog1.FileName);

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

        private void saveForComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter txtWriter;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                try
                {

                    String VersionString = String.Empty;
                    String tempString = String.Empty;
                    txtWriter = new StreamWriter(saveFileDialog1.FileName);

                    using (txtWriter)
                    {

                        foreach (ReleaseContainer v in ReleaseList)
                        {
                            foreach (HistoryEntry h in v.EntryList)
                            {
                                txtWriter.WriteLine(JsonConvert.SerializeObject(h));
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
        }

        private void comparisonFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseList.Clear();
            groupBox1.Enabled = false;
            btnSet.Enabled = false;
            string filename = string.Empty;
            try
            {

                if (Select_File(ref filename) == true)
                {
                    Read_Comp_File(filename);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            Repopulate_Versions_List();


            System.Console.WriteLine(filename);
        }



        private void lsbxHistory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void KeyRouter(char c){
            if (!(textBox1.Text.Equals(String.Empty)) && !(txtName.ContainsFocus))
            {
                switch (c)
                {
                    case 'q':
                        Classify_Statement(new Bug());
                        break;
                    case 'w':
                        Classify_Statement(new Feature());
                        break;
                    case 'e':
                        Classify_Statement(new Enhancement());
                        break;
                    case 'r':
                        Classify_Statement(new Junk());
                        break;
                    //case 'u':
                    //    Classify_Statement(new Ad());
                    //    break;
                    //case 'i':
                    //    Classify_Statement(new RevChangeRequest());
                    //    break;
                    case '\r':
                        MoveToNextIndex();
                        break;
                    default: //do nothing
                        break;
                }
            }
        }
     

    }
}