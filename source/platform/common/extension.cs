using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace platform.common
{
    public static class Extension
    {
        public static Int32 Send(
            this Socket socket, Dictionary<String, String> dict)
        {
            return socket.Send(DataEncoding.get_bytes(dict));
        }
    }
}
