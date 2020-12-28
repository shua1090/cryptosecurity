using System;
using cryptosecurity;


namespace cryptosecurity{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("Hello World!");
            var z = new encrypt("Hello! My Name Is Shynn Lawrence, and today I'm demonstrating the power of the Ron Rivest, Adi Shamir, and Leonard Adleman's " +
                " algorithm in the C# language. Enjoy");
            //z.encryptToFile();
            //z.decryptFromFile();
        }
    }
}
