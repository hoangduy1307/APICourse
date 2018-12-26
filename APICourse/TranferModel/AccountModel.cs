using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class AccountModel
    {
        public int Idteacher { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Status { get; set; }

        public AccountModel() { }
        public AccountModel(Account ac)
        {
            this.Idteacher = ac.Idteacher;
            this.Username = ac.Username;
            this.Password = ac.Password;
            this.Status = ac.Status;
        }
    }
}
