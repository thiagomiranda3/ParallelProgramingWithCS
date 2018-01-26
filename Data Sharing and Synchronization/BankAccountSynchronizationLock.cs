using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS.Data_Sharing_and_Synchronization
{
    // Classe sem nenhum tratamento de sincronicidade de threads
    public class BankAccountSimple
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

    // Utilizando a keyword lock para criar trechos de código atômicos
    public class BankAccountWithLock
    {
        // atributo utilizado para ser marcado como sendo utilizado pelo lock
        private object padLock = new object();
        public int Balance { get; set; }
        
        public void Deposit(int amount)
        {
            lock(padLock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock(padLock)
            {
                Balance -= amount;
            }
        }
    }

    // Classe que simplifica o uso do lock para operações mais simples
    public class BankAccountWithInterlock
    {
        private int balance;
        public int Balance
        {
            get { return balance; }
            private set { balance = value; }
        }
        
        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }

    public class BankAccountSynchronizationLock
    {
        public void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccountWithInterlock();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                        ba.Deposit(100);
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for(int j = 0; j < 1000;j++)
                        ba.Withdraw(100);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}");
        }
    }
}

/*
    Com a keyword lock, podemos utilizar uma outra variável para ser responsável como sendo a vigia das threads.
    No caso desse exemplo estamos utilizando a variável padlock. Quando uma thread passa por um lock que utiliza
    a variável padlock, ela fica marcada como sendo utiliza para que, quando outra thread tente utilizar o mesmo
    trecho de código, perceba que o padlock esta sendo utilizada e espere a outra thread liberar o padlock.

    Isso serve para prevenir a falta de sincronização na utilização da mesma variável por diferentes threads.

    Experimente comentar os trechos com os locks para ver o que acontece
 */