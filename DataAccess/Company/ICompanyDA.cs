using BusinessModel.Common;
using BusinessModel.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Company
{
    public interface ICompanyDA
    {
        StatusModel CreateCompanyDetails(CompanyModel companyModel);
        StatusModel UpdateCompanyDetails(CompanyModel companyModel);
        List<CompanyModel> GetCompanyDetailsById(int id);
        StatusModel DeleteCompanyDetailsById(int id);
    }
}
