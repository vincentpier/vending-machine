using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Capstone
{
    class Program
    {

        static void Main(string[] args)

        {
            Menu menu = new Menu();
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.MakingDictionary();
            Dictionary<string, Animal> itemList = vendingMachine.Items;

            while (true)
            {
                menu.DisplayOptions();
                int userChoice = menu.ParseUserChoice();
                while (userChoice == 1)
                {
                    menu.DisplayItem(itemList);
                    Console.WriteLine();
                    menu.DisplayOptions();
                    userChoice = menu.ParseUserChoice();

                }


                while (userChoice == 2)
                {

                    menu.DisplayPurchaseOptions(vendingMachine.CurrentMoneyProvided);
                    userChoice = menu.ParseUserChoice();
                    while (userChoice == 1)
                    {
                        vendingMachine.FeedMoney();
                        menu.DisplayPurchaseOptions(vendingMachine.CurrentMoneyProvided);
                        userChoice = menu.ParseUserChoice();
                    }
                    if (userChoice == 2)
                    {
                        menu.DisplayItem(itemList);
                        //userChoice = menu.ParseUserChoice();
                        Console.WriteLine();
                        Console.WriteLine($"Please enter code to choose your item.");
                        string itemChoice = Console.ReadLine().ToUpper();
                        vendingMachine.PurchaseItem(vendingMachine, itemList, itemChoice);
                        
                       
                    }

                }

                while (userChoice == 3)
                {
                    vendingMachine.ReturnChange(vendingMachine);
                    break;
                }
            }
        }
    }
}

