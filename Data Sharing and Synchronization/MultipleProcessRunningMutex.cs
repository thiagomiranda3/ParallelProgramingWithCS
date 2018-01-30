using System;
using System.Threading;

namespace ParallelProgramingWithCS.Data_Sharing_and_Synchronization
{
    public class MultipleProcessRunningMutex
    {
        public void Run()
        {
            const string myApp = "MultipleProcessRunningMutex";

            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(myApp);
                Console.WriteLine($"Sorry, {myApp} is already running");
            }
            catch(WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine.");
                mutex = new Mutex(true, myApp);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();
        }
    }
}