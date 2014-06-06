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
        int CheckLogin(string login, string pass);
        [OperationContract]
        int AddLogin(string login, string pass);
        [OperationContract]
        string ErrorDescription(int k);
    }
}
