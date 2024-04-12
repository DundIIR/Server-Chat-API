using FirstChat.Models;
using System.Collections.Concurrent;

namespace FirstChat.Db
{
    public class DbConnect
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new ();

        public ConcurrentDictionary<string , UserConnection> connections => _connections;
    }
}
