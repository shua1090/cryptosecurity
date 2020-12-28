using System;
using System.Numerics;

namespace cryptosecurity {

    public class encrypt{

        string stringToEncrypt;
        BigInteger n;
        BigInteger phi;
        BigInteger e; // Public Key
        BigInteger d; // Private Key
        BigInteger a;
        BigInteger b;

        public void setupNPhi() {
            BigInteger f = largePrimeNumber();
            BigInteger k = largePrimeNumber();
            this.a = f;
            this.b = k;
            this.n = f * k;
            this.phi = (f - 1) * (k - 1);
            Console.WriteLine("f is: " + f);
            Console.WriteLine("k is: " + k);
            BigInteger temp = 0;
            Console.WriteLine("Starting here:");
            do {
                temp = largePrimeNumber();
                if (temp < f || temp > k) {
                    continue;
                }
            } while (!(BigInteger.GreatestCommonDivisor(temp, this.phi) == 1));
            this.e = temp;
            Console.WriteLine("e is " + this.e);
            Console.WriteLine("phi is " + this.phi);
            this.d = inverse(this.phi, this.e);
            Console.WriteLine("d is " + this.d);
            Console.WriteLine("Expected answer, 1; Actual Answer: " + (this.e * this.d) % this.phi);
        }

        BigInteger inverse(BigInteger a, BigInteger b) {
            BigInteger inv =0;
            BigInteger q, r, r1 = a, r2 = b, t, t1 = 0, t2 = 1;

            while (r2 > 0) {
                q = r1 / r2;
                r = r1 - q * r2;
                r1 = r2;
                r2 = r;

                t = t1 - q * t2;
                t1 = t2;
                t2 = t;
            }

            if (r1 == 1)
                inv = t1;

            if (inv < 0)
                inv = inv + a;

            return inv;
        }

        public encrypt(string text){
            this.stringToEncrypt = text;
            setupNPhi();
            var integerToEncrypt = 105;
            Console.WriteLine("Public Key");
            Console.WriteLine(this.e);
            Console.WriteLine("Private Key");
            Console.WriteLine(this.d);
            var encryptedValue = BigInteger.ModPow(integerToEncrypt, this.e, this.n);
            Console.WriteLine("Encrypted Data");
            Console.WriteLine(encryptedValue);
            var decryptedText = BigInteger.ModPow(encryptedValue, this.d, this.n);
            Console.WriteLine("Decrypted Data:");
            Console.WriteLine(decryptedText);
        }

        public BigInteger randomBigInt(){
            Random rand = new Random();
            byte[] data = new byte[256];
            rand.NextBytes(data);
            return new BigInteger(data);
        }

        public BigInteger largePrimeNumber() {
            BigInteger num1 = 0;
            while (!IsProbablyPrime(num1)){
                num1 = this.randomBigInt();
            }
            Console.WriteLine("Got 1 big number");
            return num1;
        }

        public BigInteger gcd(BigInteger a, BigInteger b) {
            if (b == 0) return a;

            else return gcd(b, a % b);
        }

        public static bool IsProbablyPrime(BigInteger value, int witnesses = 10) {
            if (value <= 1)
                return false;

            if (witnesses <= 0)
                witnesses = 10;

            BigInteger d = value - 1;
            int s = 0;

            while (d % 2 == 0) {
                d /= 2;
                s += 1;
            }

            Byte[] bytes = new Byte[value.ToByteArray().LongLength];
            BigInteger a;
            var Gen = new Random();

            for (int i = 0; i < witnesses; i++) {
                do {
                    Gen.NextBytes(bytes);

                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= value - 2);

                BigInteger x = BigInteger.ModPow(a, d, value);
                if (x == 1 || x == value - 1)
                    continue;

                for (int r = 1; r < s; r++) {
                    x = BigInteger.ModPow(x, 2, value);

                    if (x == 1)
                        return false;
                    if (x == value - 1)
                        break;
                }

                if (x != value - 1)
                    return false;
            }

            return true;
        }
    }
}