using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H_Net.Data_Contract
{
    [DataContract(Name ="Contact")]
    public class contact
    {
        [DataMember(Name ="ContactId")]
        public int  ContactId { get; set; }
        [DataMember(Name ="Mobile")]
        public string Mobile { get; set; }
        [DataMember(Name ="Email")]
        public string Email { get; set; }
        [DataMember(Name = "UserId")]
        public int UserId { get; set; }
    }
}