using BusinessModel.GST_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.PAN
{
    public interface IPanBusiness
    {

       public string getPanData(PanRequest PanDataRequest);
       public string getGSTData(PanRequest PanDataRequest);
    }
}
