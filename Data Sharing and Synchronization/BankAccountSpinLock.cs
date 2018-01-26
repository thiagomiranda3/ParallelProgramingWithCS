using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS.Data_Sharing_and_Synchronization
{
    public class BankAccount
    {
        public int Balance { get; set; }
        
        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    public class BankAccountSpinLock
    {
        public void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                    {
                        var lockTaken = false;

                        try
                        {
                            // Só executa essa linha caso consiga pegar o lock, do contrário, envia uma exceção do tipo LockRecursion
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            // Se tiver conseguido o lock, faz o exit, liberando para outra thread que esteja utilizando o mesmo SpinLock como watcher
                            if(lockTaken)
                                sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                    {
                        var lockTaken = false;

                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if(lockTaken)
                                sl.Exit();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
}