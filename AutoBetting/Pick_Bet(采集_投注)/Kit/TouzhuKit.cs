using Ku.API.Enum;
using Ku.API.Model;
using Ku.Common;
using Ku.Forms.DAL;
using Ku.Forms.Model;
using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms.Kit
{
    public class TouzhuKit
    {
        private List<JJOG1K3Result> OG1K3List = new List<JJOG1K3Result>();
        /// <summary>
        /// 错误历史投注记录
        /// 投注成功后，或暂停投注清空集合
        /// </summary>
        private List<TouzhuEntity> betFaileRecordList = new List<TouzhuEntity>();
        /// <summary>
        /// 最后一次投注记录
        /// </summary>
        private TouzhuEntity lastBetRecord = new TouzhuEntity();
        /// <summary>
        /// 连续重复次数
        /// </summary>
        private int RepeatSum = 100;
        public TouzhuTypeEnum Type { get; set; }
        /// <summary>
        /// 基础投注费用 
        /// </summary>
        public int BaseMoney { get; set; }
        private MessageDisplay _md;
        public TouzhuKit(TouzhuTypeEnum type, int repeatSum, int tzmoney)
        {
            Type = type;
            RepeatSum = repeatSum;
            BaseMoney = tzmoney;

        }

        #region 开始投注计划
        public void Start(JJApiKit jjKit, List<JJOG1K3Result> list, long currentIssueNo)
        {
            OG1K3List = list;
            int res = GetPrevBettingResult(lastBetRecord.IssueNo, Type);
            string codes = "";
            if (Type == TouzhuTypeEnum.大小)
            {
                codes = list[0].daxiao;
            }
            else
            {
                codes = list[0].danshuang;

            }
            int rCount = GetRepeatTimes(codes);
            var valueNum = TurnSelected(codes);
            switch (res)
            {
                case -1://未找到
                    lastBetRecord = new TouzhuEntity();
                    break;
                case 0://未投注
                    {
                        if (rCount == RepeatSum)
                        {
                            //去投注
                            BettingDaxiao(jjKit, currentIssueNo, codes, BaseMoney);
                        }
                    }
                    break;
                case 1://投注成功
                    {
                        JJOG1K3Result entity = OG1K3List.Find(o => o.issueNo == lastBetRecord.IssueNo);
                        JJ_BetSingleDAL.Instance.UploadResult(lastBetRecord.IssueNo, entity.daxiao);

                        ClearFaileRecord();
                    }
                    break;
                case 2://投注失败
                    {
                        betFaileRecordList.Add(lastBetRecord);
                        JJOG1K3Result entity = OG1K3List.Find(o => o.issueNo == lastBetRecord.IssueNo);
                        JJ_BetSingleDAL.Instance.UploadResult(lastBetRecord.IssueNo, entity.daxiao);
                        int failCount = GetBetFaileNum();
                        int money = BaseMoney * (failCount * 3);
                        //去投注
                        BettingDaxiao(jjKit, currentIssueNo, codes, money);
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 检测上期是否投注成功
        /// <summary>
        /// -1 未找到
        /// 0未投注
        /// 1投注成功
        /// 2投注失败
        /// </summary>
        /// <param name="prevIssueNo"></param>
        /// <param name="type"></param>
        /// <param name="codes"></param>
        /// <returns></returns>
        public int GetPrevBettingResult(long prevIssueNo, TouzhuTypeEnum type)
        {
            if (lastBetRecord.IssueNo <= 0)
            {
                return 0;
            }
            string codes = lastBetRecord.codes.ToString();
            JJOG1K3Result entity = OG1K3List.Find(o => o.issueNo == prevIssueNo);
            if (entity != null)
            {
                if (type == TouzhuTypeEnum.单双)
                {
                    if (codes == entity.danshuang)
                    {
                        return 1;
                    }
                    else { return 2; }
                }
                else
                {
                    if (codes == entity.daxiao)
                    {
                        return 1;
                    }
                    else { return 2; }
                }
            }
            else
            {
                return -1;
            }

        }
        #endregion

        #region 获取投注失败次数
        public int GetBetFaileNum()
        {
            return betFaileRecordList.Count;
        }
        #endregion

        #region 计算重复次数
        /// <summary>
        /// 计算重复次数
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public int GetRepeatTimes(string codes)
        {
            int times = 0;
            List<JJOG1K3Result> rtList = new List<JJOG1K3Result>();
            foreach (JJOG1K3Result item in OG1K3List)
            {
                if (Type == TouzhuTypeEnum.单双)
                {
                    if (item.danshuang == codes)
                    {
                        rtList.Add(item);
                    }
                    else { break; }
                }
                else
                {
                    if (item.daxiao == codes)
                    {
                        rtList.Add(item);
                    }
                    else { break; }
                }

            }
            times = rtList.Count;
            return times;
        }
        #endregion

        #region 清空投注失败记录
        public void ClearFaileRecord()
        {
            betFaileRecordList = new List<TouzhuEntity>();
            lastBetRecord = new TouzhuEntity();
        }


        #endregion

        #region 反选
        public string TurnSelected(string codes)
        {
            if (codes == "大")
            {
                return "小";
            }
            else if (codes == "双")
            {

                return "单";
            }
            return "";
        }
        #endregion

        #region 投注大 小

        /// <summary>
        /// 大小投注
        /// </summary>
        /// <param name="failNum">容错次数</param>
        /// <param name="repeatNum">重复次数</param>
        public void BettingDaxiao(JJApiKit jjKit, long currentIssueNo, string valueNum, int money)
        {
            if (currentIssueNo <= lastBetRecord.IssueNo)
            {
                return;
            }
            JJBetEnum codes = valueNum == "大" ? JJBetEnum.大 : JJBetEnum.小;
            #region 调用API投注
            //string html = jjKit.BetSingle(currentIssueNo, money, codes);
            #endregion

            #region 存档
            int isExist = JJ_BetSingleDAL.Instance.GetSingle(currentIssueNo.ToString());
            if (isExist == 0)
            {
                DBParamEntity[] pramsAdd =
                            {  
                                new DBParamEntity(){ FieldName="issueNo",Value=currentIssueNo},
                                new DBParamEntity(){ FieldName="codes",Value=valueNum},
                                new DBParamEntity(){ FieldName="money",Value=money}, 
                            };
                int res = JJ_BetSingleDAL.Instance.Add(pramsAdd);
                //msg("期数=" + entity.issueNo + ",值=" + item.lotteryOpen);
            }
            #endregion
            lastBetRecord = new TouzhuEntity() { bs = 1, codes = codes, money = money, IssueNo = currentIssueNo };

        }
        #endregion


    }
}
