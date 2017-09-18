using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mush_Casting_Analyzer
{
    class User
    {
        private string _id;
        private string _name;
        private userStatus _status; 

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public userStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public User(string id, string name, string status)
        {
            _id = id;
            _name = name;

            switch (status)
            {
                case "ready":
                    _status = userStatus.Readied;
                    break;
                case "idle":
                    _status = userStatus.Idle;
                    break;
                case "in game":
                    _status = userStatus.InGame;
                    break;
                default:
                    _status = userStatus.Unknown;
                    break;
            }
        }
    }
}
