using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H_Net.Data_Contract
{
    [DataContract(Name ="User") ]
    public class User
    {
        [DataMember(Name ="UserId")]
        public int UserId { get; set; }
        [DataMember(Name ="Name")]
        public string Name { get; set; }
        [DataMember(Name = "Age")]
        public int Age { get; set; }
        [DataMember(Name ="Volunteer")]
        public int Volunteer { get; set; }
        [DataMember(Name = "SocialPoint")]
        public int SocialPoint { get; set; }
        [DataMember(Name ="Username")]
        public string Username { get; set; }
        [DataMember(Name ="Password")]
        public string Password { get; set; }
    }
}