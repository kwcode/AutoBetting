using Ku.Common;
using Ku.DAL;
using Ku.Domain;
using Ku.Forms.DAL;
using Ku.Forms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ku.Forms
{
    public class GrouTask : BaseTask
    {
        public override void DoThreadTask()
        {
            while (true)
            {
                try
                {
                    DoGroupTask();
                    Thread.Sleep(1000 * 60);//300秒=5分钟
                }
                catch (Exception ex)
                {
                    msg("已经停止###" + ex.ToString());
                    Thread.Sleep(10000);
                }
            }
        }
        /// <summary>
        /// 获取之前的的50条
        /// </summary>
        private void DoGroupTask()
        {
            string path = System.Windows.Forms.Application.StartupPath + "//config//issueNo.txt";
            var hisissueId = Util.ConvertToInt32(FileHelper.ReadFile(path));

            var Id = JJ_一分快三DAL.Instance.GetNewId(hisissueId);

            List<JJ_一分快三Entity> topList = JJ_一分快三DAL.Instance.GetTop30(Id);

            GoGrouping(topList);
            msg("下期=" + Id.ToString());
            FileHelper.WriteFile(path, Id.ToString());
        }

        private void GoGrouping(List<JJ_一分快三Entity> oG1K3List)
        {
            List<JJ_一分快三Entity> daxGroup = new List<JJ_一分快三Entity>();
            List<JJ_一分快三Entity> dansGroup = new List<JJ_一分快三Entity>();
            foreach (JJ_一分快三Entity item in oG1K3List)
            {
                #region 鉴定大小
                if (daxGroup.Count == 0)
                {
                    daxGroup.Add(item);
                }
                else
                {
                    string dx = item.daxiao;
                    JJ_一分快三Entity dxE = daxGroup.LastOrDefault(o => o.daxiao == dx);
                    if (dxE == null)//不存在
                    {
                        int count = daxGroup.Count;
                        foreach (JJ_一分快三Entity item2 in daxGroup)
                        {
                            if (item2.DXGroup != count)
                            {
                                JJ_一分快三DAL.Instance.Query("UPDATE JJ_一分快三 SET DXGroup={1} WHERE ID={0}", item2.ID, count);
                            }
                        }
                        daxGroup = new List<JJ_一分快三Entity>();
                        daxGroup.Add(item);
                    }
                    else
                    {
                        daxGroup.Add(item);
                    }
                }
                #endregion

                #region 鉴定单双
                if (dansGroup.Count == 0)
                {
                    dansGroup.Add(item);
                }
                else
                {
                    string dans = item.danshuang;
                    JJ_一分快三Entity dxE = dansGroup.LastOrDefault(o => o.danshuang == dans);
                    if (dxE == null)//不存在
                    {
                        int count = dansGroup.Count;
                        foreach (JJ_一分快三Entity item2 in dansGroup)
                        {
                            if (item2.DSGroup != count)
                            {
                                JJ_一分快三DAL.Instance.Query("UPDATE JJ_一分快三 SET DSGroup={1} WHERE ID={0}", item2.ID, count);
                            }

                        }
                        dansGroup = new List<JJ_一分快三Entity>();
                        dansGroup.Add(item);
                    }
                    else
                    {
                        dansGroup.Add(item);
                    }
                }
                #endregion
            }
        }
    }
}
