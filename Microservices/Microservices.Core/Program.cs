using NLog;

class Program
{
    private static Logger log = LogManager.GetCurrentClassLogger();

    static async Task Main(string[] args)
    {
        log.Info("Starting");

        // Usamos Masstransit
        //var integrador = new Integrador.MassTransit.Modulo(App.ModulosRemotos);
        //integrador.UsaAlmacenamientoMongoDB().UsaCacheEventosTemporalesEnMongoDB();
        //// Usamos Masstransit
        //App.RegistraModulos(integrador);
        //App.Inicializa();

        using (var sem = new SemaphoreSlim(0, 1))
        using (var ct = new CancellationTokenSource())
        {
            try
            {
                Console.WriteLine("Hit Ctrl-C to exit.");
                Console.CancelKeyPress += (o, e) =>
                {
                    e.Cancel = true;
                    if (e.SpecialKey == ConsoleSpecialKey.ControlC || e.SpecialKey == ConsoleSpecialKey.ControlBreak)
                        ct.Cancel();
                };
                await sem.WaitAsync(ct.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                log.Info("Terminating application...");
                Microservices.Core.Domain.Module.Finalize();
                LogManager.Flush();
                System.Environment.Exit(0);
            }
        }
    }
}