using ConsoleEventDispatcher;

namespace Menu
{
    public class CommandsMenu
    {
        private int id;
        public static List<CommandsMenu> CommandsMenus { get; set; } = new List<CommandsMenu>();
        public static CommandsMenu CurrentCommandsMenu { get; set; }
        public static readonly CommandsMenu TopMostMenu = new CommandsMenu(0);
        private CommandsMenu(int? id = null) {

            this.id = id.HasValue ? id.Value : new Random().Next(1, 10000);
        }


        public CommandsMenu(string prompt, CommandsMenu previousMenu, params (char key, Action action)[] actions)
        {

            this.Prompt = prompt;
            
            this.PreviousMenu = previousMenu != TopMostMenu ? previousMenu: null;

            this.ActionMappings = actions.ToDictionary(kvp => kvp.key, kvp => kvp.action);

            CommandsMenus.Add(this);

            if (CommandsMenu.CurrentCommandsMenu == null)
            {
                CommandsMenu.CurrentCommandsMenu = this;
            }
        }
        public string Prompt { get; set; }
        public Dictionary<char, Action> ActionMappings { get; set; }
        public Dictionary<char, CommandsMenu> NextMenusDict { get; set; } = new Dictionary<char, CommandsMenu>();
        public CommandsMenu PreviousMenu { get; internal set; }

        public void Invoke(char key)
        {
            if (ActionMappings.ContainsKey(key))
            {
                ActionMappings[key]();


                // If the invoked function returns a non-null menu, set it as the current menu
                if (NextMenusDict.Keys.Contains(key))
                {
                    CommandsMenu.CurrentCommandsMenu = NextMenusDict[key];
                    CommandsMenu.CurrentCommandsMenu.PreviousMenu = this;
                }
            }
            // If the key isn't in the dictionary, do nothing.
        }

        public static void DoWork()
        {
            Console.Write(CommandsMenu.CurrentCommandsMenu.Prompt);
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Backspace && CommandsMenu.CurrentCommandsMenu.PreviousMenu != null)
            {
                Console.WriteLine();
                CommandsMenu.CurrentCommandsMenu = CommandsMenu.CurrentCommandsMenu.PreviousMenu;
                return;
            }

            char key = keyInfo.KeyChar;
            Console.WriteLine();

            CommandsMenu.CurrentCommandsMenu.Invoke(key);
        }
    }
}
