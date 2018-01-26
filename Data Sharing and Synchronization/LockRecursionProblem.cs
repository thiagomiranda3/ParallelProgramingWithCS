using System;
using System.Threading;

namespace ParallelProgramingWithCS.Data_Sharing_and_Synchronization
{
    public class LockRecursionProblem
    {
        // Caso seja passado false como parâmetro para o construtor, corremos o risco de criarmos um Dead Lock
        public SpinLock sl = new SpinLock();

        public void LockRecursion(int x)
        {
            bool lockTaken = false;

            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException e)
            {
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    // Aqui, o método que pede lock é chamado antes da liberação dele pelo exit()
                    LockRecursion(x-1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }

        public void Run()
        {
            LockRecursion(5);
        }
    }
}