using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public MainWindow()
        {
            InitializeComponent();
            
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

        protected string getRegKey(string key)
        {
            string value = string.Empty;

            if (
                key != null && !key.Equals(""))
            {
                
                RegistryKey localKey32 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
                
                localKey32 = localKey32.OpenSubKey(key);

                if (localKey32 != null)
                {
                    value = localKey32.GetValue("InstallPath").ToString();
                }

                if (value == null || value == string.Empty)
                {
                    RegistryKey localKey64 = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                    if (localKey64 != null)
                    {
                        value = localKey64.GetValue("InstallPath").ToString();
                    }

                }

                Console.WriteLine(String.Format("InstallPath [value64]: {0}", value));
            }

            return value;
        }


        /*
         * TODO: Directory detection
         */
        protected string guessWoWBetaDirectory()
        {
            string[] regKeys = { @"Software\Blizzard Entertainment\World of Warcraft\Beta"};

            foreach (string key in regKeys)
            {
                string betaDirectory = getRegKey(key);

                if (betaDirectory != null && !betaDirectory.Equals(""))
                {
                    return betaDirectory;
                }
            }

            return "";

        }

        public void scan()
        {
            Regex regex = new Regex(@"^##\sInterface:\s([0-9]*)", RegexOptions.Compiled);

            string rootDirectory = null;

            if (tbFolder.Text != null && Directory.Exists(tbFolder.Text))
            {
                rootDirectory = tbFolder.Text;
            }
            else
            {
                //rootDirectory = @"C:\World of Warcraft Beta\Interface\AddOns\_DevPad";
            }

            tocList = new List<TocFile>();

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
                        Console.WriteLine("MATSCH!!!! {0} - {1}", lineNumber, tmpFilename);

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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            tbFolder.Text = guessWoWBetaDirectory();
        }

       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbInterfaceNumber.Text == null || tbInterfaceNumber.Text == "" || System.Convert.ToInt32(tbInterfaceNumber.Text) > 99999 || System.Convert.ToInt32(tbInterfaceNumber.Text) < 10000)
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
    }
}
