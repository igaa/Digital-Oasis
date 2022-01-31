using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soal_nomor_01
{
    public class Program 
    {
        public static void prosses()
        {
            string hasil = string.Empty;
            int inputx = 0; 
            string input = Console.ReadLine();

            inputx = int.Parse(input); 

            if (inputx > 0)
            {
                int kali2 = 2;
                int kali3 = 3;
                int kali6 = 6; 
                for (int x = 1; x <= inputx; x++)
                {
                    if (x == kali2)
                    {
                        if (x == kali6)
                        {
                            hasil = string.Format("{0} {1}", x, "DIGITAL OASIS");
                            kali6 += 6;
                            kali3 += 3; 
                            kali2 += 2; 

                        }else 
                        {
                            hasil = string.Format("{0} {1}", x, "DI");
                            kali2 += 2;
                        }
                        
                    }else if ( x == kali3)
                    {
                        if (x == kali6)
                        {
                            hasil = string.Format("{0} {1}", x, "DIGITAL OASIS");
                            kali6 += 6;
                            kali3 += 3;
                            kali2 += 2; 
                        }
                        else
                        {
                            hasil = string.Format("{0} {1}", x, "OS");
                            kali3 += 3;
                        }
                        
                    }else if (x == kali6)
                    {
                        hasil = string.Format("{0} {1}", x, "DIGITAL OASIS");
                        kali6 += 6;
                    }
                    else
                    {
                        hasil = string.Format("{0}", x);
                    }

                    Console.WriteLine(hasil); 

                }

            }
            
          
        }
        public static void Main(string[] args)
        {
            prosses(); 

            Console.ReadLine();
           
        }

    }
        
}

