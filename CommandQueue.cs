using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace BTM
{
	[Serializable]
	public class CommandRequest : ISerializable
	{
		public string commandLine;

        [NonSerialized]
		public ICommand command;

        public string CommandLine { get => commandLine; }

		public CommandRequest() { }

        public CommandRequest(ICommand command, string commandLine)
        {
            this.command = command;
            this.commandLine = commandLine;
        }

        public void execute()
		{
			command.execute(commandLine);
		}

		public void print()
		{
			command.checkcommandLine(commandLine);
			Console.WriteLine(command);
		}

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
			info.AddValue("commandLine", commandLine);
        }
    }

	public class CommandQueue
	{
		public  Queue<CommandRequest> commandQueue;
		private static CommandQueue? instance;


		private CommandQueue()
		{
			commandQueue = new Queue<CommandRequest>();
		}

		public static CommandQueue getCommandQueue()
		{
			if (instance == null) instance = new CommandQueue();
			return instance;
		}

		public bool addCommand(ICommand command,string commandLine)
		{
			CommandRequest commandRequest = new CommandRequest(command, commandLine);

			if (command.checkcommandLine(commandLine) == false)
			{
				Console.WriteLine("invalid command"); return false;

            }

            commandQueue.Enqueue(commandRequest);
			return true;
		}

		public void printQueue()
		{
			foreach(CommandRequest commandRequest in commandQueue)
			{
				commandRequest.print();
			}
		}

		public void commit()
		{
			while(commandQueue.Count > 0)
			{
				CommandRequest commandRequest = commandQueue.Dequeue();
				commandRequest.execute();
			}
		}
	}
}

