using System;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS.Basics
{
    public class ExceptionsInTasks
    {

        public void ExceptionTasks()
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Invalid Operation") { Source = "t"};
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Access Violation") { Source = "t2"};
            });

            try
            {
                Task.WaitAll(t, t2);
            }
            catch(AggregateException ae) // Todas as exceptions ficam agrupadas aqui
            {
                // o retorno do Handle indica se a excessão deve ser lançada um nível acima ainda
                ae.Handle(e =>
                {
                    // Tratando apenas a expressão InvalidOperation
                    if(e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid Operation Handled in ExceptionTasks");
                        return true;
                    }
                    else
                        return false;
                });
            }
        }

        public void Run()
        {
            try
            {
                ExceptionTasks();
            }
            catch(AggregateException ae)
            {
                // Como a InvalidOperation foi tratada no método acima, apenas a excessão AccessViolation será lançada aqui
                foreach(var e in ae.InnerExceptions)
                    Console.WriteLine($"{e.GetType()} from {e.Source} with message: {e.Message}");
            }
        }
    }
}