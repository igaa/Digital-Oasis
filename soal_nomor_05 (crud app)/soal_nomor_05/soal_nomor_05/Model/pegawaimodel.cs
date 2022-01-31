using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soal_nomor_05.Model
{
    public class pegawaimodel
    {

        public Guid id { get; set; }
        public string nama { get; set; }
        public string email { get; set; }
        public int gender { get; set; }
        public int nip { get; set; }
        public string hoby { get; set; }
        public byte[] photo {get; set;}
    }
}
