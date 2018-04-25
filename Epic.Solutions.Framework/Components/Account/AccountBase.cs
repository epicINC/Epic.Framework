using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.Components
{
    public abstract class AccountBase : IAccount
    {
        public string Account
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string CA
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Salt
        {
            get;
            set;
        }

        public AccountPasswordEncryptType PasswordEncryptType
        {
            get;
            set;
        }

        public AccountCredentialType CredentialType
        {
            get;
            set;
        }
    }
}
