using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H_Net.Data_Contract
{
    [DataContract(Name = "Photo")]
    public class photo
    {
        [DataMember(Name = "PhotoId")]
        public int PhotoID { get; set; }
        [DataMember(Name = "PhotoAddress")]
        public string PhotoAddress { get; set; }
        [DataMember(Name = "Datephoto")]
        public DateTime Datephoto {get; set;}
        [DataMember(Name ="Latitude")]
        public decimal Latitude { get; set; }
        [DataMember(Name = "Latdirection")]
        public decimal Latdirection { get; set; }
        [DataMember(Name = "Longitude")]
        public decimal Longitude { get; set; }
        [DataMember(Name = "Longdirection")]
        public decimal Longdirection { get; set; }
        [DataMember(Name ="Result")]
        public int Result { get; set; }
        [DataMember(Name = "UserId")]
        public int UserId { get; set; }
        [DataMember(Name = "PhotoString")]
        public string PhotoString { get; set; }


    }
}