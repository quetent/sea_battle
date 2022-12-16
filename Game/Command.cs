namespace Game
{
    public readonly struct Command
    {
        private readonly CommandsEnum _type;
        public CommandsEnum Type { get { return _type; } }

        public Command(CommandsEnum type)
        {
            _type = type;
        }

        public override string ToString()
        {
            return _type.ToString();
        }
    }
}
