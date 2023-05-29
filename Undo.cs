using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM
{
    public class UndoCommand : ICommand
    {
        CommandHistory history = CommandHistory.getCommandHistory();
        public void execute(string s)
        {
            execute();

        }

        public void execute()
        {
            if (history.CommandList.Count == 0)
            {
                Console.WriteLine("There is nothing to be undone");
                return;
            }
            CommandRequest lastCommand = history.CommandList[history.CommandList.Count - 1];
            history.CommandList.RemoveAt(history.CommandList.Count - 1);
            lastCommand.command.undo();
            history.UndoCommandList.Add(lastCommand);
        }

        public string saveCommand()
        {
            throw new NotImplementedException();
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }

    public class RedoCommand : ICommand
    {
        CommandHistory history = CommandHistory.getCommandHistory();
        public void execute(string s)
        {
            execute();
        }

        public void execute()
        {
            if (history.UndoCommandList.Count == 0)
            {
                Console.WriteLine("There is nothing to be redone");
                return;
            }
            CommandRequest lastCommand = history.UndoCommandList[history.UndoCommandList.Count - 1];
            history.UndoCommandList.RemoveAt(history.UndoCommandList.Count - 1);
            lastCommand.command.execute();
            history.CommandList.Add(lastCommand);
        }

        public string saveCommand()
        {
            throw new NotImplementedException();
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
