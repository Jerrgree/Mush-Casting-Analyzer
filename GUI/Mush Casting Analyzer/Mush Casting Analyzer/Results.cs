using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mush_Casting_Analyzer
{
    public partial class Results : Form
    {
        private Dictionary<int, UserStatusReport> userStatusReport;
        private TimeZoneInfo _timeZone;
        private bool TimeIn24hrs;

        public TimeZoneInfo TimeZone
        {
            get
            {
                return _timeZone;
            }

            set
            {
                _timeZone = value;
            }
        }

        public Results(TimeZoneInfo timeZone, bool timeIn24hrs)
        {
            InitializeComponent();

            TimeZone = timeZone;
            TimeIn24hrs = timeIn24hrs;
        }

        public void AnalyzeData(int days, ref List<CastingDataInstances> Data)
        {
            userStatusReport = new Dictionary<int, UserStatusReport>();

            long endTime = (long)(DateTime.Now.AddDays(-days) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            Data.Sort();

            foreach (CastingDataInstances dataPoint in Data)
            {
                if (dataPoint.TimeStamp > endTime)
                {
                    AnalyzeInstance(dataPoint, ref Data);
                }
                else
                {
                    break;
                }
            }

            publishDetailReport();
        }

        public void AnalyzeData(long startTime, long endTime, ref List<CastingDataInstances> Data)
        {
            userStatusReport = new Dictionary<int, UserStatusReport>();

            Data.Sort();

            foreach(CastingDataInstances dataPoint in Data)
            {

            }
        }

        private void AnalyzeInstance(CastingDataInstances dataPoint, ref List<CastingDataInstances> Data)
        {
            long time = dataPoint.TimeStamp;
            foreach (User idlePlayer in dataPoint.IdleUsers)
            {
                if (!userStatusReport.ContainsKey(idlePlayer.ID))
                {
                    if (dataPoint == Data.First())
                    {
                        userStatusReport[idlePlayer.ID] = new UserStatusReport
                            (
                                idlePlayer.Name,
                                userStatus.Idle,
                                time
                            );
                    }

                    else
                    {
                        userStatusReport[idlePlayer.ID] = new UserStatusReport
                            (
                                idlePlayer.Name,
                                userStatus.NewIdle,
                                time
                            );
                    }
                }
                else if (userStatusReport[idlePlayer.ID].StatusChanges.Last().Status != userStatus.Idle || userStatusReport[idlePlayer.ID].StatusChanges.Last().Status != userStatus.NewIdle)
                {
                    userStatusReport[idlePlayer.ID].UserName = idlePlayer.Name;
                    userStatusReport[idlePlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.Idle, time));
                    userStatusReport[idlePlayer.ID].LastRecordedTimeStamp = time;
                }
                else
                {
                    userStatusReport[idlePlayer.ID].LastRecordedTimeStamp = time;
                }
            }

            foreach (User readiedPlayer in dataPoint.ReadiedUsers)
            {
                if (!userStatusReport.ContainsKey(readiedPlayer.ID))
                {
                    if (dataPoint == Data.First())
                    {
                        userStatusReport[readiedPlayer.ID] = new UserStatusReport
                            (
                                readiedPlayer.Name,
                                userStatus.Readied,
                                time
                            );
                    }

                    else
                    {
                        userStatusReport[readiedPlayer.ID] = new UserStatusReport
                            (
                                readiedPlayer.Name,
                                userStatus.NewReadied,
                                time
                            );
                    }
                }
                else if (userStatusReport[readiedPlayer.ID].StatusChanges.Last().Status != userStatus.Readied || userStatusReport[readiedPlayer.ID].StatusChanges.Last().Status != userStatus.NewReadied)
                {
                    userStatusReport[readiedPlayer.ID].UserName = readiedPlayer.Name;
                    userStatusReport[readiedPlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.Readied, time));
                    userStatusReport[readiedPlayer.ID].LastRecordedTimeStamp = time;
                }
                else
                {
                    userStatusReport[readiedPlayer.ID].LastRecordedTimeStamp = time;
                }
            }

            foreach (User inGamePlayer in dataPoint.PlayingUsers)
            {
                if (!userStatusReport.ContainsKey(inGamePlayer.ID))
                {
                    if (dataPoint == Data.First())
                    {
                        userStatusReport[inGamePlayer.ID] = new UserStatusReport
                            (
                                inGamePlayer.Name,
                                userStatus.InGame,
                                time
                            );
                    }

                    else
                    {
                        userStatusReport[inGamePlayer.ID] = new UserStatusReport
                            (
                                inGamePlayer.Name,
                                userStatus.NewInGame,
                                time
                            );
                    }
                }
                else if (userStatusReport[inGamePlayer.ID].StatusChanges.Last().Status != userStatus.InGame || userStatusReport[inGamePlayer.ID].StatusChanges.Last().Status != userStatus.NewInGame)
                {
                    userStatusReport[inGamePlayer.ID].UserName = inGamePlayer.Name;
                    userStatusReport[inGamePlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.InGame, time));
                    userStatusReport[inGamePlayer.ID].LastRecordedTimeStamp = time;
                }
                else
                {
                    userStatusReport[inGamePlayer.ID].LastRecordedTimeStamp = time;
                }
            }
            
            foreach (KeyValuePair<int, UserStatusReport> userInstance in userStatusReport)
            {
                if (userInstance.Value.LastRecordedTimeStamp != time && userInstance.Value.StatusChanges.Last().Status != userStatus.Removed)
                {
                    userInstance.Value.StatusChanges.Add(new UserStatusTimeStamp(userStatus.Removed, time));
                }
            }
        }

        public void publishDetailReport()
        {
            DataTable masterDetailView = new DataTable();
            masterDetailView.Columns.Add("User ID", typeof(int));
            masterDetailView.Columns.Add("User Name", typeof(string));

            DataTable userDetailView = new DataTable();
            userDetailView.Columns.Add("User ID", typeof(int));
            userDetailView.Columns.Add("Status", typeof(userStatus));
            userDetailView.Columns.Add("Date", typeof(DateTime));
            foreach (KeyValuePair<int, UserStatusReport> userInstance in userStatusReport)
            {
                int id = userInstance.Key;
                masterDetailView.Rows.Add(id, userInstance.Value.UserName);

                foreach (UserStatusTimeStamp StatusChange in userInstance.Value.StatusChanges)
                {
                    userDetailView.Rows.Add(id, StatusChange.Status, FromEpochTime(StatusChange.TimeStamp));
                }
            }

            DataSet CombinedSet = new DataSet();

            CombinedSet.Tables.Add(masterDetailView);
            CombinedSet.Tables.Add(userDetailView);

            DataRelation relation = new DataRelation("DetailedView", CombinedSet.Tables[0].Columns[0], CombinedSet.Tables[1].Columns[0], true);
            CombinedSet.Relations.Add(relation);


            DataGrid ResultDataGridView = new DataGrid();
            ResultDataGridView.Dock = DockStyle.Fill;
            ResultDataGridView.DataSource = CombinedSet.Tables[0];

            this.Controls.Add(ResultDataGridView);
        }

        private DateTime FromEpochTime(long TimeStamp)
        {
            DateTime retValue = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimeStamp);
            return TimeZoneInfo.ConvertTimeFromUtc(retValue, TimeZone);
        }
    }
}
