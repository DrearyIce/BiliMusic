using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igex.Models
{
    public static class util_File
    {
        public static FileInfo SaveFile(string str, string fullname)
        {
            using (FileStream fs = File.Create(fullname))
            {
                byte[] buffer = new UTF8Encoding(true).GetBytes(str);
                fs.Write(buffer);
                fs.Close();
            }
            return new FileInfo(fullname);
        }
    }
}
