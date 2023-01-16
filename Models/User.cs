using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApp.Models.Abstract;

namespace TaxiApp.Model
{
    [AddINotifyPropertyChangedInterface]

    public class User:Entity
    {
  
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<History> HistoryList { set; get; }
                   = new List<History>();
    }
}
