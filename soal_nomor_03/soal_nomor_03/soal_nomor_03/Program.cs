using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soal_nomor_03
{
    class Program
    {
        private static bool anagram()
        {
            bool is_anagram = false;

            string word = string.Empty; 
            
            word = Console.ReadLine();

            if (word.Length > 0)
            {
                var str = word.Trim().Split(new string[] { " " }, StringSplitOptions.None);

                string kata1 = "";
                string kata2 = "";

                if (str.Length != 2)
                {
                    Console.WriteLine("harus 2 kata");
                    anagram(); 
                }else
                {
                    kata2 = str[0]; 
                    if(str[0].Length == str[1].Length)
                    {

                        for (int kt = 0; kt < str[0].Length; kt++)
                        {
                            kata1 += str[1][((str[0].Length -1) - kt)]; 

                        }

                    }

                }

                if (kata2 == kata1) {
                    is_anagram = true; 
                
                }  

            }

            return is_anagram;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(anagram());

            Console.ReadLine(); 
        }
    }
}
