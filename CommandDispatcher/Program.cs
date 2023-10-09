using Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEventDispatcher
{
    class Program
    {
        private static bool keepRunning = true;
        private static SomeService serviceProvider;

        static void Main(string[] args)
        {
            serviceProvider = new SomeService();

            Configure();

            // Attach Ctrl+C event handler
            Console.CancelKeyPress += HandleCloseConsoleKeyEvent();

            while (keepRunning)
            {
                CommandsMenu.DoWork();
            }

            Console.WriteLine("Application terminated gracefully.");
        }


        public static void Configure()
        {
            // Add prompt
            // Add methods related to keys
            var mainMenu = new CommandsMenu(
                "Main: Press 'a' to execute command, 'b' to execute command and switch to tetriary menu, 's' to switch to secondary.",
                CommandsMenu.TopMostMenu,
                ('a', serviceProvider.PodajWiek),
                ('b', serviceProvider.DodajDwieZmienne),
                ('s', serviceProvider.ThisIsCommandUnderKeyS)
            );

            // this is example of pushing the parameter to the command method
            var secondaryMenu = new CommandsMenu(
                "Secondary: You're in the secondary menu. Press 'x' to go to tertiary.  Backspace to go to previous. ",
                mainMenu,
                ('x', () => { serviceProvider.ThisIsCommandUnderKeyX(5); })
            );

            // this is example of useless menu with no action xD
            var tertiaryMenu = new CommandsMenu(
                "Tertiary: This is a tertiary menu. Backspace to go to previous menu. ",
                secondaryMenu
            );


            // Set Menus Workflows - from which menu you will go to which menu using which 
            mainMenu.NextMenusDict.Add('s', secondaryMenu);

            // after the action the menu will be also moved to teriatryMenu
            mainMenu.NextMenusDict.Add('b', tertiaryMenu);
            secondaryMenu.NextMenusDict.Add('x', tertiaryMenu);
        }


        private static ConsoleCancelEventHandler HandleCloseConsoleKeyEvent()
        {
            return delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;  // Prevent the process from terminating
                keepRunning = false;
            };
        }

    }

  
   
}
