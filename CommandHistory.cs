using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace BTM
{
	[Serializable]
	public class CommandRequest
	{
        public ICommand command;

        [XmlIgnore]
		public string commandLine;

        public string CommandLine { get => commandLine; }

		public CommandRequest() { }

        public CommandRequest(ICommand command, string commandLine)
        {
            this.command = command;
            this.commandLine = commandLine;
        }

        public void execute()
		{
			command.execute();
		}

		public void print()
		{
            Console.WriteLine(command);
		}
    }

	public class CommandRequestXml
	{
		public string commandLine;
		public List<string> Data;

		public CommandRequestXml() { }
        public CommandRequestXml(string commandLine, string data)
        {
            this.commandLine = commandLine;
            Data = data.Split('\n').ToList();
        }
    }


	public class CommandHistory
	{
		private List<CommandRequest> commandList;
		private List<CommandRequest> undoCommandList = new List<CommandRequest>();
		private static CommandHistory? instance;

        private Dictionary<string, ICommandFactory> commandFactoriesMap;

        public List<CommandRequest> UndoCommandList { get => undoCommandList; set => undoCommandList = value; }
        public List<CommandRequest> CommandList { get => commandList; set => commandList = value; }

		private CommandHistory() { commandList = new List<CommandRequest>(); }
        private CommandHistory(Dictionary<string, ICommandFactory> commandFactoriesMap)
        {
			commandList = new List<CommandRequest>();
            this.commandFactoriesMap = commandFactoriesMap;
        }

        public static CommandHistory createCommandHistory(Dictionary<string, ICommandFactory> commandsMap)
        {
            if (instance == null) instance = new CommandHistory(commandsMap);
            return instance;
        }

        public static CommandHistory? getCommandHistory()
        {
            if (instance == null) return null;
            return instance;
        }
        public bool addCommand(ICommand command, string commandLine)
        {

            CommandRequest commandRequest = new CommandRequest(command, commandLine);
			commandList.Add(commandRequest);
            //command.execute(commandLine) ;
            return true;
        }
		public bool addCommand(CommandRequest commandRequest)
		{
			commandList.Add(commandRequest);
			return true;
		}

        public bool addCommand(string commandLine)
        {
            if (commandFactoriesMap.ContainsKey(commandLine.Split(' ')[0]) == false) return false;
            ICommand command = commandFactoriesMap[commandLine.Split(' ')[0]].createCommand();

            //if (addCommand(command, commandLine) == false) return false;
            command.execute(commandLine);
            return true;
        }

        public void printHistory()
        {
            foreach (CommandRequest commandRequest in commandList)
            {
                commandRequest.print();
            }
        }

    }

    
}

