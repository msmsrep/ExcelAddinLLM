using ExcelAddinLLM.Utils;
using ExcelDna.Integration;

namespace ExcelAddinLLM;

public static class ExcelAddonFunctions
{
    [ExcelFunction(Description = "exceldna関数")]
    public static string SayHello(string name)
    {
        return "Hello " + name;
    }
    private static readonly object ReturnNA = new();
    [ExcelFunction(Description = "ローカルLLM")]
    public static string SayLLM(string targetValue, int row, int col)
    {
        try
        {
            var functionName = nameof(SayLLM);
            var parameters = new object[] { targetValue, row, col };
            var result = AsyncTaskUtil.RunTask(functionName, parameters, async () =>
              {
                  return await LLamaAddin.LLamaChatAsync(targetValue, row, col);
              });
            if (result.Equals(ReturnNA))
                return ExcelError.ExcelErrorNA.ToString();
            // return "エラー";
            if (result.Equals(ExcelError.ExcelErrorNA))
                return "Busy...";
            else
                return "true";
            // return result.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}
