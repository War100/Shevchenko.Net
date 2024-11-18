namespace Shevchenko.WordDeclension
{
    public class ReplaceCommandRunner : IInflectionCommandRunner
    {
        private readonly InflectionCommand _command;

        public ReplaceCommandRunner(InflectionCommand command)
        {
            _command = command;
        }

        /// <summary>
        /// Replaces the given value with the command value.
        /// Returns a new value.
        /// </summary>
        public string Exec(string value)
        {
            return _command.Value;
        }
    }
}