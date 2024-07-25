using System.Collections.Generic;
using System.Threading.Tasks;

namespace UkParlyEndPointsFuncApp
{
    public interface IFunctionServices
    {
        Task<List<string>> PingAll();
        Task<List<string>> PingNewOrFailedEndpoints();
    }
}
