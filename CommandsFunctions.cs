using System;
namespace BTM
{
	public static class CommandsFunctions
	{
		public static ICommand? getCommandFromLine(string? CommandLine)
		{
			if (CommandLine == null) return null;
			string[] words = CommandLine.Trim().ToLower().Split(' ');
			return null;
		}
	}
}

