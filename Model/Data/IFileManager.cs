using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public interface IFileManager
    {
        string FolderPath
        {
            get;
        }

        string FilePath
        {
            get;
        }

        void SelectFile(string name);
        void SelectFolder(string path);
    }
}
