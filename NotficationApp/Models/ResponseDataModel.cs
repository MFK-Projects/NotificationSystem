using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotficationApp.Models
{
    public class ResponseDataModel
    {
     ///   public string Odata_Contex { get; set; }  
     ///    
        public List<Value> Value { get; set; }
    }
    public class Value
    {

        public string domainname { get; set; }
        public string fullname { get; set; }
        public int identityid { get; set; }
        public string systemuserid { get; set; }
        public string ownerid { get; set; }
    }
}
