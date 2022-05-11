using System;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Ulamek ulamek1 = new Ulamek(6, 10);
            Ulamek ulamek2 = new Ulamek(5, 25);
            Console.WriteLine(ulamek1);
            Console.WriteLine(ulamek2);

            Ulamek dodawanie = ulamek1 + ulamek2;
            Console.WriteLine(dodawanie);


        }


        class Ulamek
        {


            private int licznik, mianownik;




            public Ulamek()
            {

            }

            //konstruktor dwuargumentowy przypisujący do lczik1 i mianownik2 wartości z licznika i mianownika
            public Ulamek(int licznik1, int mianownik2)
            {
                licznik = licznik1;
                mianownik = mianownik2;
            }


            //konstruktor kopiujący licznik i mianownik
            public Ulamek(Ulamek kopia)
            {
                this.mianownik = kopia.mianownik;
                this.licznik = kopia.licznik;
            }


            //przeciążenie operatora +
            public static Ulamek operator +(Ulamek licz, Ulamek mian)
            {
                return new Ulamek(licz.licznik + mian.licznik, licz.mianownik + mian.mianownik);
            }
            //przeciążenie motedy TOString
            public override string ToString()
            {

                return "Ten Ulamek to: " + licznik + " " + mianownik;
            }

        }
    }
}