using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using uhttpsharp.Clients;

namespace uhttpsharp.Listeners
{
    public class ListenerSslDecorator : IHttpListener
    {
        private readonly IHttpListener _child;
        private readonly X509Certificate _certificate;
        private readonly SslProtocols _protocol;

        public ListenerSslDecorator(IHttpListener child, X509Certificate certificate, SslProtocols protocol)
        {
            _protocol = protocol;
            _child = child;
            _certificate = certificate;
        }

        public async Task<IClient> GetClient()
        {
            return new ClientSslDecorator(await _child.GetClient().ConfigureAwait(false), _certificate, _protocol);
        }

        public void Dispose()
        {
            _child.Dispose();
        }
    }
}
