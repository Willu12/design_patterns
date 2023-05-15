using System;
using System.Xml.Serialization;

namespace BTM
{
	public class commandPrintQueue : ICommand
	{
		private CommandQueue commandQueue;
		public commandPrintQueue(CommandQueue commandQueue)
		{
			this.commandQueue = commandQueue;
		}

        public bool checkcommandLine(string commandLine)
        {
            if (commandLine.ToLower() != "queue print") return false;
            return true;
        }

        public void execute(string s = "")
        {
            commandQueue.printQueue();
        }
    }

    public class commandCommitQueue : ICommand
    {
        
        private CommandQueue commandQueue;
        public commandCommitQueue(CommandQueue commandQueue)
        {
            this.commandQueue = commandQueue;
        }

        public bool checkcommandLine(string commandLine)
        {
            if (commandLine.ToLower() != "queue commit") return false;
            return true;
        }

        public void execute(string s = "")
        {
            commandQueue.commit();
        }
    }

    public class commandExportQueue : ICommand
    {
        private CommandQueue commandQueue;

        private string fileName;
        private string format;

        public commandExportQueue(CommandQueue commandQueue)
        {
            this.commandQueue = commandQueue;
        }

        public bool checkcommandLine(string commandLine)
        {
            string[] words = commandLine.Split(' ');
            if (words.Length != 4) return false;

            if (words[3] != "xml" && words[3] != "plaintext") return false;
            fileName = words[2];
            format = words[3];
            return true;
        }

        public void execute(string s = "")
        {
            if (checkcommandLine(s) == false) return;

            FileStream fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write);

            if(format == "plaintext")
            {
                using(var sw = new StreamWriter(fileStream))
                {
                    foreach (CommandRequest commandRequest in commandQueue.commandQueue)
                    {
                        sw.WriteLine(commandRequest.CommandLine);
                    }
                }
            }
            else
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CommandRequest));
                TextWriter textWriter = new StreamWriter(fileName);
                xmlSerializer.Serialize(textWriter, commandQueue.commandQueue);
            }
        }
    }

    
}

