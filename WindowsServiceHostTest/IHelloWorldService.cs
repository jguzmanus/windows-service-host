using System.ServiceModel;
using System.Threading.Tasks;

namespace WindowsServiceHostTest
{
   [ServiceContract]
   public interface IHelloWorldService
   {
      [OperationContract]
      string SayHello(string name);

      [OperationContract]
      Task<string> SayHelloWithTask(string name);
   }
}