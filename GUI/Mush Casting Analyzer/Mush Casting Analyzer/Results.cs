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

        public Results()
        {
            InitializeComponent();

            ResultsTabControl.TabPages[0].Text = "Readied -> Idle";
            ResultsTabControl.TabPages[1].Text = "Readied -> In Game";
            ResultsTabControl.TabPages[2].Text = "Unchanged";
        }

        public void AnalyzeData(int days, ref List<CastingDataInstances> Data)
        {
            userStatusReport = new Dictionary<int, UserStatusReport>();

            long endTime = (long)(DateTime.Now.AddDays(-days) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            Data.Sort(new Comparison<CastingDataInstances>((i1, i2) => i2.CompareTo(i1))); // Sort in reverse

            foreach (CastingDataInstances dataPoint in Data)
            {
                if (dataPoint.TimeStamp < endTime)
                {
                    break;
                }

                AnalyzeInstance(dataPoint, ref Data);
            }
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
                                dataPoint.TimeStamp
                            );
                    }

                    else
                    {
                        userStatusReport[idlePlayer.ID] = new UserStatusReport
                            (
                                idlePlayer.Name,
                                userStatus.NewIdle,
                                dataPoint.TimeStamp
                            );
                    }
                }
                else if (userStatusReport[idlePlayer.ID].StatusChanges.Last().Status != userStatus.Idle || userStatusReport[idlePlayer.ID].StatusChanges.Last().Status != userStatus.NewIdle)
                {
                    userStatusReport[idlePlayer.ID].UserName = idlePlayer.Name;
                    userStatusReport[idlePlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.Idle, time));
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
                                dataPoint.TimeStamp
                            );
                    }

                    else
                    {
                        userStatusReport[readiedPlayer.ID] = new UserStatusReport
                            (
                                readiedPlayer.Name,
                                userStatus.NewReadied,
                                dataPoint.TimeStamp
                            );
                    }
                }
                else if (userStatusReport[readiedPlayer.ID].StatusChanges.Last().Status != userStatus.Readied || userStatusReport[readiedPlayer.ID].StatusChanges.Last().Status != userStatus.NewReadied)
                {
                    userStatusReport[readiedPlayer.ID].UserName = readiedPlayer.Name;
                    userStatusReport[readiedPlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.Readied, time));
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
                                dataPoint.TimeStamp
                            );
                    }

                    else
                    {
                        userStatusReport[inGamePlayer.ID] = new UserStatusReport
                            (
                                inGamePlayer.Name,
                                userStatus.NewInGame,
                                dataPoint.TimeStamp
                            );
                    }
                }
                else if (userStatusReport[inGamePlayer.ID].StatusChanges.Last().Status != userStatus.InGame || userStatusReport[inGamePlayer.ID].StatusChanges.Last().Status != userStatus.NewInGame)
                {
                    userStatusReport[inGamePlayer.ID].UserName = inGamePlayer.Name;
                    userStatusReport[inGamePlayer.ID].StatusChanges.Add(new UserStatusTimeStamp(userStatus.InGame, time));
                }
            }
            
            foreach (KeyValuePair<int, UserStatusReport> userInstance in userStatusReport)
            {
                if (userInstance.Value.StatusChanges.Last().TimeStamp != time && userInstance.Value.StatusChanges.Last().Status != userStatus.Removed)
                {
                    userInstance.Value.StatusChanges.Add(new UserStatusTimeStamp(userStatus.Removed, time));
                }
            }
        }
    }
}
