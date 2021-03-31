using System;

namespace vending_operator
{
    class VendingMachine
    {
        private int total_items; // Keeps track of active items
        public void Run()
        {
            int i = 1;
            do
            {
                Console.WriteLine("\n" + "1: Add item");
                Console.WriteLine("2: Show items");
                Console.WriteLine("3: Calculate total item value");
                Console.WriteLine("4: Calculate average item value");
                Console.WriteLine("5: Show items between a price range");
                Console.WriteLine("6: Show the most expensive item");
                Console.WriteLine("7: Show item costs sorted from low to high");
                Console.WriteLine("8: Print item state (fresh/stale)");
                Console.WriteLine("9: Taste an item");
                Console.WriteLine("10: Remove an item");
                Console.WriteLine("11: Automatically fill with items");
                Console.WriteLine("0: End program" + "\n");

                Console.Write("Choose a function: ");

                try
                {
                    i = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (i) // Switch statement to execute selected method
                    {
                        case 1:
                            add_item();
                            break;
                        case 2:
                            print_items();
                            break;
                        case 3:
                            print_value();
                            break;
                        case 4:
                            print_averageValue();
                            break;
                        case 5:
                            find_value();
                            break;
                        case 6:
                            max_value();
                            break;
                        case 7:
                            sort_items();
                            break;
                        case 8:
                            print_state();
                            break;
                        case 9:
                            eat_item();
                            break;
                        case 10:
                            remove_item();
                            break;
                        case 11:
                            add_auto();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Invalid input, choose one of the functions");
                            break;
                    }
                }
                catch (FormatException) // Catches all FormatExceptions.
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input, make sure to enter a valid input");
                    continue;
                }
                catch (Exception ex) // Catches all other exceptions and prints them to screen.
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                }
            }
            while (i != 0); // Ends program if user enters 0.

        }

        Item[] items;
        public VendingMachine() // Constructor
        {
            items = new Item[25]; // Creates an array of item objects
        }

        private void empty_check()
        {
            if (total_items == 0)
            {
                Console.WriteLine("There are no items in the vending machine.");
            }
        }

        private void add_item()
        {
            var loop = true;
            while (loop) // Loop to enable adding multiple items
            {
                if (total_items >= 5) // Checks if vending machine is full and skips if full. 
                {
                    Console.WriteLine("\n" + "The vending machine is full!");
                    loop = false;
                    continue;
                }

                string itemName;
                int itemPrice;
                string itemQuality = ""; // Initializing because they are within forloop codeblock
                string itemHealth = "";
                try
                {
                    Console.Write("Enter item name: ");
                    itemName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(itemName))
                    {
                        Console.WriteLine("Name cannot be left empty");
                        break;
                    }
                    Console.Write("Enter item price [0-100]: ");
                    itemPrice = int.Parse(Console.ReadLine());

                    for (int i = 0; i < 1; i++) // For loop instead of try catch to loop until there's a valid input. 
                    {
                        Console.Write("Enter item state [fresh] [stale]: ");
                        itemQuality = Console.ReadLine().ToLower();
                        if (itemQuality != "fresh" && itemQuality != "stale") // Checks if input isn't equal to fresh AND stale
                        {
                            Console.WriteLine("Invalid input, type in fresh or stale.");
                            i--; // Keeps the loop going 
                        }
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        Console.Write("Enter item healthiness [neutral] [healthy] [unhealthy]: ");
                        itemHealth = Console.ReadLine().ToLower();
                        if (itemHealth != "neutral" && itemHealth != "healthy" && itemHealth != "unhealthy")
                        {
                            Console.WriteLine("Invalid input, type in neutral, healthy or unhealthy.");
                            i--;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect format. Price only accepts numbers");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                Console.WriteLine("Item successfully added");
                items[total_items] = new Item(itemName, itemPrice, itemQuality, itemHealth); // Adds the item and properties. 
                total_items++;

                for (int i = 0; i < 1; i++)
                {
                    Console.Write("Would you like to add another item? [Y/N]: ");
                    string addAnother = Console.ReadLine().ToUpper();

                    if (addAnother == "NO" || addAnother == "N")
                    {
                        loop = false;
                    }
                    else if (addAnother == "YES" || addAnother == "Y")
                    {
                        loop = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, type in yes or no.");
                        i--;
                    }
                }
            }
        }

        private void add_auto()
        {
            if (total_items == 0)
            {
                string[] itemName = { "Bonk Atomic Energy", "Kexchoklad", "Bamsehonung", "Pootus Perfume", "Dr. Breen's Private Reserve" };
                int[] itemPrice = { 20, 10, 50, 100, 3 };
                string[] itemQuality = { "fresh", "stale", "fresh", "fresh", "stale" };
                string[] itemHealth = { "unhealthy", "unhealthy", "neutral", "healthy", "healthy" };

                for (int i = 0; i < 5; i++)
                {
                    items[total_items] = new Item(itemName[i], itemPrice[i], itemQuality[i], itemHealth[i]);
                    total_items++;
                    Console.WriteLine("Added {0}...", itemName[i]);
                }
            }
            else Console.WriteLine("The vending machine needs to be empty!");
        }

        private void print_items()
        {
            Console.WriteLine("You look through the window . . .");
            empty_check();
            for (int i = 0; i < total_items; i++) // Loops out all the existing items
            {
                Console.WriteLine(items[i]);
            }
            Console.WriteLine("There are [{0}/5] items in the vending machine", total_items);
        }

        private int calc_total_value()
        {
            Console.WriteLine("Calculating combined item value . . .");

            int totalValue = 0;
            for (int i = 0; i < total_items; i++)
            {
                totalValue += items[i].get_price(); // Retrieves items price props and adds & assigns to totalValue 
            }
            return totalValue;

        }

        private void print_value() // Method to print total value to screen.
        {
            Console.WriteLine("The total value of all items is {0}", calc_total_value());
            empty_check();
        }

        private int calc_average_value()
        {
            Console.WriteLine("Calculating average item value . . .");
            if (total_items > 0)
            {
                int totalPrice = 0;
                for (int i = 0; i < total_items; i++)
                {
                    totalPrice += items[i].get_price();
                }
                int averagePrice = totalPrice / total_items;
                return averagePrice;
            }
            else
            {
                return 0;
            }
        }

        private void print_averageValue()
        {
            Console.WriteLine("The average price of all items is {0}", calc_average_value());
            empty_check();
        }

        private void max_value()
        {
            int maxPrice = 0;
            for (int i = 0; i < total_items; i++)
            {
                int price = items[i].get_price();

                if (maxPrice < price) // Compares all prices and assigns the largest one
                {
                    maxPrice = price;
                }
            }
            Console.WriteLine("The most expensive item in the machine costs: {0}", maxPrice);
            empty_check();
        }

        private void find_value()
        {
            void ClearLine()
            {
                Console.SetCursorPosition(0, 1);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, 1);
            }

            if (total_items > 0)
            {
                Console.WriteLine("Choose search interval");
                Console.Write("Enter min price: ");
                int minPrice = int.Parse(Console.ReadLine());
                ClearLine();
                Console.Write("Enter max price: ");
                int maxPrice = int.Parse(Console.ReadLine());
                ClearLine();
                string intelligenceCheck = (minPrice > maxPrice) ? "your intelligence bestows us all" : "";
                Console.WriteLine("Showing items between {0}-{1} . . . {2}\n", minPrice, maxPrice, intelligenceCheck);
                for (int i = 0; i < total_items; i++)
                {
                    int price = items[i].get_price();

                    if (minPrice <= price && price <= maxPrice) // Looks for items with the price between the minimun and maximum price interval
                    {
                        Console.WriteLine("Slot {2} contains \"{0}\", it costs {1}", items[i].get_name(), price, i);
                    }
                }
            }
            empty_check();
        }

        private void sort_items()
        {
            if (total_items > 0)
            {
                int[] prices = new int[total_items]; // New array for the prices from all items
                for (int i = 0; i < total_items; i++)
                {
                    prices[i] = items[i].get_price(); // Add all prices from the items into the new prices array
                }

                int temp = 0;
                for (int sort = 0; sort < prices.Length; sort++) // Bubble sort
                {
                    for (int i = 0; i < prices.Length - 1; i++)
                    {
                        if (prices[i] > prices[i + 1])
                        {
                            temp = prices[i + 1];
                            prices[i + 1] = prices[i];
                            prices[i] = temp;
                        }
                    }
                }
                Console.WriteLine("Sorted prices from low to high: ");
                for (int i = 0; i < prices.Length; i++)
                {
                    Console.Write(prices[i] + ", ");
                }
                Console.WriteLine();
            }
            empty_check();
        }

        private void print_state()
        {
            for (int i = 0; i < total_items; i++)
            {
                Console.WriteLine("Slot: {0} - Quality: {1}", i, items[i].get_quality()); // Shows freshness of each item
            }
            empty_check();
        }

        private void eat_item()
        {
            if (total_items > 0)
            {
                Console.WriteLine("ITEM LIST");
                for (int i = 0; i < total_items; i++)
                {
                    Console.WriteLine("Slot: {0}, name: {1}, quality: {2}, price: {3}, health: {4}.", i, items[i].get_name(), items[i].get_quality(), items[i].get_price(), items[i].get_health());
                }
                Console.Write("Select the position of the item you want to eat: ");
                int position = int.Parse(Console.ReadLine());
                if (position < 0 || position > total_items - 1)
                {
                    Console.WriteLine("There is no item there. You take a big bite out of the vending machine. Please select an occupied slot");
                }
                else
                {
                    var selected = items[position];

                    /*
                    0-10 AND unhealthy OR neutral		= fills you with joy
                    0-10 AND healthy 			        = it's truly disgusting
                    10-60 AND unhealthy		            = it tastes magnificent
                    10-60 AND neutral OR unhealthy     	= why people spend money on this is beyond you
                    60-100 AND stale		            = you feel scammed
                    60-100 AND fresh                    = expensive but at least it tastes fresh
                    */

                    if (selected.get_price() >= 0 && selected.get_price() <= 10 && selected.get_health() == "unhealthy" || selected.get_health() == "neutral")
                    {
                        Console.WriteLine("You taste the {0}. It fills you with joy.", selected.get_name());
                    }
                    else if (selected.get_price() >= 0 && selected.get_price() <= 10 && selected.get_health() == "healthy")
                    {
                        Console.WriteLine("You taste the {0}. It's truly disgusting", selected.get_name());
                    }
                    else if (selected.get_price() >= 10 && selected.get_price() <= 60 && selected.get_health() == "unhealthy")
                    {
                        Console.WriteLine("You taste the {0}. It tastes magnificent", selected.get_name());
                    }
                    else if (selected.get_price() >= 10 && selected.get_price() <= 60 && selected.get_health() == "healthy" || selected.get_health() == "neutral")
                    {
                        Console.WriteLine("You taste the {0}. Why people spend money on this is beyond you", selected.get_name());
                    }
                    else if (selected.get_price() >= 60 && selected.get_price() <= 100 && selected.get_quality() == "stale")
                    {
                        Console.WriteLine("You taste the {0}. You feel scammed", selected.get_name());
                    }
                    else if (selected.get_price() >= 60 && selected.get_price() <= 100 && selected.get_quality() == "fresh")
                    {
                        Console.WriteLine("You taste the {0}. Expensive but at least it tastes fresh", selected.get_name());
                    }
                    else
                        Console.WriteLine("You taste the {0}. You feel empty, as if the programmer forgot about you", selected.get_name());
                }
            }
            empty_check();
        }

        private void remove_item()
        {
            if (total_items > 0)
            {
                Console.WriteLine("ITEM LIST");
                for (int i = 0; i < total_items; i++)
                {
                    Console.WriteLine("Slot: {0}, name: {1}, quality: {2}, price: {3}, health: {4}.", i, items[i].get_name(), items[i].get_quality(), items[i].get_price(), items[i].get_health());
                }
                Console.Write("Select the position of the item you want to remove: ");
                int position = int.Parse(Console.ReadLine()); // Gets the position of the item removed

                if (position < 0 || position > total_items - 1)
                {
                    Console.WriteLine("There is no item there, select an occupied slot");
                }
                else
                {
                    total_items--;
                    for (int i = position; i < total_items; i++) // Sets all the items under the removed one to a position above in the array
                    {
                        items[i] = new Item(items[i + 1].get_name(), items[i + 1].get_price(), items[i + 1].get_quality(), items[i + 1].get_health());

                    }
                    Console.WriteLine("Item successfully removed");
                }
            }
            empty_check();
        }
    }

    class Item // Stores item properties
    {
        private string Name;
        private int Price;
        private string Quality;
        private string Health;

        public Item(string name, int price, string quality, string health) // Constructor
        {
            Name = name;
            if (price < 1) // Checks for unreasonable numbers and corrects them
                Price = 1;
            else if (price > 100)
                Price = 100;
            else
                Price = price;
            Quality = quality;
            Health = health;
        }
        public int get_price() // These allow access to the item class properties while keeping them private, good OOP practice. 
        {
            return Price;
        }
        public string get_name()
        {
            return Name;
        }
        public string get_quality()
        {
            return Quality;
        }
        public string get_health()
        {
            return Health;
        }
        public override string ToString() // Used to return a custom string representation of item object, used for print_items.
        {
            return string.Format("There's an item called {0} and it costs {1}. It looks kinda {2}. It is quite {3}", Name, Price, Quality, Health);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your vending machine, operator!");
            var myMachine = new VendingMachine();            // Create instance of vending machine
            myMachine.Run();                      // Run instance
            Console.Write("Goodbye! Press any key to continue...");
            Console.ReadKey(true);
        }
    }

}