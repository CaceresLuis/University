namespace EjemploFlujoAsync
{
    public class CalculadoraHipotecaAsync
    {
        public static async Task<int> GetYearsOfWorkingLife()
        {
            Console.WriteLine("\n Obtener años de vida laboral");
            await Task.Delay(5000);
            return new Random().Next(1, 35);
        }

        public static async Task<bool> EstipoContratoIndefinido()
        {
            Console.WriteLine("\n Verificando si el tipo de contrato es indefinido");
            await Task.Delay(5000);
            return (new Random().Next(1, 10)) % 2 == 0;
        }

        public static async Task<int> ObtenerSueldoNeto()
        {
            Console.WriteLine("\n Obteniendo el sueldo neto...");
            await Task.Delay(5000);
            return new Random().Next(200, 2000);
        }

        public static async Task<int> ObtenerGastosMensuales()
        {
            Console.WriteLine("\n Obteniendo los gastos mensuales...");
            await Task.Delay(5000);
            return new Random().Next(200, 1000);
        }

        public static bool AnalisisInformacionParaConcederHipotecaAsync(int aniosVidaLaboral, bool estipoContratoIndefinido, int sueldoNeto, int gastosmensuales, int cantidadSolicitada, int aniosApagar)
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
