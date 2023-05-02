using BusinessModel.Common;
using BusinessModel.Company;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Company
{
    public class CompanyDA : DapperRepository<object>, ICompanyDA
    {
        public CompanyDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public StatusModel CreateCompanyDetails(CompanyModel companyModel)
        {
            var dParam = new DynamicParameters();
            //dParam.Add("@ID", companyModel.Id);
            dParam.Add("@NAME", companyModel.Name);
            dParam.Add("@OWNERNAME", companyModel.Ownername);
            dParam.Add("@EMAIL", companyModel.Email);
            dParam.Add("@ALTERNATEEMAIL", companyModel.AlternateEmail);
            dParam.Add("@MOBILE", companyModel.Mobile);
            dParam.Add("@ALTERNATEMOBILE", companyModel.AlternateMobile);
            dParam.Add("@WEBSITEURL", companyModel.Websiteurl);
            dParam.Add("@ANDROIDURL", companyModel.Androidurl);
            dParam.Add("@LOGO", companyModel.Logo);
            dParam.Add("@ADDRESS", companyModel.Address);
            dParam.Add("@COPYRIGHT", companyModel.Copyright);
            dParam.Add("@FaceBook", companyModel.FaceBook);
            dParam.Add("@WhastApp", companyModel.WhastApp);
            dParam.Add("@Instagram", companyModel.Instagram);
            dParam.Add("@Twiter", companyModel.Twiter);
            dParam.Add("@Youtube", companyModel.Youtube);
            dParam.Add("@BankName", companyModel.BankName);
            dParam.Add("@ACName", companyModel.ACName);
            dParam.Add("@ACType", companyModel.ACType);
            dParam.Add("@ACNumber", companyModel.ACNumber);
            dParam.Add("@IFSC", companyModel.IFSC);
            dParam.Add("@MICRCode", companyModel.MICRCode);
            dParam.Add("@ProfileAmount", companyModel.ProfileAmount);
            dParam.Add("@Feviconicon", companyModel.Feviconicon);
            dParam.Add("@signature", companyModel.signature);
            dParam.Add("@MemberID", companyModel.MemberID);
            dParam.Add("@BodyColor", companyModel.BodyColor);
            dParam.Add("@LeftColor", companyModel.LeftColor);
            dParam.Add("@HeaderColor", companyModel.HeaderColor);

            var result = QuerySP<StatusModel>("sp_companyAddEdit", dParam).FirstOrDefault();
            return result;
        }

        public StatusModel UpdateCompanyDetails(CompanyModel companyModel)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@ID", companyModel.Id);
            dParam.Add("@NAME", companyModel.Name);
            dParam.Add("@OWNERNAME", companyModel.Ownername);
            dParam.Add("@EMAIL", companyModel.Email);
            dParam.Add("@ALTERNATEEMAIL", companyModel.AlternateEmail);
            dParam.Add("@MOBILE", companyModel.Mobile);
            dParam.Add("@ALTERNATEMOBILE", companyModel.AlternateMobile);
            dParam.Add("@WEBSITEURL", companyModel.Websiteurl);
            dParam.Add("@ANDROIDURL", companyModel.Androidurl);
            dParam.Add("@LOGO", companyModel.Logo);
            dParam.Add("@ADDRESS", companyModel.Address);
            dParam.Add("@COPYRIGHT", companyModel.Copyright);
            dParam.Add("@FaceBook", companyModel.FaceBook);
            dParam.Add("@WhastApp", companyModel.WhastApp);
            dParam.Add("@Instagram", companyModel.Instagram);
            dParam.Add("@Twiter", companyModel.Twiter);
            dParam.Add("@Youtube", companyModel.Youtube);
            dParam.Add("@BankName", companyModel.BankName);
            dParam.Add("@ACName", companyModel.ACName);
            dParam.Add("@ACType", companyModel.ACType);
            dParam.Add("@ACNumber", companyModel.ACNumber);
            dParam.Add("@IFSC", companyModel.IFSC);
            dParam.Add("@MICRCode", companyModel.MICRCode);
            dParam.Add("@ProfileAmount", companyModel.ProfileAmount);
            dParam.Add("@Feviconicon", companyModel.Feviconicon);
            dParam.Add("@signature", companyModel.signature);
            dParam.Add("@MemberID", companyModel.MemberID);
            dParam.Add("@BodyColor", companyModel.BodyColor);
            dParam.Add("@LeftColor", companyModel.LeftColor);
            dParam.Add("@HeaderColor", companyModel.HeaderColor);

            var result = QuerySP<StatusModel>("sp_companyAddEdit", dParam).FirstOrDefault();
            return result;
        }

        public List<CompanyModel> GetCompanyDetailsById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", id);

            List<CompanyModel> result = QuerySP<CompanyModel>("sp_GetCompanyDetailsbyId", dParam).ToList();

            return result;
        }

        public StatusModel DeleteCompanyDetailsById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", id);

            var result = QuerySP<StatusModel>("sp_DeleteCompanyDetails", dParam).FirstOrDefault();

            return result;
        }
    }
}
