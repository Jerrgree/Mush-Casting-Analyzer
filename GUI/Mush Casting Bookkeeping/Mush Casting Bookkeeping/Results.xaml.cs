using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mush_Casting_Bookkeeping
{
    /// <summary>
    /// Interaction logic for Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        private Dictionary<int, UserStatusReport> userStatusReport;
        private TimeZoneInfo _timeZone;
        private bool TimeIn24hrs;
        private string CastingName;
        private List<int> InitialUsers;

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
            InitialUsers = new List<int>();
        }

        public void AnalyzeData(int days, ref List<CastingDataInstances> Data)
        {
            CastingName = Data[0].CastingName;
            userStatusReport = new Dictionary<int, UserStatusReport>();

            long endTime = (long)(DateTime.Now.AddDays(-days) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            Data.Sort();


            foreach (User user in Data.First().MasterUserList)
            {
                InitialUsers.Add(user.ID);
            }

            foreach (CastingDataInstances dataPoint in Data)
            {
                if (dataPoint.TimeStamp >= endTime)
                {
                    AnalyzeInstance(dataPoint);
                }
            }

            publishDetailReport();
        }

        public void AnalyzeData(long startTime, long endTime, ref List<CastingDataInstances> Data)
        {
            userStatusReport = new Dictionary<int, UserStatusReport>();

            Data.Sort();

            foreach (User user in Data.First().MasterUserList)
            {
                InitialUsers.Add(user.ID);
            }

            foreach (CastingDataInstances dataPoint in Data)
            {
                if (dataPoint.TimeStamp <= endTime && dataPoint.TimeStamp >= startTime)
                {
                    AnalyzeInstance(dataPoint);
                }
            }

            publishDetailReport();
        }

        private void AnalyzeInstance(CastingDataInstances dataPoint)
        {
            long time = dataPoint.TimeStamp;
            foreach (User idlePlayer in dataPoint.IdleUsers)
            {
                if (!userStatusReport.ContainsKey(idlePlayer.ID))
                {
                    if (InitialUsers.Contains(idlePlayer.ID))
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
                    if (InitialUsers.Contains(readiedPlayer.ID))
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
                    if (InitialUsers.Contains(inGamePlayer.ID))
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

        private void publishDetailReport()
        {
            string titleMessage;
            titleMessage = CastingName + " user detailed report\n" + "run on " + DateTime.Now;


            Label title = new Label();
            title.Content = titleMessage;
            stack.Children.Add(title);


            DataTable masterDetailView = new DataTable();
            masterDetailView.Columns.Add("User ID", typeof(int));
            masterDetailView.Columns.Add("User Name", typeof(string));

            DataTable userDetailView = new DataTable();
            userDetailView.Columns.Add("User ID", typeof(int));
            userDetailView.Columns.Add("Status", typeof(string));
            userDetailView.Columns.Add("Date", typeof(string));

            List<KeyValuePair<int, UserStatusReport>> list = userStatusReport.ToList();
            list.Sort((pair1, pair2) => pair1.Value.UserName.CompareTo(pair2.Value.UserName));

            foreach (KeyValuePair<int, UserStatusReport> userInstance in list)
            {
                int id = userInstance.Key;
                masterDetailView.Rows.Add(id, userInstance.Value.UserName);

                foreach (UserStatusTimeStamp StatusChange in userInstance.Value.StatusChanges)
                {
                    DateTime InstanceTime = FromEpochTime(StatusChange.TimeStamp);
                    if (TimeIn24hrs)
                    {
                        userDetailView.Rows.Add(id, StatusChange.Status.ToString(), InstanceTime.ToString("yyyy-MM-d -- HH:mm:ss"));
                    }
                    else
                    {
                        userDetailView.Rows.Add(id, StatusChange.Status.ToString(), InstanceTime.ToString("yyyy-MM-d -- hh:mm:ss tt"));
                    }
                }
            }

            DataSet CombinedSet = new DataSet();

            CombinedSet.Tables.Add(masterDetailView);
            CombinedSet.Tables.Add(userDetailView);

            DataRelation relation = new DataRelation("DetailedView", CombinedSet.Tables[0].Columns[0], CombinedSet.Tables[1].Columns[0], true);
            CombinedSet.Relations.Add(relation);

            for(int i = 0; i < CombinedSet.Tables[0].Rows.Count; i++)
            {
                Expander newRow = new Expander();
                newRow.Header = CombinedSet.Tables[0].Rows[i][1];

                DataRow[] drs = CombinedSet.Tables[0].Rows[i].GetChildRows("DetailedView");
                DataTable dt = new DataTable();
                DataGrid dg = new DataGrid();

                dt.Columns.Add("User ID", typeof(int));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Date", typeof(string));

                foreach (DataRow dr in drs)
                {
                    dt.ImportRow(dr);
                }
                dg.ItemsSource = dt.DefaultView;

                newRow.Content = dg;

                stack.Children.Add(newRow);
            }
        }

        private DateTime FromEpochTime(long TimeStamp)
        {
            DateTime retValue = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimeStamp);
            return TimeZoneInfo.ConvertTimeFromUtc(retValue, TimeZone);
        }
    }
}
