using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.User
{
    public class UserModel
    {

        public string uniquevalue { get; set; }
        public string customerId { get; set; }
        public string shopname { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string mobileNo { get; set; }
        public string emailAddress { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pincode { get; set; }
        public string gst { get; set; }
        public string pan { get; set; }
        public string userPhotoURL { get; set; }
        public string idProofType { get; set; }
        public string idProofNumber { get; set; }
        public string idProofURL { get; set; }
        public string addressProofType { get; set; }
        public string addressProofNumber { get; set; }
        public string addressProofURL { get; set; }
        public string password { get; set; }
        public int tpin { get; set; }
        public int usertype { get; set; }
        public int commissiontype { get; set; }
        public int agenttype { get; set; }
        public string createdby { get; set; }
        public int status { get; set; }
        public int tpinstatus { get; set; }
        public int kycstatus { get; set; }
        public int passwordcount { get; set; }
        public DateTime passwordexpiry { get; set; }
        public string cdatetime { get; set; }
        public string mdatetime { get; set; }
        public string modifiedby { get; set; }
        public string remarks { get; set; }
        public string aesKey { get; set; }
        public string consumerkey { get; set; }
        public string consumersecret { get; set; }
        public string vaccountid { get; set; }
        public string vbankname { get; set; }
        public string vifsccode { get; set; }
        public string mapUserCode { get; set; }
        public string mapusertype { get; set; }

    }
}
