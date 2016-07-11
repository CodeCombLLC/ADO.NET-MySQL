// Copyright (c) Pomelo Foundation. All rights reserved.
// Licensed under the MIT. See LICENSE in the project root for license information.

using Pomelo.Data.MySql;

using System;
using System.IO;


namespace Pomelo.Data.Common
{
    /// <summary>
    /// Summary description for StreamCreator.
    /// </summary>
    internal class StreamCreator
    {
        string hostList;
        uint port;
        string pipeName;
        uint keepalive;
        DBVersion driverVersion;

        public StreamCreator(string hosts, uint port, string pipeName, uint keepalive, DBVersion driverVersion)
        {
            hostList = hosts;
            if (hostList == null || hostList.Length == 0)
                hostList = "localhost";
            this.port = port;
            this.pipeName = pipeName;
            this.keepalive = keepalive;
            this.driverVersion = driverVersion;
        }

        public static Stream GetStream(string server, uint port, string pipename, uint keepalive, DBVersion v, uint timeout)
        {
            MySqlConnectionStringBuilder settings = new MySqlConnectionStringBuilder();
            settings.Server = server;
            settings.Port = port;
            settings.PipeName = pipename;
            settings.Keepalive = keepalive;
            settings.ConnectionTimeout = timeout;
            return GetStream(settings);
        }

        public static Stream GetStream(MySqlConnectionStringBuilder settings)
        {
            switch (settings.ConnectionProtocol)
            {
                case MySqlConnectionProtocol.Tcp: return GetTcpStream(settings);
#if RT
        case MySqlConnectionProtocol.UnixSocket: throw new NotImplementedException();
        case MySqlConnectionProtocol.SharedMemory: throw new NotImplementedException();
#else
#if !NETSTANDARD1_6
        case MySqlConnectionProtocol.UnixSocket: return GetUnixSocketStream(settings);        
        case MySqlConnectionProtocol.SharedMemory: return GetSharedMemoryStream(settings);
#endif

#endif
#if !NETSTANDARD1_6
                case MySqlConnectionProtocol.NamedPipe: return GetNamedPipeStream(settings);
#endif
            }
            throw new InvalidOperationException(Resources.UnknownConnectionProtocol);
        }

        private static Stream GetTcpStream(MySqlConnectionStringBuilder settings)
        {
            MyNetworkStream s = MyNetworkStream.CreateStream(settings, false);
            return s;
        }

#if !NETSTANDARD1_6
    private static Stream GetUnixSocketStream(MySqlConnectionStringBuilder settings)
    {
      if (Platform.IsWindows())
        throw new InvalidOperationException(Resources.NoUnixSocketsOnWindows);

      MyNetworkStream s = MyNetworkStream.CreateStream(settings, true);
      return s;
    }

        private static Stream GetSharedMemoryStream(MySqlConnectionStringBuilder settings)
    {
      SharedMemoryStream str = new SharedMemoryStream(settings.SharedMemoryName);
      str.Open(settings.ConnectionTimeout);
      return str;
    }


    private static Stream GetNamedPipeStream(MySqlConnectionStringBuilder settings)
    {
      Stream stream = NamedPipeStream.Create(settings.PipeName, settings.Server, settings.ConnectionTimeout);
      return stream;
    }
#endif

    }
}
