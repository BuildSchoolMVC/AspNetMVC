using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models {
    public class CountyModels {
        public string Name;
        public List<string> Districts;
        public CountyModels(string name, List<string> districts) {
            Name = name;
            Districts = districts;
        }
    }
}