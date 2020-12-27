using System;
using System.Numerics;

namespace cryptosecurity {

    public class encrypt{

        string stringToEncrypt;
        BigInteger n;
        BigInteger phi;

        public void setupNPhi() {
            BigInteger f = largePrimeNumber() * largePrimeNumber() * largePrimeNumber();
            BigInteger k = largePrimeNumber() * largePrimeNumber() * largePrimeNumber();
            this.n = f * k;
            this.phi = (f - 1) * (k - 1);
            Console.WriteLine(this.n);
            Console.WriteLine(this.phi);
        }

        public encrypt(string text){
            this.stringToEncrypt = text;
            setupNPhi();
        }

        public static int NextInt32() {
            var rand = new Random();
            int firstBits = rand.Next(0, 1 << 4) << 28;
            int lastBits = rand.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public decimal randomDecimal(){
            var rand = new Random();
            byte scale = (byte) rand.Next(29);
            bool sign = rand.Next(2) == 1;
            return new decimal(NextInt32(), NextInt32(), NextInt32(), sign, scale);
        }

        public BigInteger largePrimeNumber() {
            decimal num1 = 0;
            while (((num1 < long.MaxValue) || (num1 < long.MinValue)) || (!IsPrime(num1))) {
                num1 = this.randomDecimal();
            }
            return (BigInteger) num1;
        }

        public static bool IsPrime(decimal number){
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            int root = (int) Math.Sqrt((double)number);
            for (int i = 3; i <= root; i += 2){
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}