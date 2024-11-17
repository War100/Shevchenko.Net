namespace Shevchenko.WordDeclension
{
    using System;
    public class CommandRunnerFactory
    {
        /// <summary>
        /// Creates a new command runner for the given command.
        /// </summary>
        public IInflectionCommandRunner Make(InflectionCommand command)
        {
            switch (command.Action)
            {
                case InflectionCommandAction.Append:
                    return new AppendCommandRunner(command);
                case InflectionCommandAction.Replace:
                    return new ReplaceCommandRunner(command);
                default:
                    throw new ArgumentException($"Invalid command action: {command.Action}");
            }
        }
    }
}