using System;
using System.Threading;

namespace ExamApp
{
    class Program
    {
        private static Data vData = new Data();
        private static AutoResetEvent vRepeatHandler = new AutoResetEvent(true);
        private static AutoResetEvent vResetHandler = new AutoResetEvent(true);


        public static void Main(string[] args)
        {
            vData.vString = Console.ReadLine();
            Thread vThread = new Thread(vmResetApp);
            vThread.Start();
        }

        public static void vmRepeat ()
        {
            vRepeatHandler.WaitOne();

            while (vData.IsRepeat)
            {
                Console.WriteLine(vData.vString);
                Thread.Sleep(vData.vTimeEnter);
            }
           
            vRepeatHandler.Set();
        }

        public static void vmResetApp()
        {
            vResetHandler.WaitOne();

            Thread vThread = new Thread(vmRepeat);
            vThread.Start();            

            Console.ReadLine();
            vData.IsRepeat = false;
            vResetHandler.Set();

            vData.vString = Console.ReadLine();
            vData.IsRepeat = true;
            Thread vRepeatThread = new Thread(vmResetApp);
            vRepeatThread.Start();
        }
    }
}
