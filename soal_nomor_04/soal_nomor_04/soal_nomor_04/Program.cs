using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soal_nomor_04
{
    class dataobject
    {
        public string angka { get; set; }
    }
    class Program
    {
        private static void  prosess(int nominal)
        {
            string hasil = string.Empty; 
            dataobject dto = new dataobject(); 
            dto.angka = nominal.ToString();

            for (int x = 0; x < dto.angka.Length; x++)
            {
                hasil = dto.angka[x].ToString();

                for (int y = 0; y <  (dto.angka.Length - 1) - x; y++)
                {
                    hasil += "0";
                }

                Console.WriteLine(string.Format("{0}", hasil)); 

            }

            Console.ReadLine();  

        }
        static void Main(string[] args)
        {
            int nominal = 1225441;
            prosess(nominal);

            //Console.ReadLine(); 
        }
    }
}
