using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Animal
    {
        public string Name { get; set; }

        public string SlotLocation { get; set; }

        public decimal Price { get; set; }

        public int Inventory { get; set; } = 5;

        public string AnimalType { get; set; }

        public Animal (string name, int inventory, string slotLocation, decimal price, string animalType)
        {
            Name = name;
            Inventory = inventory;
            SlotLocation = slotLocation;
            Price = price;
            AnimalType = animalType;
        }
        public Animal (int inventory)
        {
          
            Inventory = inventory;
        }

    }
}
