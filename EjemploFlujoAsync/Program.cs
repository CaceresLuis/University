using EjemploFlujoAsync;
using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();
stopwatch.Start();

Console.WriteLine("\n Bienvenido a la calculadora de Hipoteca Sincrona");

int aniosVidaLaboral = CalculadoraHipotecaSync.GetYearsOfWorkingLife();
Console.WriteLine($"Años de vida laboral obtenido: {aniosVidaLaboral}");

bool esTipoContratoIndefinido = CalculadoraHipotecaSync.EstipoContratoIndefinido();
Console.WriteLine($"Tipo de contrato indefinido: {esTipoContratoIndefinido}");

int sueldoNeto = CalculadoraHipotecaSync.ObtenerSueldoNeto();
Console.WriteLine($"Sueldo neto obtenido: ${sueldoNeto}");

int gastosMensuales = CalculadoraHipotecaSync.ObtenerGastosMensuales();
Console.WriteLine($"Gastos mensuales obtenido: ${gastosMensuales}");

bool hipotecaConcedida = CalculadoraHipotecaSync.AnalisisInformacionParaConcederHipoteca(aniosVidaLaboral, esTipoContratoIndefinido, sueldoNeto, gastosMensuales, cantidadSolicitada: 1500, aniosApagar: 10);

string resultado = hipotecaConcedida ? "Aprovada" : "Denegada";

Console.WriteLine($"Analisis finalizado, Su hipoteca fue: {resultado}");

stopwatch.Stop();

Console.WriteLine($"La operacion ha tardado: {stopwatch.Elapsed}");

stopwatch.Restart();
Console.WriteLine("**************************************************");
Console.WriteLine("Bienvenido a la calculadora de hipoteca Asyncrona");
Console.WriteLine("**************************************************");

Task<int> aniosVidaLaboralaAsync = CalculadoraHipotecaAsync.GetYearsOfWorkingLife();
Task<bool> esTipoContratoIndefinidoAsync =  CalculadoraHipotecaAsync.EstipoContratoIndefinido();
Task<int> sueldoNetoAsync =  CalculadoraHipotecaAsync.ObtenerSueldoNeto();
Task<int> gastosMensualesAsync =  CalculadoraHipotecaAsync.ObtenerGastosMensuales();

var analisisHipotecaTask = new List<Task>
{
    aniosVidaLaboralaAsync, esTipoContratoIndefinidoAsync, sueldoNetoAsync, gastosMensualesAsync
};

while (analisisHipotecaTask.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecaTask);

    if(tareaFinalizada == aniosVidaLaboralaAsync)
        Console.WriteLine($"Años de vida laboral obtenido: {aniosVidaLaboralaAsync.Result}");
    else if(tareaFinalizada == esTipoContratoIndefinidoAsync)
        Console.WriteLine($"Tipo de contrato indefinido: {esTipoContratoIndefinidoAsync.Result}");
    else if (tareaFinalizada == sueldoNetoAsync)
        Console.WriteLine($"Sueldo neto obtenido: ${sueldoNetoAsync.Result}");
    else if (tareaFinalizada == gastosMensualesAsync)
        Console.WriteLine($"Gastos mensuales obtenido: ${gastosMensualesAsync.Result}");

    analisisHipotecaTask.Remove(tareaFinalizada);
}

bool hipotecaConcedidaAsync = CalculadoraHipotecaAsync.AnalisisInformacionParaConcederHipotecaAsync(aniosVidaLaboralaAsync.Result, esTipoContratoIndefinidoAsync.Result, sueldoNetoAsync.Result, gastosMensualesAsync.Result, cantidadSolicitada: 1500, aniosApagar: 10);

string resultadoAsync = hipotecaConcedidaAsync ? "Aprovada" : "Denegada";

Console.WriteLine($"Analisis finalizado, Su hipoteca fue: {resultadoAsync}");

stopwatch.Stop();
Console.WriteLine($"La operacion Async ha tardado: {stopwatch.Elapsed}");