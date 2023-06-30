using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace Capstone
{
    public class VendingMachine
    {
        Menu menu = new Menu();

        public decimal CurrentMoneyProvided { get; set; } = 0;


        public Dictionary<string, Animal> Items = new Dictionary<string, Animal>();



        public void FeedMoney()
        {
            Console.WriteLine($"Insert dollar bills of $1, $5, $10, $20");
            while (true)
            {


                string moneyFed = Console.ReadLine();
                decimal.TryParse(moneyFed, out decimal money);
                if (money != 1 && money != 5 && money != 10 && money != 20)
                {
                    Console.WriteLine("Legal tender only please.");
                    Console.WriteLine();
                }
                else
                {


                    CurrentMoneyProvided += money;
                    string logMessage = $"{DateTime.Now} FEED MONEY: ${money} ${CurrentMoneyProvided}";
                    LogPurchase(logMessage);
                    Console.WriteLine($"Current money provided: ${this.CurrentMoneyProvided}");
                    Console.WriteLine();
                    break;
                }

            }



        }
        public Dictionary<string, Animal> MakingDictionary()
        {
            string directory = @"C:\Users\Student\workspace\c-sharp-minicapstonemodule1-team2\";
            string fileName = "vendingmachine.csv";
            string fullPath = Path.Combine(directory, fileName);

            using (StreamReader sr = new StreamReader(fullPath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] newArray = line.Split("|");
                    if (newArray[3] == "Duck")
                    {
                        Duck duck = new Duck(newArray[3], 5, newArray[0], Decimal.Parse(newArray[2]), newArray[1]);



                        Items[newArray[0]] = duck;

                    }
                    if (newArray[3] == "Pony")
                    {

                        Pony pony = new Pony(newArray[3], 5, newArray[0], Convert.ToDecimal(newArray[2]), newArray[1]);


                        Items[newArray[0]] = pony;
                    }
                    if (newArray[3] == "Cat")
                    {
                        Cat cat = new Cat(newArray[3], 5, newArray[0], Convert.ToDecimal(newArray[2]), newArray[1]);


                        Items[newArray[0]] = cat;
                    }
                    if (newArray[3] == "Penguin")
                    {
                        Penguin penguin = new Penguin(newArray[3], 5, newArray[0], Convert.ToDecimal(newArray[2]), newArray[1]);


                        Items[newArray[0]] = penguin;
                    }

                }
                return Items;
            }

        }
        public void ReturnChange(VendingMachine vendingMachine)
        {
            decimal quarter = .25m;
            int quarterCount = 0;
            decimal dime = .10m;
            int dimeCount = 0;
            decimal nickel = .05m;
            int nickelCount = 0;
            decimal change = vendingMachine.CurrentMoneyProvided;

            Console.WriteLine($"Thank you for your purchase!");
            Console.WriteLine($"Your change is $ {change}");
            Console.WriteLine();

            quarterCount = (int)(change / quarter);
            change = change % quarter;

            if (change > 0)
            {
                dimeCount = (int)(change / dime);
                change = change % dime;
            }
            if (change > 0)
            {
                nickelCount = (int)(change / nickel);
                nickel = change % nickel;
            }
            Console.WriteLine("Dispensing:");
            if (quarterCount > 0)
            {
                Console.WriteLine($"{quarterCount} {(quarterCount == 1 ? "quarter" : "quarters")}");
                Console.WriteLine();
            }
            if (dimeCount > 0)
            {
                Console.WriteLine($"{dimeCount} {(dimeCount == 1 ? "dime" : "dimes")}");
                Console.WriteLine();
            }
            if (nickelCount > 0)
            {
                Console.WriteLine($"{nickelCount} {(nickelCount == 1 ? "nickel" : "nickels")}");
                Console.WriteLine();
            }

            // Console.WriteLine($"dispensing {quarterCount} quarters, " +
            //   $"{dimeCount} dimes, and {nickelCount} nickels");
            Console.WriteLine();

            string logMessage = $"{DateTime.Now} GIVE CHANGE: ${CurrentMoneyProvided} 0";
            vendingMachine.CurrentMoneyProvided = 0;
            LogPurchase(logMessage);
        }





        public void LogPurchase(string logMessage)
        {
            string directory = @"C:\Users\Student\workspace\c-sharp-minicapstonemodule1-team2\Capstone\";
            string filename = "Log.txt";
            string fullPath = Path.Combine(directory, filename);


            using (StreamWriter sw = new StreamWriter(fullPath, true))
            {

                {
                    sw.WriteLine(logMessage);
                }
            }
        }

        public void PurchaseItem(VendingMachine vendingMachine, Dictionary<string, Animal> itemList, string itemChoice)
        {
            //check if containskey before trying to access in dictionary 
            Animal valueKey = itemList[itemChoice];
            string itemName = valueKey.Name;
            decimal itemPrice = valueKey.Price;
            int itemInventory = valueKey.Inventory;
            string itemType = valueKey.AnimalType;
            decimal remainingBalance = vendingMachine.CurrentMoneyProvided - itemPrice;



            if (!itemList.ContainsKey(itemChoice) || itemInventory == 0 || itemPrice > vendingMachine.CurrentMoneyProvided)
            {
                Console.WriteLine($"Please try again.");
                return;

            }




            Console.WriteLine($"Good choice, enjoy your new {itemType}. It" +
               $" cost ${itemPrice}. Your remaining balance is ${remainingBalance}.");
            Console.WriteLine();

            //interface

            if (itemName == "Duck")
            {
                Console.WriteLine("Quack, Quack, Splash!");

            }
            if (itemName == "Penguin")
            {
                Console.WriteLine("Squawk, Squawk, Whee!");

            }
            if (itemName == "Cat")
            {
                Console.WriteLine("Meow, Meow, Meow!");

            }
            if (itemName == "Pony")
            {
                Console.WriteLine("Neigh, Neigh, Yay!");
            }


            vendingMachine.CurrentMoneyProvided -= itemPrice;
            valueKey.Inventory--;
            menu.DisplayItem(itemList);
            string logMessage = $"{DateTime.Now} {itemType} ${itemPrice} ${CurrentMoneyProvided}";
            LogPurchase(logMessage);
            Console.WriteLine($"Only {valueKey.Inventory} {itemType} remain.");
            Console.WriteLine();



        }
    }
}