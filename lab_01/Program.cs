using System;

namespace Lab_1
{
    class Program
    {
        static void Main()
        {
            Ulamek a = new(1, 2);
            Ulamek b = new(5, 4);
            Ulamek c = new(4, 10);
            Ulamek d = new(a);
            Ulamek e = new(2, 4);

            Console.WriteLine($"TEST 2: a= {a}");
            Console.WriteLine($"TEST 3: {a} + {b} = {a + b}");

            Ulamek[] tab = { a, b, c, d, e };

            Console.WriteLine("Nie posortowane: ");
            for (int i = 0; i < tab.Length; i++)
                Console.WriteLine($"Ułamek: {tab[i].licznik}/{tab[i].mianownik}");
            
            Array.Sort(tab);

            Console.WriteLine();
            Console.WriteLine("Posortowane rosnąco: ");
            for (int i = 0; i < tab.Length; i++)
            {
                Console.WriteLine($"Ułamek: {tab[i].licznik}/{tab[i].mianownik}");
            }
            Console.WriteLine();
            Console.WriteLine($"czy {a} takie samo jak {c}?" + (a == c));
            Console.WriteLine($"czy  {a} jest takie samo jak  {e}?" + a.Equals(e));
            Console.WriteLine();

            Console.WriteLine(Ulamek.Dziesietny(a));

            Console.WriteLine(a + " " + Ulamek.ZaokronglonyWGore(a) + " " + b + " " + Ulamek.ZaokronglonyWGore(b) + " " + c + " " + Ulamek.ZaokronglonyWGore(c));
        }
    }
    public class Ulamek : IEquatable<Ulamek>, IComparable<Ulamek>  /// utworzenie ułamka i jego właściwości
    {
        public int licznik { get; private set; }
        public int mianownik { get; private set; }


       

        public Ulamek() => (this.licznik, this.mianownik) = (1, 1); /// Tworzy obiekt Ułamek na podstawie wprowadzonych danych


        

        public Ulamek(int licznik, int mianownik) /// Tworzy kopie wybranego obiektu Ułamek.
        {
            this.licznik = licznik;
            this.mianownik = mianownik;
        }


 
        /// <param name="ToCopy"> Obiekt którego kopie chcemy uzyskać</param>
        public Ulamek(Ulamek ToCopy)
            => (this.licznik, this.mianownik) = (ToCopy.licznik, ToCopy.mianownik);

        /// Funkcja definuje w jaki sposób obiekt ma być drukowany w konsoli.
        public override string ToString()
        {
            return $"{licznik}/{mianownik}";
        }

        /// Funkcja sprawdzająca czy obiekty mają taką samą wartość

        /// <param name="other"> Obiekt do którego porównujemy 
        public virtual bool Equals(Ulamek other)
        {
            if (other == null) return false;
            if (other == this) return true;
            return Object.Equals(this.licznik, other.licznik) && Object.Equals(this.mianownik, other.mianownik);
        }

        /// Funkcja pórwnująca wartości obiektów

        /// <param name="other"> Obiekt do którego porównujemy </param>
        /// jesli obiekt jest miejszy to = -1, równy = 1 , większy = 1
        public int CompareTo(Ulamek other)
        {
            int x = (this.licznik / other.mianownik) - (other.licznik / this.mianownik);
            if (other == null || x < 0)
            {
                return -1;
            }
            if (x > 0) 
            {
                return 1; 
            }
            return 0;
        }


        /// Przeciążanie operatorów i definicja operacji na ułamkach 
        /// 
        public static Ulamek operator +(Ulamek a, Ulamek b)
        {
            return new Ulamek(a.licznik * b.mianownik + b.licznik * a.mianownik, a.mianownik * b.mianownik);
        }
        public static Ulamek operator -(Ulamek a, Ulamek b)
        {
            return new Ulamek(a.licznik * b.mianownik - b.licznik * a.mianownik, a.mianownik * b.mianownik);
        }
        public static Ulamek operator *(Ulamek a, Ulamek b)
        {
            return new Ulamek(b.licznik / a.mianownik, b.mianownik * a.licznik);
        }
        public static Ulamek operator /(Ulamek a, Ulamek b)
        {
            return new Ulamek(b.mianownik / a.mianownik, b.licznik / a.licznik);
        }
        public static bool operator <(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik < a.mianownik * b.licznik;
        }
        public static bool operator >(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik > a.mianownik * b.licznik;
        }
        public static bool operator <=(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik <= a.mianownik * b.licznik;
        }
        public static bool operator >=(Ulamek a, Ulamek b)
        {
            return a.licznik * b.mianownik >= a.mianownik * b.licznik;
        }


        /// Konwersja ułamka zwykłego na dziesiętny

        /// x => Ułamek zwykły
        public static decimal Dziesietny(Ulamek x)
        {
            decimal a = x.licznik;
            decimal b = x.mianownik;
            return a / b;
        }

        /// Zaokrąglanie ułamka
        ///
        /// x => ułamek zwykły
        public static decimal ZaokronglonyWGore(Ulamek x)
        {
            decimal a = x.licznik;
            decimal b = x.mianownik;
            return Math.Round(a / b, MidpointRounding.ToPositiveInfinity);
        }
    }
}