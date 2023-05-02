using APIService;
using BusinessModel.GST_Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.PAN
{
   public class PanBusiness:IPanBusiness
    {

        public string getPanData(PanRequest PanDataRequest)
        {
            PANProvider panProvider = new PANProvider();
            var result = panProvider.getPanData(PanDataRequest);
            return result;
        } 
        public string getGSTData(PanRequest PanDataRequest)
        {
            PANProvider panProvider = new PANProvider();
            var result = panProvider.getGSTData(PanDataRequest);
            return result;
        }
       
    }
}
