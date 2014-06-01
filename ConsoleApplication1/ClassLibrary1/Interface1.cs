using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Service
{
    [ServiceContract]
    public interface Interface
    {
        [OperationContract]
        string test(string g);
        [OperationContract]
        bool CheckLogin(string login, string pass);
        [OperationContract]
        bool AddLogin(string login, string pass);

    }
}
