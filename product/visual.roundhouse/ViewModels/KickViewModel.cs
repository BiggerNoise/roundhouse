using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using visual.roundhouse.Models;

namespace visual.roundhouse.ViewModels
{
    public class KickViewModel
    {
        private readonly KickModel _model;
        private readonly List<ConnectionViewModel> _connections = new List<ConnectionViewModel>();
        public KickViewModel(KickModel model)
        {
            _model = model;
            _connections.AddRange(_model.Connections.Select(c => new ConnectionViewModel(c)));
        }
        public string FileName { get { return _model.FileName; } }
        public IEnumerable<ConnectionViewModel> Connections
        {
            get { return _connections; }
        }

        public bool WithTransactions
        {
            get { return _model.Control.WithTransactions; }
            set { _model.Control.WithTransactions = value; }
        }

    }
    public class ConnectionViewModel
    {
        private readonly Connection _connection;
        public ConnectionViewModel(Connection connection)
        {
            _connection = connection;
        }
        public string ServerName
        {
            get { return _connection.ServerName; }
            set { _connection.ServerName = value; }
        }
        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }

    }
}
