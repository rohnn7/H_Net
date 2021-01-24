using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace H_Net.Data_Contract
{
    [DataContract(Name ="Address")]
    public class Address
    {
        [DataMember(Name ="Addressid")]
        public int Addressid { get; set; }
        [DataMember(Name ="Add1")]
        public string Add1 { get; set; }
        [DataMember(Name ="Add2")]
        public string Add2 { get; set; }
        [DataMember(Name="Cityid")]
        public int Cityid { get; set; }
        [DataMember(Name ="Cityname")]
        public string Cityname { get; set; }
        [DataMember(Name ="Stateid")]
        public int Stateid { get; set; }
        [DataMember(Name ="Statename")]
        public string Statename { get; set; }
        [DataMember(Name ="Countryid")]
        public int Countryid { get; set; }
        [DataMember(Name ="CountryName")]
        public string CountryName { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }
    }
}