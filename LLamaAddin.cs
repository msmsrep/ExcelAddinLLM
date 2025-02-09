using LLama.Common;
using LLama;
using ExcelDna.Integration;

namespace ExcelAddinLLM;

public static class LLamaAddin
{
    public static async Task<string> LLamaChatAsync(string inputText, int row, int col)
    {
        ExcelAsyncUtil.QueueAsMacro(() =>
         {
             var cellRef = new ExcelReference(row - 1, row - 1, col - 1, col - 1);
             cellRef.SetValue("");
         });
        try
        {
            DirectoryInfo DirectoryInfo = new(Path.GetDirectoryName(ExcelDnaUtil.XllPath)
                                        ?? new(AppDomain.CurrentDomain.BaseDirectory));
            string modelPath = Path.Combine(DirectoryInfo.FullName, @"Llama-3-ELYZA-JP-8B-q4_k_m.gguf");
            var prompt = @"以下はタスクを記述した指示である。要求を適切に完了する応答を書きなさい。";
            prompt += inputText;
            var parameters = new ModelParams(modelPath)
            {
                ContextSize = 1024,
                GpuLayerCount = 20
            };
            using var model = LLamaWeights.LoadFromFile(parameters);
            using var context = model.CreateContext(parameters);
            var executor = new InstructExecutor(context);
            var inferenceParams = new InferenceParams() { MaxTokens = 600 };
            var resultText = "";
            await foreach (var text in executor.InferAsync(prompt, inferenceParams))
            {
                resultText += text;
                ExcelAsyncUtil.QueueAsMacro(() =>
                {
                    var cellRef = new ExcelReference(row - 1, row - 1, col - 1, col - 1);
                    cellRef.SetValue(resultText);
                });
            }
            return "true";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}
