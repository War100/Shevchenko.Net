namespace Shevchenko.WordDeclension
{
    public class AppendCommandRunner : IInflectionCommandRunner
    {
        private readonly InflectionCommand _command;

        public AppendCommandRunner(InflectionCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Appends the command's value to the given value.
        /// Returns a new modified value.
        /// </summary>
        public string Exec(string value)
        {
            return value + _command.Value;
        }
    }
}