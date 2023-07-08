using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Ookii.CommandLine.Validation;
using System.ComponentModel;

namespace CalculatorApp.Commands
{
    [Command("Divide")]
    [Description("Divide numbers.")]

    class DivideCommand : AsyncCommandBase
    {
        [CommandLineArgument(Position = 0, IsRequired = true)]
        [MultiValueSeparator]

        public double[] Numbers { get; set; }

        public override async Task<int> RunAsync()
        {
            double result = Numbers[0];
            foreach (double number in Numbers.Skip(1))
            {
                result /= number;
            }

            using var writer = LineWrappingTextWriter.ForConsoleOut();
            await writer.WriteLineAsync($"The answer is: {result}");

            var recorder = new HistoryRecorder();
            await recorder.RecordCommand($"Divide {string.Join(" ", Numbers)}", $"The answer is: {result}");

            return 0;
        }
    }
}
