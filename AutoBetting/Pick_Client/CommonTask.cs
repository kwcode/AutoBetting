using Ku.UIForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
 
using Ku.Domain;
namespace Ku.Forms
{
    public class CommonTask : BaseTask
    {

        public override void DoThreadTask()
        {
            try
            {
                List<Ku.Domain.DemoEntity> domainList = DAL.DemoDAL.Instance.GetList("*", null);
                foreach (DemoEntity item in domainList)
                {
                    msg(item.Name);
                }


            }
            catch (Exception)
            {

            }
        }

    }
}
