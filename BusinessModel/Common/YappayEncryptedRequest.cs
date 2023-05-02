/***************************************************************************
'* Application  	:   Yappay WCF
'* Module       	:   Yappay Encrypted Request
'* File name    	:   YappayEncryptedRequest.cs
'* Purpose      	:   Data Members of the Yappay Request
'*                     
'* Author       	:   
'* Date Created 	:   
'* End Created 	    :   
'* Modified History	:   
'*==========================================================================
'* S.No  RFC No/Bug ID  Date    Author      Description
'* 
'***************************************************************************/
using System.Runtime.Serialization;

namespace BusinessModel.Common
{
    /// <summary>
    /// Created by sriramk on 17-04-2015.
    /// </summary>
    [DataContract]
    public class YappayEncryptedRequest
    {

        [DataMember(Name = "token", IsRequired = true)]
        public string token; // body, business pvt Key
        [DataMember(Name = "body", IsRequired = true)]
        public string body; //body, key
        [DataMember(Name = "entity", IsRequired = true)]
        public string entity; // entityId, m2p Pub Key
        [DataMember(Name = "key", IsRequired = true)]
        public string key; // M2P Pub Key, key
        [DataMember(Name = "refNo", IsRequired = true)]
        public string refNo;
    }
}
