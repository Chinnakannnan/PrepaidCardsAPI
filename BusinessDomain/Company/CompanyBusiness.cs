using BusinessModel.Common;
using BusinessModel.Company;
using DataAccess.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Company
{
    public class CompanyBusiness : ICompanyBusiness
    {
        private readonly ICompanyDA _companyRepository;

        public CompanyBusiness(ICompanyDA companyRepositoryInstance)
        {
            _companyRepository = companyRepositoryInstance;
        }
        public StatusModel CreateCompanyDetails(CompanyModel companyModel)
        {
            return _companyRepository.CreateCompanyDetails(companyModel);
        }
        public StatusModel UpdateCompanyDetails(CompanyModel companyModel)
        {
            return _companyRepository.UpdateCompanyDetails(companyModel);
        }
        public List<CompanyModel> GetCompanyDetailsById(int id)
        {
            return _companyRepository.GetCompanyDetailsById(id);
        }
        public StatusModel DeleteCompanyDetailsById(int id)
        {
            return _companyRepository.DeleteCompanyDetailsById(id);
        }

    }
}
