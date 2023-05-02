using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Login;

namespace BusinessDomain.Queries.Login
{
    //public record GetLoginDataByQuery(string userName,string password):IRequest<LoginModel>;
    //public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeModel>;
    public class GetLoginDataByQuery : IRequest<LoginModel>
    {
        public GetLoginDataByQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
       
    public record GetAllUsersByQuery() : IRequest<List<LoginModel>>;
}
