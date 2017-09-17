using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mush_Casting_Analyzer
{
    class CastingDataInstances
    {
        private string _castingName;
        private long _dateTime;
        private List <User> _readiedUsers;
        private List <User> _idleUsers;
        private List <User> _playingUsers;

        public long DateTime
        {
            get
            {
                return _dateTime;
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

            _dateTime = Convert.ToInt64(values[0]);
            _castingName = values[1];

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
    }
}
