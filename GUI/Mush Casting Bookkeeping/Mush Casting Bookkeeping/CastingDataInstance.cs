using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mush_Casting_Bookkeeping
{
    public class CastingDataInstances : IComparable<CastingDataInstances>
    {
        private string _castingName;
        private long _timeStamp;
        private List<User> _readiedUsers;
        private List<User> _idleUsers;
        private List<User> _playingUsers;

        public long TimeStamp
        {
            get
            {
                return _timeStamp;
            }
        }

        public List<User> ReadiedUsers
        {
            get
            {
                return _readiedUsers;
            }
        }

        public List<User> IdleUsers
        {
            get
            {
                return _idleUsers;
            }
        }

        public List<User> PlayingUsers
        {
            get
            {
                return _playingUsers;
            }
        }

        public string CastingName
        {
            get
            {
                return _castingName;
            }
        }

        public CastingDataInstances(string file)
        {
            _readiedUsers = new List<User>();
            _idleUsers = new List<User>();
            _playingUsers = new List<User>();
            var reader = new StreamReader(file);

            string line = reader.ReadLine();
            var values = line.Split(',');
            values[2] = values[2].Trim();

            _timeStamp = Convert.ToInt64(values[0]);
            _castingName = values[1];

            if (values[2] != "MushCastingBookkeeping")
            {
                throw new SystemException("Invalid File");
            }

            line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                values = line.Split(',');
                values[2] = values[2].Trim();

                User _user = new User(values[0], values[1], values[2]);

                if (values[2] == "ready")
                {
                    _readiedUsers.Add(_user);
                }

                else if (values[2] == "idle")
                {
                    _idleUsers.Add(_user);
                }

                else if (values[2] == "in game")
                {
                    _playingUsers.Add(_user);
                }

                line = reader.ReadLine();
            }

            reader.Close();
        }

        public int CompareTo(CastingDataInstances rhs)
        {
            return TimeStamp.CompareTo(rhs.TimeStamp);
        }
    }
}
