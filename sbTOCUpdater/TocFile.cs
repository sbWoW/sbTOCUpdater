using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbTOCUpdater
{
    class TocFile
    {

        bool _selected;

        string _parentFolder;

        string _filePath;

        string _tocName;

        string _interfaceVersion;

        string _status;

        int _interfaceVersionLineNumber;


        public string FilePath
        {
            set { this._filePath = value; }
            get { return this._filePath; }
        }


        public string ParentFolder
        {
            set { this._parentFolder = value; }
            get { return this._parentFolder; }
        }
        public string TocName
        {
            set { this._tocName = value; }
            get { return this._tocName; }
        }
        public string InterfaceVersion
        {
            set { this._interfaceVersion = value; }
            get { return this._interfaceVersion; }
        }

        public string Status
        {
            set { this._status = value; }
            get { return this._status; }
        }

        public bool Selected
        {
            set { this._selected = value; }
            get { return this._selected; }
        }

        public int InterfaceVersionLineNumber
        {
            set { this._interfaceVersionLineNumber = value; }
            get { return this._interfaceVersionLineNumber; }
        }

        public TocFile(string pFilePath, string pInterfaceVersion, int pInterfaceVersionLineNumber)
        {
            FilePath = pFilePath;
            ParentFolder= Path.GetDirectoryName(pFilePath);
            TocName = Path.GetFileName(pFilePath);
            InterfaceVersion = pInterfaceVersion;
            InterfaceVersionLineNumber = pInterfaceVersionLineNumber;

            Selected = false;
        }
    }
}
