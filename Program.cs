using ParallelProgramingWithCS.Basics;
using ParallelProgramingWithCS.Data_Sharing_and_Synchronization;

namespace ParallelProgramingWithCS
{
    public class Program
    {
        public static void Main()
        {
            var t = new BankAccountSynchronizationLock();
            t.Run();
        }
    }
}