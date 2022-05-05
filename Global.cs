using System;
using System.Collections.Generic;
namespace BookStore
{
    public class Book
    {
        /*int id;
        string name;
        int quantity;
        string ageRange;
        double price;*/
        public Book(int id,string name,int quantity,string ageRange,double price)
        {
            this.id = id;
            this.name = name;
            this.quantity = quantity;
            this.ageRange = ageRange;
            this.price = price;
        }

        public int id { get; private set; }
        public string name { get; private set; }
        public int quantity { get; private set; }
        public string ageRange { get; private set; }
        public double price { get; private set; }
    }
}