using System;
using System.Numerics;

namespace cryptosecurity {

    public class encrypt{

        string stringToEncrypt;
        BigInteger n;
        BigInteger phi;

        public void setupNPhi() {
            BigInteger f = largePrimeNumber();
            BigInteger k = largePrimeNumber();
            this.n = f * k;
            this.phi = (f - 1) * (k - 1);
        }

        public encrypt(string text){
            this.stringToEncrypt = text;
            setupNPhi();
        }

        public BigInteger randomBigInt(){
            Random rand = new Random();
            byte[] data = new byte[100000];
            rand.NextBytes(data);
            return new BigInteger(data);
        }

        public BigInteger largePrimeNumber() {
            BigInteger num1 = 0;
            while (((num1 < long.MaxValue) || (num1 < long.MinValue)) || (!IsPrime(num1))) {
                num1 = this.randomBigInt();
            }
            return num1;
        }

        public static bool IsPrime(BigInteger number){
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