namespace EjemploFlujoAsync
{
    public static class CalculadoraHipotecaSync
    {
        public static int GetYearsOfWorkingLife()
        {
            Console.WriteLine("\n Obtener años de vida laboral");
            Task.Delay(5000).Wait();
            return new Random().Next(1, 35);
        }

        public static bool EstipoContratoIndefinido()
        {
            Console.WriteLine("\n Verificando si el tipo de contrato es indefinido");
            Task.Delay(5000).Wait();
            return (new Random().Next(1, 10)) % 2 == 0;
        }

        public static int ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo el sueldo neto...");
            Task.Delay(5000).Wait();
            return new Random().Next(200, 2000);
        }

        public static int ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo los gastos mensuales...");
            Task.Delay(5000).Wait();
            return new Random().Next(200, 1000);
        }

        public static bool AnalisisInformacionParaConcederHipoteca(int aniosVidaLaboral, bool estipoContratoIndefinido, int sueldoNeto, int gastosmensuales, int cantidadSolicitada, int aniosApagar)
        {
            Console.WriteLine("\n Analizando la informacion para conceder hipoteca");
            if (aniosVidaLaboral < 2) return false;

            int cuota = (cantidadSolicitada / aniosApagar) / 12;
            if (cuota >= sueldoNeto || cuota > (sueldoNeto / 2)) return false;

            int porcentajeGastosSobreSueldo = ((gastosmensuales * 100) / sueldoNeto);
            if (porcentajeGastosSobreSueldo > 30) return false;

            if ((cuota + gastosmensuales) >= sueldoNeto) return false;

            if (!estipoContratoIndefinido)
            {
                if ((cuota + gastosmensuales) > (sueldoNeto / 3)) return false;
                else return true;
            }

            return true;
        }
    }
}
