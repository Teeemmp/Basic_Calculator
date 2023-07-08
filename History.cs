using Ookii.CommandLine;
using Ookii.CommandLine.Commands;
using Ookii.CommandLine.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorApp.Commands
{
    [Command("History")]
    [Description("Show command history.")]
    class HistoryCommand : AsyncCommandBase
    {
        public override async Task<int> RunAsync()
        {
            var recorder = new HistoryRecorder();
            List<string> history = await recorder.GetHistory();

            if (history.Count == 0)
            {
                Console.WriteLine("No command history.");
                return 0;
            }

            foreach (string entry in history)
            {
                Console.WriteLine(entry);
            }

            return 0;
        }
    }

    [Command("ClearHistory")]
    [Description("Clear command history.")]
    class ClearHistoryCommand : AsyncCommandBase
    {
        public override async Task<int> RunAsync()
        {
            var recorder = new HistoryRecorder();
            await recorder.ClearHistory();
            Console.WriteLine("Command history cleared.");
            return 0;
        }
    }

    public class HistoryRecorder
    {
        private static readonly string HistoryFilePath = "command_history.txt";

        public async Task RecordCommand(string command, string result)
        {
            string entry = $"{DateTime.Now}: {command} - {result}";
            await File.AppendAllTextAsync(HistoryFilePath, entry + Environment.NewLine);
        }

        public async Task<List<string>> GetHistory()
        {
            if (File.Exists(HistoryFilePath))
            {
                return (await File.ReadAllLinesAsync(HistoryFilePath)).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public async Task ClearHistory()
        {
            if (File.Exists(HistoryFilePath))
            {
                File.Delete(HistoryFilePath);
            }
        }
    }
}