using ParallelProgramingWithCS.Basics;
using ParallelProgramingWithCS.Data_Sharing_and_Synchronization;
using ParallelProgramingWithCS.Data_Sharing_and_Synchronization2;

namespace ParallelProgramingWithCS
{
    public class Program
    {
        public static void Main()
        {
            var t = new BankAccountMutex();
            t.Run();
        }
    }
}