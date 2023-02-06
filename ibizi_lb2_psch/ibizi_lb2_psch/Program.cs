using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
//using static System.Collections;

namespace ibizi_lb2_psch
{
    class Program
    {
        //Для формирования гаммы шифра выберем параметры датчика ПСЧ: A=5; C=3;T(0)=7; M=2b ; b=5; M=2^5=32.
        //Сформируем три псевдослучайных числа
        //ACTB = 5375
        static int basis = 2;
        const int A = 5;
        const int C = 3;
        const int T = 7;
        static int B = 5;
        static double M = Pow(2,B);

        //const string key = "5375";
        static string int_to_bit_string(int value)
        {
            string result = "";
            //System.Collections.BitArray b = new System.Collections.BitArray(new int[] { value });
            // b = new BitArray(new int[] { value });
            string code = Convert.ToString(value, 2);
            if(code.Length < 8)
                for (int i = 0; i < 8 - code.Length; i++)
                    result += "0";
            return result += code;
        }

        static string text_to_bit_text(string text)
        {
            string result = "";
            foreach(char c in text)
            {
                result += int_to_bit_string((int)c) + " ";
            }
            return result;
        }

        static int intpow(int x, int y)// x в степени y
        {
            int result = 1;
            while(y > 0)
            {
                result *= x;
                y--;
            }
            return result;
        }
        static int PSCHgen(int index)
        {
            int result = -1;
            if(index != 0)
            {
                result = (A * PSCHgen(index - 1) + C)%intpow(basis, B);
            }
            else
            {
                result = 7;
            }

            return result;
        }
        static string Encrypt(string text)
        {
            string result = "";
            string gamma = "";
            
            for(int i = 0; i < text.Length; i++)
            {
                gamma += (char)(PSCHgen(i));
                result += (char)((int)text[i] | (int)gamma[i]);
            }
            Console.WriteLine($"gamma:{gamma}");
            return result;
        }

        static string Decrypt(string text)
        {
            string gamma = "";
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                gamma += (char)PSCHgen(i);
                result += (char)((int)text[i] | (int)gamma[i]);
            }
            Console.WriteLine($"gamma:{gamma}");
            return result;
        }
        static void Main(string[] args)
        {
            string text = "Hello";
            Console.WriteLine(text_to_bit_text(text));
            /*
            string encrypted_text = Encrypt(text);
            Console.WriteLine(encrypted_text);
            string decrypted = Decrypt(encrypted_text);
            Console.WriteLine(decrypted);
            Console.WriteLine(int_to_bit_string((int)'a'));
            */
            Console.ReadKey();
        }
    }
}
