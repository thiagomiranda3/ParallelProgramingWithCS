using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS.Data_Sharing_and_Synchronization2
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

        public void Transfer(BankAccount dest, int amount)
        {
            Balance -= amount;
            dest.Deposit(amount);
        }
    }

    public class BankAccountMutex
    {
        public void Run()
        {
            var tasks = new List<Task>();
            var ba1 = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex1 = new Mutex();
            Mutex mutex2 = new Mutex();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                    {
                        var haveLock = false;
                        // Só executa essa linha caso consiga pegar o lock, do contrário, a thread fica em modo de espera
                        haveLock = mutex1.WaitOne();
                        ba1.Deposit(1);
                        // Se tiver conseguido o lock, faz o release, liberando para outra thread que esteja utilizando o mesmo Mutex como watcher
                        if(haveLock)
                            mutex1.ReleaseMutex();
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                    {
                        var haveLock = false;
                        haveLock = mutex2.WaitOne();
                        ba2.Deposit(1);
                        if(haveLock)
                            mutex2.ReleaseMutex();
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int k = 0; k < 1000; k++)
                    {
                        // Com mutex podemos executar um conjunto de instruções apenas quando todos os mutexes estiverem livres
                        var haveLock = Mutex.WaitAll(new[] {mutex1, mutex2});
                        ba1.Transfer(ba2, 1);
                        if(haveLock)
                        {
                            mutex1.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is ba1: {ba1.Balance} and ba2: {ba2.Balance}");
        }
    }
}