using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    public interface IAuth
    {
        Task<string> Authenticate(string email, string password);
        Task<string> Register(string email, string password);

        string AuthenticatedToken();

        bool SignOut();

        bool Authenticated();
    }
}
