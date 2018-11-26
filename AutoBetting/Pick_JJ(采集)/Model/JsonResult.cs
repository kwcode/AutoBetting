using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Forms
{
    public class JsonResult
    {
        public string code { get; set; }
        public Data data { get; set; }
        public object msg { get; set; }
    }

    public class Data
    {
        public Backdata backData { get; set; }
        public Cachedata cacheData { get; set; }
    }

    public class Backdata
    {
        public Lotteryopen[] lotteryOpen { get; set; }
        public string time { get; set; }
    }

    public class Lotteryopen
    {
        public string gameId { get; set; }
        public string gameName { get; set; }
        public long issueNo { get; set; }
        public string lotteryOpen { get; set; }
        public string resultDate { get; set; }
        public string openTime { get; set; }
        public int count { get; set; }
        public string daxiao { get; set; }
        public string danshuang { get; set; }
    }

    public class Cachedata
    {
        public string floatingList { get; set; }
        public string h5BannerList { get; set; }
        public string defaultPhotoList { get; set; }
        public string hallBanner { get; set; }
        public string serviceRating { get; set; }
        public string lotteryList { get; set; }
        public string footerConfig { get; set; }
        public string helpConfig { get; set; }
        public string noticeData { get; set; }
        public string bannerList { get; set; }
        public string gradeList { get; set; }
        public string rankingList { get; set; }
        public string activityConfig { get; set; }
        public string lotteryConfig { get; set; }
        public string config { get; set; }
        public string rewardData { get; set; }
    }
}

