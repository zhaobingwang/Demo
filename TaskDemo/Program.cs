using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            Task t1 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("第一个任务");

            });

            Task t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                RunMethod(cancellationToken);
                Console.WriteLine("第二个任务");
            },cancellationToken);

            Task t3 = Task.Run(() =>
            {
                throw new DivideByZeroException();
                //Console.WriteLine("第三个任务");
            });


            try
            {
                Console.WriteLine($"t1.Status:{t1.Status}");
                //t1.Wait();  //无条件等待t1完成
                //t1.Wait(2000);
                Thread.Sleep(2000);
                cancellationTokenSource.Cancel();
                Task.WaitAll(t1, t2, t3);
                bool completed = t1.IsCompleted;
                Console.WriteLine($"t1 completed:{completed},t1.Status:{t1.Status}");
                if (!completed)
                {
                    Console.WriteLine("t1任务完成前时间已结束");
                }
            }
            catch (AggregateException ex)
            {
                //foreach (var inner in ex.InnerExceptions)
                //{
                //    if (inner is OperationCanceledException)
                //    {
                //        Console.WriteLine("task canceled");
                //    }
                //    else
                //    {
                //        Console.WriteLine(inner.Message);
                //    }
                //}

                //处理异常
                ex.Handle((inner) =>
                {
                    if (inner is OperationCanceledException)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        private static void RunMethod(CancellationToken cancellationToken)
        {
            Console.WriteLine("进入 取消任务的测试方法");
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine("结束 取消任务的测试方法");
        }
    }
}
