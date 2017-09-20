using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mush_Casting_Bookkeeping
{
    public enum userStatus
    {
        [Display(Name = "Unkown")]
        Unknown,
        [Display(Name = "New - Readied")]
        NewReadied,
        [Display(Name = "New - Idle")]
        NewIdle,
        [Display(Name = "New - In Game")]
        NewInGame,
        [Display(Name = "Readied")]
        Readied,
        [Display(Name = "Idle")]
        Idle,
        [Display(Name = "In Game")]
        InGame,
        [Display(Name = "Removed From Casting")]
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

        public long LastRecordedTimeStamp;

        public UserStatusReport(string userName, userStatus status, long timeStamp)
        {
            UserName = userName;
            StatusChanges = new List<UserStatusTimeStamp>();
            StatusChanges.Add(new UserStatusTimeStamp(status, timeStamp));
            LastRecordedTimeStamp = timeStamp;
        }
    }
}