using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMVC.Models
{
    public class VeggieModel
    {
        public List<Veggie> veggies = null;
        public class Veggie
        {
            public string Name { get; set; }
            public int Qty { get; set; }
        }
        public VeggieModel()
        {
            veggies = new List<Veggie>();
        }
        public void AddVeggie(string Name,int Qty)
        {
            Veggie newVeggie = new Veggie();
            newVeggie.Name = Name;
            newVeggie.Qty = Qty;
            veggies.Add(newVeggie);
        }
    }
}