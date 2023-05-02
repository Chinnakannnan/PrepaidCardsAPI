using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Common
{
    public class ConfigModel
    {
        public string fromAddr { get; set; }
        public string smtpAddr { get; set; }
        public string smtpPort { get; set; }
        public string statuscode { get; set; }
        public string mailSecret { get; set; }
        public string lstrImageURL { get; set; }

    }
}
