using System;
using System.IO;

namespace notPasswords
{
    class Commands {
        public static void help() {
            Console.WriteLine("Type...");
            Console.WriteLine("display - to see what you can search for");
            Console.WriteLine("search - to enter search mode, then type the number, of the displayed name");
            Console.WriteLine("add - to add new informations");
            Console.WriteLine("quit - to exit the program");
        }
        public static void display() {
            FileStream fs_display = new FileStream("passwords.txt", FileMode.Open);
            StreamReader sr_display = new StreamReader(fs_display);

            string row;
            string[] splitted_row;
            int index = 0;

            while ((row = sr_display.ReadLine()) != null) {
                splitted_row = row.Split('>');
                if (splitted_row[0] == "a")
                {
                    ++index;
                    Console.WriteLine(index + " " + splitted_row[1]);
                }
            }
            sr_display.Close();
            fs_display.Close();
        }
        public static void search() {
            FileStream fs_search = new FileStream("passwords.txt", FileMode.Open);
            StreamReader sr_search = new StreamReader(fs_search);

            string search_input = Console.ReadLine();
            bool found = false;

            string row;
            string[] splitted_row;
            int index = 0;
            
            while ((row = sr_search.ReadLine()) != null)
            {
                splitted_row = row.Split('>');
                if (splitted_row[0] == "a")
                {
                    ++index;
                    if (int.Parse(search_input) == index)
                    {
                        Console.WriteLine(splitted_row[1]);
                        while (found == false)
                        {
                            row = sr_search.ReadLine();
                            if (row == null)
                            {
                                found = true;
                                break;
                            }
                            splitted_row = row.Split('>');
                            if (splitted_row[0] == "a")
                            {
                                found = true;
                            }
                            else
                            {
                                Console.WriteLine(row);
                            }
                        }
                        if (found == true)
                            break;
                    }
                }
            }
            if (found == false)
                Console.WriteLine("The number can't be found.");

            sr_search.Close();
            fs_search.Close();
        }
        public static void add() {
            FileStream fs_add = new FileStream("passwords.txt", FileMode.Append);
            StreamWriter sw_add = new StreamWriter(fs_add);

            string add_input = "";

            Console.WriteLine("Name:");
            add_input = Console.ReadLine();
            sw_add.WriteLine("a>" + add_input);
            Console.WriteLine("informations you wanna type, for example: password: iLikeVegetable6000");
            add_input = Console.ReadLine();
            sw_add.WriteLine(add_input);

            bool add_more = true;
            while (add_more == true) {
                Console.WriteLine("Do you want to add more informartions for the current subject? [y / n]");
                string add_more_input = Console.ReadLine();
                if (add_more_input == "y")
                {
                    Console.WriteLine("type informations, for example: password: iLikeVegetable6000");
                    add_input = Console.ReadLine();
                    sw_add.WriteLine(add_input);
                }
                else add_more = false;
            }
            Console.WriteLine("New data succesfully updated! Yay");


            sw_add.Close();
            fs_add.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please type what you are looking for.");
            Console.WriteLine("If you need help, type: help.");
            string input = Console.ReadLine();
            while (input != "quit") {
                if (input == "help")
                {
                    Commands.help();
                }
                else if (input == "display")
                {
                    Commands.display();
                }
                else if (input == "search")
                {
                    Commands.search();
                }
                else if (input == "add") {
                    Commands.add();
                }
                else Console.WriteLine("Sorry, I don't understand that.");
                Console.WriteLine("Please type..");
                input = Console.ReadLine();
            }
            Console.WriteLine("\nPress something 2x to Exit...");
            Console.ReadKey();
        }
    }
}
