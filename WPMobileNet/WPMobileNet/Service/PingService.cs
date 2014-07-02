using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Collections.ObjectModel;

namespace WPMobileNet.Service
{
    public class PingService : BaseService
    {
        #region Properties
        private readonly AddressFamily addressFamily = AddressFamily.InterNetwork;
        private readonly SocketType socketType = SocketType.Stream;
        private readonly ProtocolType protocolType = ProtocolType.Tcp;
        DateTime timePre;
        DateTime timePost;
        private Socket connection
        {
            get
            {
                return new Socket(addressFamily, socketType, protocolType);
            }
        }
        private readonly IPEndPoint connectionIPEndPoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);

        #endregion

        #region Constructors
        public PingService()
        {
        }
        #endregion

        #region Methods
        private Task<bool> PingAsycSuccess()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            SocketAsyncEventArgs _connectionOperation = new SocketAsyncEventArgs();
            _connectionOperation.Completed += ((sender, e) =>
            {
                tcs.SetResult(e.SocketError.Equals(SocketError.Success));
            });
            _connectionOperation.RemoteEndPoint = connectionIPEndPoint;
            bool conercted = this.connection.ConnectAsync(_connectionOperation);
            return tcs.Task;
        }
        public async Task<double> PingAsyc()
        {
            timePre = DateTime.Now;
            if (await PingAsycSuccess())
            {
                timePost = DateTime.Now;
                TimeSpan ts = timePost - timePre;
                return ts.TotalMilliseconds;
            }
            return -1;
        }
        #endregion
    }
}
