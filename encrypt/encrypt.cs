using System;


namespace cryptosecurity{

    public class encrypt{

        public string stringToEncrypt;

        public encrypt(string text){
            this.stringToEncrypt = text;
            decimal num1 = 0;
            while (num1 < long.MaxValue) num1 = this.randomDecimal();
            Console.WriteLine(num1);
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

        public static bool IsPrime(int number){
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