using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.GST_Validation
{

   
    public class panRequestModel
    {
        public panReqData essentials { get; set; }
        public string task { get; set; }

    }

    public class panReqData
    {
        public string number{ get; set; }
        public string gstin{ get; set; }
     

    }
    public class PanRequest
    {
        public string panNo { get; set; }
        public string gstNo { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fetch { get; set; }
        public string refId { get; set; }
    }


    public class PanReqData
    {
        public string number { get; set; }



    }
    public class GSTReqData
    {

        public string gstin { get; set; }


    }
    public class PanResponse
    {


        public string id { get; set; }
        public string ttl { get; set; }
        public string created { get; set; }
        public string userId { get; set; }



    }


}
