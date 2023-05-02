using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Common
{
    public static class COMMON
    {
        public static string EMAILKEY = "9BE744B6F2379746";
        public static string KEY = "37974F8A5997A49B";
        public static string LKEY = "74F8A997A49B59A997A3749B53774F89";
    }
    public class StatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }

    }

    public class StatusResponseModel
    {
        public string statuscode { get; set; }
        public string statusdesc { get; set; }
        public string entityId { get; set; }

    }
}
