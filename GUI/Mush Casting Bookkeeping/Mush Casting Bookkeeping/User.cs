using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mush_Casting_Bookkeeping
{
    public class User
    {
        private int _id;
        private string _name;
        private userStatus _status;

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public userStatus Status
        {
            get
            {
                return _status;
            }
        }

        public User(string id, string name, string status)
        {
            _id = Int32.Parse(id);
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
