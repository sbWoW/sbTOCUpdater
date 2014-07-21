using Microsoft.Win32;
using sbTOCUpdater.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sbTOCUpdater
{
    public partial class MainWindow : Form
    {
        List<TocFile> tocList;

        static protected string regKey = @"sbTOCUpdater";

        static protected string regValueNameVersion = "lastUsedVersion";

        static protected string regValueNameDirectory = "lastUsedDirectory";

        protected Regex regex = new Regex(@"^##\sInterface:\s([0-9]*)", RegexOptions.Compiled);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSettings();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            if (!loadSettings())
            {
                tbFolder.Text = guessWoWBetaDirectory();
                tbInterfaceNumber.Text = Resources.DefaultInterfaceNumber;
            }

            this.Text = String.Format("sbTOCUpdater - v{0}", Resources.Version);
            lblAbout2.Text = String.Format("v{0}, {1:dd.MM.yyyy}, Steffen 'smb' Buehl <sb@sbuehl.com>", Resources.Version, Resources.BuildDate);

        }



        private void btnFolderSelection_Click(object sender, EventArgs e)
        {
            if (tbFolder.Text != null && Directory.Exists(tbFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = tbFolder.Text;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = folderBrowserDialog1.SelectedPath;
            }

            btnUpdate.Enabled = false;
        }

        protected void setRegKeySimple(string key, string valueName, object value)
        {
            RegistryKey baseKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);
            RegistryKey softwareKey = baseKey.OpenSubKey("Software", true);

            if (softwareKey == null)
            {
                baseKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                softwareKey = baseKey.OpenSubKey("Software", true);
            }

            if (softwareKey != null)
            {
                RegistryKey orgKey = softwareKey.CreateSubKey("sbi");
                RegistryKey targetKey = orgKey.CreateSubKey(key);
                
                targetKey.SetValue(valueName, value);
            }           
        }

        protected string getRegKeySimple(string key, string valueName)
        {
            string retValue = null;

            RegistryKey baseKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);
            RegistryKey softwareKey = baseKey.OpenSubKey("Software", true);

            if (softwareKey == null)
            {
                baseKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                softwareKey = baseKey.OpenSubKey("Software", true);
            }

            if (softwareKey != null)
            {
                RegistryKey orgKey = softwareKey.CreateSubKey("sbi");
                RegistryKey targetKey = orgKey.CreateSubKey(key);

                object objValue = targetKey.GetValue(valueName);

                if (objValue != null)
                {
                    retValue = targetKey.GetValue(valueName).ToString();
                }
            }

            return retValue;
        }

        protected string getRegKey(string key, string valueName)
        {
            string value = string.Empty;

            if (
                key != null && !key.Equals(""))
            {
                
                RegistryKey localKey32 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
                
                localKey32 = localKey32.OpenSubKey(key);

                if (localKey32 != null)
                {
                    value = localKey32.GetValue(valueName).ToString();
                }

                if (value == null || value == string.Empty)
                {
                    RegistryKey localKey64 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

                    localKey64 = localKey64.OpenSubKey(key);

                    if (localKey64 != null)
                    {
                        value = localKey64.GetValue(valueName).ToString();
                    }

                }

                Console.WriteLine(String.Format("{0} [value64]: {1}", valueName, value));
            }

            return value;
        }

        protected bool saveSettings()
        {
            bool retValue = false;

            if (isValidInterfaceNumber(tbInterfaceNumber.Text))
            {
                setRegKeySimple(regKey, regValueNameVersion, this.tbInterfaceNumber.Text);

                retValue = true;
            }
            if (this.tbFolder.Text != "" && Directory.Exists(this.tbFolder.Text))
            {
                setRegKeySimple(regKey, regValueNameDirectory, this.tbFolder.Text);

                retValue = true;
            }

            return retValue;
        }

        protected bool loadSettings()
        {
            bool retValue = false;

            string lastVersion = getRegKeySimple(regKey, regValueNameVersion);
            string lastDirectory = getRegKeySimple(regKey, regValueNameDirectory);

            if (isValidInterfaceNumber(lastVersion))
            {
                this.tbInterfaceNumber.Text = lastVersion;

                retValue = true;
            }
            if (lastDirectory != null && lastDirectory != "" && Directory.Exists(lastDirectory))
            {
                this.tbFolder.Text = lastDirectory;

                retValue = true;
            }

            return retValue;
        }


        /*
         * TODO: Directory detection
         */
        protected string guessWoWBetaDirectory()
        {
            string[] regKeys = { @"Software\Blizzard Entertainment\World of Warcraft\Beta"};

            foreach (string key in regKeys)
            {
                string betaDirectory = getRegKey(key, "InstallPath");

                if (betaDirectory != null && !betaDirectory.Equals(""))
                {
                    return betaDirectory;
                }
            }

            return "";

        }

        public void scan()
        {
            
            tocList = new List<TocFile>();

            string rootDirectory = null;

            if (tbFolder.Text != null && Directory.Exists(tbFolder.Text))
            {
                rootDirectory = tbFolder.Text;
            }

            

            /*
             * if(rootDirectory != null && !rootDirectory.ToLower().Contains(@"interface\addons")) {
             *  rootDirectory = rootDirectory + @"\Interface\AddOns\";
             *}
             */

            if (!Directory.Exists(rootDirectory))
            {
                tbFolder.BackColor = Color.Red;

                return;
            }
            else
            {
                tbFolder.BackColor = Color.White;
            }

            

            foreach (string tmpFilename in Directory.GetFiles(rootDirectory, "*.toc", SearchOption.AllDirectories))
            {
                int lineNumber = -1;
                string curLine;

                StreamReader sr = new StreamReader(tmpFilename);

                while ((curLine = sr.ReadLine()) != null)
                {
                    lineNumber++;

                    Match match = regex.Match(curLine);

                    Console.WriteLine(curLine);

                    if (match.Success)
                    {
                        // Console.WriteLine("MATSCH!!!! {0} - {1}", lineNumber, tmpFilename);

                        TocFile tocFile = new TocFile(tmpFilename, match.Groups[1].Value, lineNumber);
                        tocList.Add(tocFile);

                        sr.Close();

                        break;
                    }
                }

                sr.Close();
            }

            dataGridView1.AutoGenerateColumns = false;
            BindingSource source = new BindingSource();
            source.DataSource = tocList;
            dataGridView1.DataSource = source;

            if (tocList.Count > 0)
            {
                btnUpdate.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {

            scan();            

        }

        private bool isValidInterfaceNumber(string interfaceNumber)
        {
            return (interfaceNumber != null
                && interfaceNumber != ""
                && System.Convert.ToInt32(interfaceNumber) < 99999
                && System.Convert.ToInt32(interfaceNumber) > 10000);
        }

       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isValidInterfaceNumber(tbInterfaceNumber.Text))
            {
                tbInterfaceNumber.BackColor = Color.Red;
            }
            else
            {
                tbInterfaceNumber.BackColor = Color.White;

                foreach (TocFile tocFile in tocList)
                {
                    if (tocFile.InterfaceVersion != null)
                    {
                        if (!tocFile.InterfaceVersion.Equals(tbInterfaceNumber.Text))
                        {
                            string[] arrLine = File.ReadAllLines(tocFile.FilePath);

                            arrLine[tocFile.InterfaceVersionLineNumber] = arrLine[tocFile.InterfaceVersionLineNumber].Replace(tocFile.InterfaceVersion, tbInterfaceNumber.Text);

                            File.WriteAllLines(tocFile.FilePath, arrLine);

                            tocFile.InterfaceVersion = tbInterfaceNumber.Text;
                            tocFile.Status = "updated";                            
                        }
                        else
                        {
                            tocFile.Status = "not updated";
                        }

                        dataGridView1.Refresh();
                    }
                }                
            }
        }

        private void tbInterfaceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url;
            if (e.Link.LinkData != null)
                url = e.Link.LinkData.ToString();
            else
                url = lblAbout1.Text.Substring(e.Link.Start, e.Link.Length);

            if (!url.Contains("://"))
                url = "http://" + url;

            var si = new ProcessStartInfo(url);
            Process.Start(si);
        }

    
 
    }
}
