using System;
namespace BTM
{
	public class CommandListFactory : ICommandFactory
	{
		public  ICommand createCommand()
		{
			return new CommandList();
		}
	}

    public class CommandAddFactory : ICommandFactory
    {
        public  ICommand createCommand()
        {
            return new CommandAdd();
        }
    }

    public class CommandEditFactory : ICommandFactory
    {
        public  ICommand createCommand()
        {
            return new CommandEdit();
        }
    }

    public class CommandDeleteFactory : ICommandFactory
    {
        public  ICommand createCommand()
        {
            return new CommandDelete();
        }
    }

    public class CommandFindFactory : ICommandFactory
    {
        public  ICommand createCommand()
        {
            return new CommandFind();
        }
    }

    public class CommandPrintQueueFactory : ICommandFactory
    {
        public ICommand createCommand()
        {
            return new CommandPrintHistory();
        }
    }

    public class CommandExportQueueFactory : ICommandFactory
    {
        public ICommand createCommand()
        {
            return new CommandExportHistory();
        }
    }

    public class CommandLoadQueueFactory : ICommandFactory
    {
        public ICommand createCommand()
        {
            return new CommandLoadHistory();
        }
    }

    public class CommandUndoFactory : ICommandFactory
    {
        public ICommand createCommand()
        {
            return new UndoCommand();
        }
    }

    public class CommandRedoFactory : ICommandFactory
    {
        public ICommand createCommand()
        {
            return new RedoCommand();
        }
    }
}

