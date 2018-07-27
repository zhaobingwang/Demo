using Autofac;
using System;

namespace AutofacExample
{
    // document: http://docs.autofac.org/en/latest/getting-started/index.html
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();
            var writer = Container.Resolve<IDateWriter>();
            writer.WriteDate();
        }
    }

    //public static void WriteDate()
    //{
    //    using (var scope = Container.BeginLifetimeScope())
    //    {
    //        var writer = scope.Resolve<IDateWriter>();
    //        writer.WriteDate();
    //    }
    //}

    public interface IOutput
    {
        void Write(string content);
    }
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    public interface IDateWriter
    {
        void WriteDate();
    }
    public class TodayWriter : IDateWriter
    {
        public IOutput _output;
        public TodayWriter(IOutput output)
        {
            _output = output;
        }
        public void WriteDate()
        {
            this._output.Write(DateTime.Today.ToShortDateString());
        }
    }
}
