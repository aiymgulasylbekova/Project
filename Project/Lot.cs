using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Lot
    {
        public static int nextID = 1;

        public int ID { get; }   //Id
        public string Name { get; }//name
        public string Description { get; }// описание
        public double StartingPrice { get; } //начальная цена
        public double CurrentBid { get; set; } //текущая ставка
        public string WinningBidder { get; set; }  //победитель
        public DateTime EndTime { get; set; }
        

        public Lot(string name, string description, double startingPrice, TimeSpan auctionDuration)  //конструктор класса лот
        {
            ID = nextID++;
            Name = name;
            Description = description;
            StartingPrice = startingPrice;
            CurrentBid = startingPrice;
            WinningBidder = null;
            EndTime = DateTime.Now + auctionDuration;
        }

        public void PlaceBid(string bidder, double amount) //метод обновляет текущую ставку
        {
            //bidder-пользователь сделавший ставку, amount-значение которое он вел
            CurrentBid = amount; //новое значение для текущей ставки
            WinningBidder = bidder;//содержит текущую ставку сделавщего пользователь
        }
    }
}
