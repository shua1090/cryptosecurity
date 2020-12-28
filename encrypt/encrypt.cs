using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

namespace cryptosecurity {

    public class encrypt{

        string stringToEncrypt;
        BigInteger n;
        BigInteger phi;
        BigInteger e; // Public Key
        BigInteger d; // Private Key
        int maxLength; // For Padding Purposes

        public void setupNPhi() {
            BigInteger f = largePrimeNumber();
            BigInteger k = largePrimeNumber();
            this.n = f * k;
            this.phi = (f - 1) * (k - 1);
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
                inv += a;
            return inv;
        }

        public string encryptionMethodPadding(char z, int maxLength = 0) {
            BigInteger value = 0;
            
                value = BigInteger.ModPow(z, this.e, this.n);
           
            if (maxLength == 0) {
                return value.ToString();
            }
            return new string('0', maxLength - value.ToString().Length) + value;
        }

        public char decryptionMethod(BigInteger f) {
            var z = BigInteger.ModPow(f, this.d, this.n);
            return (char) z;
             
        }

        private void writePublicKeyToFile(string path = "public.key") {
            using (StreamWriter sw = new StreamWriter(path)) {
                sw.WriteLine(this.e);
            }
        }

        private void writePrivateKeyToFile(string path = "private.key") {
            using (StreamWriter sw = new StreamWriter(path)) {
                sw.WriteLine(this.d);
            }
        }

        private void readPublicKey(string path = "public.key") {
            using (StreamReader sr = new StreamReader(path)) {
                this.e = BigInteger.Parse(sr.ReadToEnd());
            }
        }

        private void readN(string path = "n.key") {
            using (StreamReader sr = new StreamReader(path)) {
                this.n = BigInteger.Parse(sr.ReadToEnd());
            }
        }

        private void writeN(string path = "n.key") {
            using (StreamWriter sw = new StreamWriter(path)) {
                sw.WriteLine(this.n);
            }
        }

        private void readPrivateKey(string path = "private.key") {
            using (StreamReader sr = new StreamReader(path)) {
                //Console.WriteLine("THIS IS WAR" + sr.ReadToEnd());
                this.d = BigInteger.Parse(sr.ReadToEnd());
            }
        }

        public void writeEncryptedTextToFile(string encrypted_text ,string path = "cipherText.txt") {
            using (StreamWriter sw = new StreamWriter(path)) {
                sw.Write(encrypted_text);
            }
        }

        public string readEncryptedFile(string path = "cipherText.txt") {
            string t = "";
            using (StreamReader sr = new StreamReader(path)) {
                t += sr.ReadToEnd();
            }
            return t;
        }

        public List<BigInteger> paddingRemoval(string input, int MaxLength) {
            List<BigInteger> temp = new List<BigInteger>();
            BigInteger f;
            int z = input.Length / MaxLength;
            for (int i = 0; i < z; i++) {
                {
                        temp.Add(BigInteger.Parse(input.Substring(0, MaxLength)));
                        input = input.Substring(MaxLength);
                }
            }
            return temp;
        }

        public void generateKeys() {
            setupNPhi();
        }

        private void getMaxLength() {
            int index = 0;
            for (int i = 1; i <= 128; i++) {
                if (encryptionMethodPadding((char)i).Length > maxLength) {
                    index = i;
                    this.maxLength = encryptionMethodPadding((char)i).Length;
                }
            }
        }

        public void encryptToFile(string pathToPublic = "") {
            if (pathToPublic.Equals("")) {
                generateKeys();
                getMaxLength();
                string temp = "";
                foreach (char z in this.stringToEncrypt) {
                    temp += encryptionMethodPadding(z, maxLength);
                }
                writeEncryptedTextToFile(temp);
                writeN();
                writePrivateKeyToFile();
                writePublicKeyToFile();
            }
            else {
                readPublicKey(pathToPublic);
                readN();
            }
        }

        public void decryptFromFile(string pathToPrivate = "") {
            if (pathToPrivate.Equals("")) {
                readN();
                readPrivateKey();
                readPublicKey();
                getMaxLength();
                var f = readEncryptedFile();
                List<BigInteger> temp = paddingRemoval(f, maxLength);
                foreach (BigInteger z in temp) {
                    Console.Write(decryptionMethod(z));
                }
            }
        }

        public encrypt(string text = ""){
            this.stringToEncrypt = text;

            //readN();

            //readPublicKey();
            //readPrivateKey();
            //string t = "";
            //getMaxLength();
            ////foreach (char z in text) {
            ////    t += encryptionMethodPadding(z, maxLength);
            ////}

            ////writeEncryptedTextToFile(t);

            //var f = readEncryptedFile();
            //List<BigInteger> temp = paddingRemoval(f, maxLength);
            //foreach (BigInteger z in temp) {
            //    Console.WriteLine(decryptionMethod(z));
            //}
        }

        public BigInteger randomBigInt(){
            Random rand = new Random();
            byte[] data = new byte[100];
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