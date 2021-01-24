using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace H_Net.Data_Contract
{
    [DataContract(Name ="Levels")]
    public class Levels
    {
        [DataMember(Name ="Levelid")]
        public int Levelid { get; set; }
        [DataMember(Name ="SocialPointRequired")]
        public int SocialPointRequired { get; set; }
    }
}