using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;


namespace BTM
{
	public class CommandPrintHistory : ICommand
	{
        private CommandHistory commandHistory = CommandHistory.getCommandHistory();
        public CommandPrintHistory(CommandHistory commandHistory)
		{
			this.commandHistory = commandHistory;
		}
        public CommandPrintHistory()
        {}
        public bool checkcommandLine(string commandLine)
        {
            if (commandLine.ToLower() != "print") return false;
            return true;
        }

        public bool enqueue(string s)
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        public void execute(string s)
        {
            execute();
        }

        public void execute()
        {
            commandHistory.printHistory();
        }
        public string saveCommand()
        {
            return "";
        }

        public void undo()
        {
            Console.WriteLine("undo print ?");
        }
    }

    public class CommandExportHistory : ICommand
    {
        private CommandHistory commandHistory = CommandHistory.getCommandHistory();

        private string fileName;
        private string format;
        public CommandExportHistory()
        {}

        public CommandExportHistory(CommandHistory commandQueue)
        {
            this.commandHistory = commandQueue;
        }

        public bool checkcommandLine(string commandLine)
        {
            string[] words = commandLine.Split(' ');
            if (words.Length != 3) return false;

            if (words[2] != "xml" && words[2] != "plaintext") return false;
            fileName = words[1];
            format = words[2];
            return true;
        }

        public bool enqueue(string s)
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        public void execute(string s)
        {
            if (checkcommandLine(s) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, $"export {fileName} {format}") == false) return;
            execute();
        }

        public void execute()
        {
            if (format == "plaintext")
            {
                FileStream fileStream = File.Open($"{fileName}", FileMode.OpenOrCreate, FileAccess.Write);
                using (var sw = new StreamWriter(fileStream))
                {
                    foreach (CommandRequest commandRequest in commandHistory.CommandList)
                    {
                        sw.WriteLine(commandRequest.CommandLine);
                        sw.WriteLine(commandRequest.command.saveCommand());
                    }
                }
            }
            else
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<CommandRequestXml>));
               
                TextWriter textWriter = new StreamWriter($"{fileName}");
                List<string> text = new List<string>();
                List<CommandRequestXml> commandRequestXmls= new List<CommandRequestXml>();

                foreach(CommandRequest commandRequest in commandHistory.CommandList)
                {
                    CommandRequestXml commandRequestXml = new CommandRequestXml(commandRequest.commandLine,commandRequest.command.saveCommand());
                    commandRequestXmls.Add(commandRequestXml);
                    text.Add(commandRequest.commandLine);
                    text.Add(commandRequest.command.saveCommand());
                }
                while(text.Contains(""))
                {
                    text.Remove("");
                }
                xmlSerializer.Serialize(textWriter, commandRequestXmls);
            }
        }
        public string saveCommand()
        {
            return "";
        }

        public void undo()
        {
            Console.WriteLine($"undoing command export to {fileName}");
            Console.WriteLine("deleting File " + fileName);
            File.Delete(fileName);
        }

        public override string ToString()
        {
            return $"export to {fileName} in {format}";
        }
    }

    public class CommandLoadHistory : ICommand
    { 
        private CommandHistory commandQueue = CommandHistory.getCommandHistory();

        private string fileName;
        private string format;
        private List<CommandRequest> addedCommands;

        public CommandLoadHistory(CommandHistory commandQueue)
        {
            addedCommands = new List<CommandRequest>();
            this.commandQueue = commandQueue;
        }
        public CommandLoadHistory()
        {
            addedCommands = new List<CommandRequest>();
        }
        public override string ToString()
        {
            return $"load from {fileName}";
        }

        public bool checkcommandLine(string commandLine)
        {
            string[] words = commandLine.Split(' ');
            if (words.Length != 2)return false;
            fileName = words[1];
            checkFormat(fileName);

            return true;
        }

        private void checkFormat(string fileName)
        {
            string firstLine = File.ReadLines(fileName).First();
            if (firstLine[0] != '<') format = "plaintext";
            else format = "xml";
            return;
        }

        public bool enqueue(string s)
        {
            if (checkcommandLine(s)) return true;
            return false;
        }

        public void execute(string s)
        {
            if (checkcommandLine(s) == false) return;
            if (CommandHistory.getCommandHistory().addCommand(this, $"load {fileName}") == false) return;
            execute();
        }

        public void execute()
        {

            if (format == "plaintext")
            {
                if (File.Exists(fileName) == false)
                {
                    Console.WriteLine("this file does not exist :-(");
                    return;
                }
                TextReader inp = File.OpenText(fileName);

                Console.SetIn(inp);
                foreach (string line in File.ReadLines(fileName))
                {
                    string commandLine = Console.ReadLine();
                    commandQueue.addCommand(line);

                }
                var standardInput = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(standardInput);
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CommandRequestXml>));
                TextReader reader = new StreamReader(fileName);
                List<CommandRequestXml> commands = (List<CommandRequestXml>)serializer.Deserialize(reader);
                List<string> readLines = new List<string>();
                
                foreach (CommandRequestXml s in commands )
                {
                    readLines.Add(s.commandLine);
                    foreach(string line in s.Data)
                    {
                        readLines.Add(line);
                    }
                }
                TextReader inp = new StringReader(string.Join(Environment.NewLine, readLines));
                Console.SetIn(inp);
                foreach (string line in readLines)
                {
                    string commandLine = Console.ReadLine();
                    commandQueue.addCommand(line);
                    addedCommands.Add(commandQueue.CommandList[commandQueue.CommandList.Count - 1]);
                    //commandQueue.CommandList[commandQueue.CommandList.Count - 1].execute();
                }
                var standardInput = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(standardInput);
            }
        }
        public string saveCommand()
        {
            return "";
        }

        public void undo()
        {
            Console.WriteLine($"undoing command load from {fileName}");
            foreach(var command in addedCommands)
            {
                commandQueue.CommandList.Remove(command);
                command.command.undo();
            }
        }
    }


}

