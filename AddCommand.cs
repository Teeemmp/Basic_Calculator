using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Ookii.CommandLine.Validation;
using System.ComponentModel;

namespace CalculatorApp.Commands
{
    [Command("Add")]
    [Description("Add numbers.")]
    
    class AddCommand : AsyncCommandBase
    {
        [CommandLineArgument(Position = 0, IsRequired = true)]
        [MultiValueSeparator]
        
        public double[] Numbers { get; set; }

        public override async Task<int> RunAsync()
        {
            var result = Numbers.Sum();

            using var writer = LineWrappingTextWriter.ForConsoleOut();
            await writer.WriteLineAsync($"The answer is: {result}");

            var recorder = new HistoryRecorder();
            await recorder.RecordCommand($"Add {string.Join(" ", Numbers)}", $"The answer is: {result}");

            return 0;
        }
    }
}