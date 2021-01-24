using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace H_Net.Data_Contract
{
    public class Param
    {
        [DataMember(Name ="Address")]
        public Address Address { get; set; }
        [DataMember(Name ="Contact")]
        public contact Contact { get; set; }
        [DataMember(Name ="Levels")]
        public Levels Levels { get; set; }
        [DataMember(Name="Photo")]
        public photo Photo { get; set; }
        [DataMember(Name ="User")]
        public User User { get; set; }
        [DataMember(Name ="ProfileDetails")]
        public ProfileDetails ProfileDetails { get; set; }

    }
}