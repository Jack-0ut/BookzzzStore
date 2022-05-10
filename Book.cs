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
        public Book() { }
        public Book(string name,int quantity,string ageRange,double price)
        {
            this.name = name;
            this.quantity = quantity;
            this.ageRange = ageRange;
            this.price = price;
        }

        //public int id { get; private set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string ageRange { get; set; }
        public double price { get;  set; }
    }
}