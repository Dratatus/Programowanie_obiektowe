using System;

namespace prog_obj_02
{
    class Program
    {
        static void Main(string[] args)
        {

        }

    }


    class Ulamek
    {
        Ulamek ulamek1 = new Ulamek(5, 3);

        private int mianownik { get; set; }
        private int licznik { get; set; }



        public Ulamek()
        {

        }


        public Ulamek(int test, int test2)
        {
            test = 2;
            test2 = 4;
        }

        public Ulamek(Ulamek kopiaUlamka)
        {
            ulamek1 = kopiaUlamka.ulamek1;
        }


        public static Ulamek operator +(Ulamek licz, Ulamek mian)
        {
            return new Ulamek(licz.)
        }

     
    }



}