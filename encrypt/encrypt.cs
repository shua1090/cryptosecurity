using System;


namespace cryptosecurity{

    public class encrypt{

        public string stringToEncrypt;

        public encrypt(string text){
            this.stringToEncrypt = text;
            decimal num1 = 0;
            while (num1 < long.MaxValue) num1 = this.num1();
        }

        public decimal num1(){
            var rand = new Random();
            decimal num1;
            int z = rand.Next(int.MinValue, int.MaxValue);
            long f = z | (long) rand.Next(int.MinValue, int.MaxValue);
            int k = rand.Next(int.MinValue, int.MaxValue);
            long y = k | (long) rand.Next(int.MinValue, int.MaxValue);
            num1 = (y | z);
            Console.WriteLine(num1);
            return num1;
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