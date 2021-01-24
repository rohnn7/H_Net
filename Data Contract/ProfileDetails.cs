using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H_Net.Data_Contract
{
    [DataContract(Name ="ProfileDetails")]
    public class ProfileDetails
    {
        [DataMember(Name ="Name")]
        public string Name { get; set; }
        [DataMember(Name = "SocialPoints")]
        public int SocialPoints { get; set; }
        [DataMember(Name = "Address1")]
        public string Address1 { get; set; }
        [DataMember(Name = "Address2")]
        public string Address2 { get; set; }
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "State")]
        public string State { get; set; }
        [DataMember(Name = "Country")]
        public string Country { get; set; }

        [DataMember(Name = "Username")]
        public string Username { get; set; }
    }
}