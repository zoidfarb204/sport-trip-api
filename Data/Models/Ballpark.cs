using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Newtonsoft.Json;

namespace Data
{
    public class Ballpark
    {
        public string name { get; set; }
        public string type { get; set; }
        public List<Feature> features { get; set; }
        
        public static async Task<Ballpark> GetParks(){
            using (StreamReader r = new StreamReader("ballparks.json"))
            {
                return JsonConvert.DeserializeObject<Ballpark>(await r.ReadToEndAsync());
            }
        }
    }
    
    
}