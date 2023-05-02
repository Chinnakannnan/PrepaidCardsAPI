using System;
using System.Collections.Generic;
/***************************************************************************
'* Application  	:   Yappay WCF
'* Module       	:   Yappay Encrypted Response
'* File name    	:   YappayEncryptedResponse.cs
'* Purpose      	:   Data Members of the Yappay Response
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
    public class YappayEncryptedResponse
    {
        [DataMember(Name = "headers", IsRequired = false)]
        public YappayEncryptedResponseHeaders headers;
        [DataMember(Name = "body", IsRequired = false)]
        public string body;
        [DataMember(Name = "detailMessage", IsRequired = false)]
        public string detailMessage;
        [DataMember(Name = "cause", IsRequired = false)]
        public string cause;
        [DataMember(Name = "stackTrace", IsRequired = false)]
        public string stackTrace;
    }
    public class YappayEncryptedResponseHeaders
    {
        [DataMember(Name = "hash", IsRequired = false)]
        public string hash;
        [DataMember(Name = "entity", IsRequired = false)]
        public string entity;
        [DataMember(Name = "refNo", IsRequired = false)]
        public string refNo;
        [DataMember(Name = "key", IsRequired = false)]
        public string key;
    }
}
