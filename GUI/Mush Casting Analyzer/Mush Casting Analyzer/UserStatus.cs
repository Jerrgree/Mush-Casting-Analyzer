using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mush_Casting_Analyzer
{
    public enum userStatus
    {
        Unknown,
        NewReadied,
        NewIdle,
        NewInGame,
        Readied,
        Idle,
        InGame,
        Removed
    }

    public class UserStatusTimeStamp
    {
        public userStatus Status { get; set; }
        public long TimeStamp { get; set; }

        public UserStatusTimeStamp(userStatus _status, long _timeStamp)
        {
            Status = _status;
            TimeStamp = _timeStamp;
        }
    }

    public class UserStatusReport
    {
        public string UserName { get; set; }

        public List<UserStatusTimeStamp> StatusChanges;

        public UserStatusReport(string userName, userStatus status, long timeStamp)
        {
            UserName = userName;
            StatusChanges = new List<UserStatusTimeStamp>();
            StatusChanges.Add(new UserStatusTimeStamp(status, timeStamp));
        }
    }
}
